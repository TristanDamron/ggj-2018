using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    
    [SerializeField]
    TentacleMovement _movement;

    [SerializeField]
    Transform _targetRotationTransform;

    List<Vector3> _rotPoints;
    IEnumerator _routine;


    private void Start()
    {
        _movement.speed = 1f;
        _routine = RandomRots();
        Invoke("InvokeRoutine", 1f);
    }

    void InvokeRoutine()
    {
        _movement.speed = 2f;
        StartCoroutine(_routine);
    }

    IEnumerator RandomRots()
    {
        _rotPoints = new List<Vector3>();

        for (int i = 0; i < 10; i++)
        {
            var refPos = _targetRotationTransform.localEulerAngles;
            var randomPos = new Vector3(Random.Range(refPos.x - 20, refPos.x + 20),
                                        Random.Range(refPos.y - 1, refPos.y + 1),
                                        Random.Range(refPos.z - 20, refPos.z + 20));
            _rotPoints.Add(randomPos);
        }

        while(true)
        {
            _targetRotationTransform.localEulerAngles = _rotPoints[Random.Range(0, _rotPoints.Count)];
            var time = Random.Range(1, 5);
            yield return new WaitForSeconds(time);
        }
    }

    public void BirdySnatch()
    {
        _movement.speed = .5f;
        if(_routine != null)
        {
            StopCoroutine(_routine);
        }

        _targetRotationTransform.localEulerAngles = new Vector3(0, -20, -8);
    }
}
