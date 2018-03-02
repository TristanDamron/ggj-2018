
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    public Transform targetRotationTransform;
    [SerializeField]
    TentacleMovement _movement;

    List<Vector3> _rotPoints;
    IEnumerator _routine;

    [SerializeField]
    TentaclePortalController _portalController;

    private void Start()
    {
        _movement.speed = 2f;
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
            //@TODO: The tentacle keeps rotating behind the portal parent. Restrict random vector to be in front of the portal parent     
            var refPos = targetRotationTransform.localEulerAngles;            
            var randomPos = new Vector3(Random.Range(refPos.x - 20, refPos.x - 1),
                                        Random.Range(refPos.y - 1, refPos.y + 1),
                                        Random.Range(refPos.z - 20, refPos.z - 1));
            _rotPoints.Add(randomPos);
        }

        while(true)
        {
            targetRotationTransform.localEulerAngles = _rotPoints[Random.Range(0, _rotPoints.Count)];
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

        targetRotationTransform.localEulerAngles = new Vector3(-8.48f, -78.14f, -1.65f);

        if(_portalController != null)
        {
            _portalController.Fed();
        }
    }
}
