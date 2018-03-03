using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointScript : MonoBehaviour {

    [SerializeField]
    ParticleSystem _particleSystem;

    [SerializeField]
    private Rigidbody _anchor;
    private SpringJoint _joint;
    private Transform _anchorTransform;
    private Rigidbody _rb;
    private float increment;
    private IEnumerator _routine,_patrolRoutine, _liveRoutine;
    List<Vector3> _patrolPoints;
    bool _snatched;

    [SerializeField]
    private BirdAnimations _birdAnims;

    private void Start()
    {
        _anchorTransform = _anchor.GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        CreateAJoint();
        _patrolRoutine = PatrolAround();
        StartCoroutine(_patrolRoutine);
        _liveRoutine = TimeTillAlive();
        StartCoroutine(_liveRoutine);
    }

    public void Grabbed()
    {
        _birdAnims.SetAnimation("Grabbed");
        _birdAnims.LookAtCam();
        _birdAnims.SetPos();

        StopCoroutine(_liveRoutine);

        CancelInvoke("InvokePatrolRoutine");

        if(_patrolRoutine != null)
        {
            StopCoroutine(_patrolRoutine);
        }

        if(GetComponent<SpringJoint>())
        {
            return;
        }

        CreateAJoint();
    }

    public void Snatched(Transform t)
    {
        StopCoroutine(_liveRoutine);
        StopCoroutine(_patrolRoutine);
        DestroyJoint();
        transform.position = t.position;
        transform.SetParent(t);
    }

    IEnumerator TimeTillAlive()
    {
        
        float time = Random.Range(10, 60);
        while (time > 0)
            
        {
            time -= Time.deltaTime;
            yield return new WaitForSeconds(0);
        }

            _particleSystem.Play(true);
            _birdAnims.DisableThisGameObject();
            Invoke("InvokeDestroy", 10f);
    }

    void InvokeDestroy()
    {
        Destroy(gameObject);
    }

    public void Release(Vector3 targetPoint)
    {        
        _anchorTransform.SetParent(null);
        _birdAnims.SetAnimation("Throw");
        _birdAnims.LookAway();

        StartCoroutine(_liveRoutine);

        if(_routine != null)
        {
            StopCoroutine(_routine);
        }

        if(_patrolRoutine != null)
        {
            StopCoroutine(_patrolRoutine);
        }

        _routine = UpdateY(targetPoint, false);
        StartCoroutine(_routine);

        CancelInvoke("InvokePatrolRoutine");
        Invoke("InvokePatrolRoutine",1f);
    }

    void InvokePatrolRoutine()
    {
        StartCoroutine(_patrolRoutine);
    }

    public void Release(Vector3 targetPoint, bool hit)
    {
        _anchorTransform.SetParent(null);
        _birdAnims.SetAnimation("Throw");
        _birdAnims.LookAway();

        if (_routine != null)
        {
            StopCoroutine(_routine);
        }

        _routine = UpdateY(targetPoint, hit);
        StartCoroutine(_routine);
    }

    IEnumerator PatrolAround()
    {        
        while (true)
        {
            _patrolPoints = new List<Vector3>();        
            for (int i = 0; i < 10; i++)
            {
                var refPos = _anchorTransform.position;
                var randomPos = new Vector3(Random.Range(refPos.x - 1, refPos.x + 1),
                                        Random.Range(refPos.y - 1, refPos.y + 1),
                                        Random.Range(refPos.z - 1, refPos.z + 1));
                _patrolPoints.Add(randomPos);
            }
            
            _anchorTransform.position = _patrolPoints[Random.Range(0, _patrolPoints.Count)];
            _birdAnims.LookAt(_anchorTransform);
            var setRandomTime = Random.Range(.1f, .7f);
            yield return new WaitForSeconds(setRandomTime);
        }
    }

    IEnumerator UpdateY(Vector3 target, bool hit)
    {
        this.increment = 0f;

        while (this.increment < 2.5f)
        {
            Vector3 moveLerp = Vector3.Lerp(transform.position, target, Time.deltaTime * 2f);
            this.increment += Time.deltaTime;
            if (hit)
            {
                moveLerp.y = 0.5f * Mathf.Sin(Mathf.Clamp01(this.increment) * 5f);
            } else
            {
                moveLerp.y = 0.5f * Mathf.Sin(Mathf.Clamp01(this.increment / 2f) * 10f);
            }
            _anchorTransform.position = moveLerp;            
            yield return new WaitForSeconds(0);
        }
    }

    public void DestroyJoint()
    {
        Destroy(GetComponent<SpringJoint>());
    }

    public Transform ReturnAnchorTransform()
    {

        if (_patrolRoutine != null)
        {
            StopCoroutine(_patrolRoutine);
        }

        if (_routine != null)
        {
            StopCoroutine(_routine);
        }


        return _anchorTransform;
    }

    IEnumerator UpdateY()
    {
        float ret = 0f;
        this.increment += Time.deltaTime;
        ret = 0.5f * Mathf.Sin(Mathf.Clamp01(this.increment) * 2f);
        yield return ret;
    }

    void CreateAJoint()
    {
        
        gameObject.AddComponent<SpringJoint>();
        _joint = GetComponent<SpringJoint>();
        _joint.connectedBody = _anchor;
        _joint.spring = 75f;
        _joint.damper = 0.3f;
        _joint.tolerance = 0f;
        _joint.enablePreprocessing = true;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _anchor.transform.SetParent(null);
    }

    public void AttachNewJoint(Rigidbody newAnchor)
    {
        Destroy(_joint);
        _anchor = newAnchor;
        CreateAJoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tentacles")
        {
            //Do Some random shit
        }
    }
}
