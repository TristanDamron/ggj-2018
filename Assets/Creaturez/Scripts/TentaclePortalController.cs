using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclePortalController : MonoBehaviour
{

    [SerializeField]
    GameObject _tentacleHolder;

    private void Start()
    {
        Invoke("EnableTentacleHolder", 2f);
    }

    public void EnableTentacleHolder()
    {
        _tentacleHolder.SetActive(true);
    }

    public void Fed()
    {
        Invoke("InvokeDestroy", 2f);
    }

    void InvokeDestroy()
    {
        Destroy(gameObject);
    }
}

