using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public bool autoLoad;
    private float timer;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if (autoLoad)
        {
            this.timer += Time.deltaTime;

            if (this.timer >= 8f)
            {
                LoadNextScene();
            }
        }
    }
}
