using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenAnimation : MonoBehaviour {
    public LoadScene obj;

    void Update () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(-1000f, transform.position.y, transform.position.z), Time.deltaTime * 20f);
        GameObject.Find("Audio Source").GetComponent<AudioSource>().Play();

        if (transform.position.x >= -200f)
        {
            obj.LoadNextScene();
        }
    }
}
