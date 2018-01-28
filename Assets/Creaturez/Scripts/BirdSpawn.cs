using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawn : MonoBehaviour {

    public InFrontOfCamera _front;
    public float timer;
    public GameObject bird;

    void Update()
    {
        this.timer += Time.deltaTime;

        if (this.timer >= 3f)
        {
            var spawnPos = _front.ReturnVectorPointInFront(Random.Range(2,4));
            Instantiate(this.bird, new Vector3(spawnPos.x + Random.Range(-1f, 1f), spawnPos.y, spawnPos.z + Random.Range(-1f, 1f)), transform.rotation);
            this.timer = 0f;
        }
    }
}
