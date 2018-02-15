using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public static int tentaclesFed;
	public static float playerHP;
	public static float creaturezHP;
	public static bool flash;

	[SerializeField]
	private Slider _creaturezSlider;

	[SerializeField]
	private Slider _playerSlider;
	
	[SerializeField]
	private RawImage _flash;
	
	[SerializeField]
	private ParticleSystem _particles; 
	[SerializeField]
	private GameObject _gameOver;

	void Start() {
		playerHP = 10;
		creaturezHP = 50;
	}

	void Update() {
		_creaturezSlider.value = creaturezHP;
		_playerSlider.value = playerHP;

		if (flash) {
			flash = false;
			Flash();
		}

		if (playerHP <= 0) {
			_gameOver.SetActive(true);
		}
	} 

	public void Flash() {
		Color c = _flash.color;
		c.a = 0.25f; 
		_flash.color = c;
		_particles.Play();
		Invoke("ResetAlpha", 1f);
	}

	public void ResetAlpha() {
		Color c = _flash.color;
		c.a = 0f; 
		_flash.color = c;		
		_particles.Stop();
	}

	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
