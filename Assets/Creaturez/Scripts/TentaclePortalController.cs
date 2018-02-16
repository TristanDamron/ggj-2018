using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclePortalController : MonoBehaviour
{
    [SerializeField]
    GameObject _tentacleHolder;
    [SerializeField]
    GameObject _headHolder;
    [SerializeField]
    GameObject _featherParticles;

    private void Start()
    {
        if (Manager.creaturezHP <= 0) {
            Destroy(gameObject);
        }

        if (Manager.tentaclesFed >= 3) {
            Manager.tentaclesFed = 0;
            Invoke("EnableHeadHolder", 2f);
        } else {
            Invoke("EnableTentacleHolder", 2f);
        }
    }

    public void EnableTentacleHolder()
    {
        _tentacleHolder.SetActive(true);
        Tips.current = Tips.Events.PickedUpBird;
    }

    public void EnableHeadHolder()
    {
        _headHolder.SetActive(true);
        Tips.current = Tips.Events.FightingMonster;
        Invoke("InvokeDestroy", 10f);
    }

    public void Fed()
    {
        Invoke("InvokeDestroy", 2f);
        Instantiate(_featherParticles, transform.localPosition, transform.localRotation);
        Manager.tentaclesFed += 1;
    }

    void InvokeDestroy()
    {
        Tips.current = Tips.Events.Patrolling;        
        Destroy(gameObject);
    }
}

