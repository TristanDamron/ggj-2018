using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squawk : MonoBehaviour {
    public AudioClip[] clips;
    public float timer;

    void Update()
    {
        this.timer += Time.deltaTime;

        if (this.timer >= 10f)
        {
            int rand = Random.Range(0, clips.Length);
            GameObject.Find("Audio Source").GetComponent<AudioSource>().PlayOneShot(this.clips[rand], 0.25f);
            this.timer = 0f;
        }
    }
}
