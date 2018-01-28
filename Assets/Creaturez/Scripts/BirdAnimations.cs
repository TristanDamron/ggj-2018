using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimations : MonoBehaviour {
    private float timer;
	void Update () {
        this.timer += Time.deltaTime;

        if (this.timer >= 5f)
        {
            bool flapping = GetComponent<Animator>().GetBool("flapping");
            GetComponent<Animator>().SetBool("flapping", !flapping);
            this.timer = 0f;
        }
	}
}
