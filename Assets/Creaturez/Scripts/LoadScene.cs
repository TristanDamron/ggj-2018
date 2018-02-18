using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public bool autoLoad;
    private float timer;

    [SerializeField]
    private VideoPlayer _player;

    void Start () {
        _player = GetComponent<VideoPlayer>();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if (autoLoad)
        {
            this.timer += Time.deltaTime;

            if (this.timer >= _player.clip.length)
            {
                LoadNextScene();
            }
        }
    }
}
