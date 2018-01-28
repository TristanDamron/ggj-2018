﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlanZucconi.IK
{

    // A typical error function to minimise
    public delegate float ErrorFunction(Vector3 target, float[] solution);

    public struct PositionRotation
    {
        Vector3 position;
        Quaternion rotation;

        public PositionRotation(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        // PositionRotation to Vector3
        public static implicit operator Vector3(PositionRotation pr)
        {
            return pr.position;
        }
        // PositionRotation to Quaternion
        public static implicit operator Quaternion(PositionRotation pr)
        {
            return pr.rotation;
        }
    }

    //[ExecuteInEditMode]
    public class InverseKinematics : MonoBehaviour
    {
        [Header("Joints")]
        public Transform BaseJoint;
        //[HideInInspector]
        [ReadOnly]
        public RobotJoint[] Joints = null;
        // The current angles
        [ReadOnly]
        public float[] Solution = null;

        [Header("Destination")]
        public Transform Effector;
        [Space]
        public Transform Destination;
        public float DistanceFromDestination;
        private Vector3 target;

        [Header("Inverse Kinematics")]
        [Range(0, 1f)]
        public float DeltaGradient = 0.1f; // Used to simulate gradient (degrees)
        [Range(0, 100f)]
        public float LearningRate = 0.1f; // How much we move depending on the gradient

        [Space()]
        [Range(0, 0.25f)]
        public float StopThreshold = 0.1f; // If closer than this, it stops
        [Range(0, 10f)]
        public float SlowdownThreshold = 0.25f; // If closer than this, it linearly slows down


        public ErrorFunction ErrorFunction;


        [Header("Tentacle")]
        public bool IsTentacle = false;

        [Space]
        [Range(0, 10)]
        public float OrientationWeight = 0.5f;
        [Range(0, 10)]
        public float TorsionWeight = 0.5f;
        public Vector3 TorsionPenality = new Vector3(1, 0, 0);

        [Header("Debug")]
        public bool DebugDraw = true;



        // Use this for initialization
        void Start()
        {
            if (Joints == null)
                GetJoints();

            // The error function to minimise
            if (IsTentacle)
                ErrorFunction = delegate (Vector3 target, float[] solution)
                {
                    PositionRotation result = ForwardKinematics(Solution);

                    // Minimise torsion (rotation on x axis)
                    float torsion = 0;
                    for (int i = 0; i < solution.Length; i++)
                    {
                        torsion += Mathf.Abs(solution[i]) * TorsionPenality.x;
                        torsion += Mathf.Abs(solution[i]) * TorsionPenality.y;
                        torsion += Mathf.Abs(solution[i]) * TorsionPenality.z;
                    }

                    return
                        // The distance
                        Vector3.Distance(target, result)

                        // The orientation of the effector
                        + Mathf.Abs(Quaternion.Angle(result, Destination.rotation) / 180f)
                            * OrientationWeight

                        // The torsion
                        + (torsion / solution.Length)
                            * TorsionWeight;
                    ;

                };
            else
                ErrorFunction = DistanceFromTarget;
        }

        [ExposeInEditor(RuntimeOnly = false)]
        public void GetJoints()
        {
            Joints = BaseJoint.GetComponentsInChildren<RobotJoint>();
            Solution = new float[Joints.Length];
        }



        // Update is called once per frame
        void Update()
        {
            // Do we have to approach the target?
            //Vector3 direction = (Destination.position - Effector.transform.position).normalized;
            Vector3 direction = (Destination.position - transform.position).normalized;
            target = Destination.position - direction * DistanceFromDestination;
            //if (Vector3.Distance(Effector.position, target) > Threshold)
            if (ErrorFunction(target, Solution) > StopThreshold)
                ApprochTarget(target);

            if (DebugDraw)
            {
                Debug.DrawLine(Effector.transform.position, target, Color.green);
                Debug.DrawLine(Destination.transform.position, target, new Color(0, 0.5f, 0));
            }
        }

        public void ApprochTarget(Vector3 target)
        {
            // Starts from the end, up to the base
            // Starts from joints[end-2]
            //  so it skips the hand that doesn't move!
            for (int i = Joints.Length - 1; i >= 0; i--)
            //for (int i = 0; i < Joints.Length - 1 - 1; i++)
            {
                // FROM:    error:      [0, StopThreshold,  SlowdownThreshold]
                // TO:      slowdown:   [0, 0,              1                ]
                float error = ErrorFunction(target, Solution);
                float slowdown = Mathf.Clamp01((error - StopThreshold) / (SlowdownThreshold - StopThreshold));

                // Gradient descent
                float gradient = CalculateGradient(target, Solution, i, DeltaGradient);
                Solution[i] -= LearningRate * gradient * slowdown;
                // Clamp
                Solution[i] = Joints[i].ClampAngle(Solution[i]);

                // Early termination
                if (ErrorFunction(target, Solution) <= StopThreshold)
                    break;
            }


            for (int i = 0; i < Joints.Length - 1; i++)
            {
                Joints[i].MoveArm(Solution[i]);
            }
        }

        /* Calculates the gradient for the invetse kinematic.
         * It simulates the forward kinematics the i-th joint,
         * by moving it +delta and -delta.
         * It then sees which one gets closer to the target.
         * It returns the gradient (suggested changes for the i-th joint)
         * to approach the target. In range (-1,+1)
         */
        public float CalculateGradient(Vector3 target, float[] Solution, int i, float delta)
        {
            // Saves the angle,
            // it will be restored later
            float solutionAngle = Solution[i];

            // Gradient : [F(x+h) - F(x)] / h
            // Update   : Solution -= LearningRate * Gradient
            float f_x = ErrorFunction(target, Solution);

            Solution[i] += delta;
            float f_x_plus_h = ErrorFunction(target, Solution);

            float gradient = (f_x_plus_h - f_x) / delta;

            // Restores
            Solution[i] = solutionAngle;

            return gradient;
        }

        // Returns the distance from the target, given a solution
        public float DistanceFromTarget(Vector3 target, float[] Solution)
        {
            Vector3 point = ForwardKinematics(Solution);
            return Vector3.Distance(point, target);
        }


        /* Simulates the forward kinematics,
         * given a solution. */
        public PositionRotation ForwardKinematics(float[] Solution)
        {
            Vector3 prevPoint = Joints[0].transform.position;
            //Quaternion rotation = Quaternion.identity;

            // Takes object initial rotation into account
            Quaternion rotation = transform.rotation;
            for (int i = 1; i < Joints.Length; i++)
            {
                // Rotates around a new axis
                rotation *= Quaternion.AngleAxis(Solution[i - 1], Joints[i - 1].Axis);
                Vector3 nextPoint = prevPoint + rotation * Joints[i].StartOffset;

                if (DebugDraw)
                    Debug.DrawLine(prevPoint, nextPoint, Color.blue);

                prevPoint = nextPoint;
            }

            // The end of the effector
            return new PositionRotation(prevPoint, rotation);
        }
    }
}