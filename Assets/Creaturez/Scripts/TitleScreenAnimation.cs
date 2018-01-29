using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenAnimation : MonoBehaviour {
    public LoadScene obj;

    [SerializeField]
    AudioSource _source;


    private void OnEnable()
    {
        _source.PlayOneShot(_source.clip);
        Invoke("InvokeNextScene",2f);
    }

    void InvokeNextScene()
    {
        obj.LoadNextScene();

    }

    void Update () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(-1000f, transform.position.y, transform.position.z), Time.deltaTime * 20f);
    }
}
