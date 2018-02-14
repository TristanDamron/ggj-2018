using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclePortalController : MonoBehaviour
{

    [SerializeField]
    GameObject _tentacleHolder;
    [SerializeField]
    GameObject _headHolder;

    private void Start()
    {
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
    }

    public void Fed()
    {
        Invoke("InvokeDestroy", 2f);
        Tips.current = Tips.Events.Patrolling;
    }

    void InvokeDestroy()
    {
        Destroy(gameObject);
    }
}

