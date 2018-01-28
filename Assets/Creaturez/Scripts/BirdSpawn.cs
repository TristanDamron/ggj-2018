using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawn : MonoBehaviour {
    public float timer;
    public GameObject bird;

    void Update()
    {
        this.timer += Time.deltaTime;

        if (this.timer >= 30f)
        {
            Instantiate(this.bird, new Vector3(transform.localPosition.x + Random.Range(-0.25f, 0.25f), transform.localPosition.y, transform.localPosition.z + Random.Range(-0.25f, 0.25f)), transform.rotation);
            this.timer = 0f;
        }
    }
}
