using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
	public static int tentaclesFed;
	public static float playerHP;
	public static float creaturezHP;
	public static bool flash, won;

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
	[SerializeField]
	private GameObject _credits;
	[SerializeField]
	private ParticleSystem _confetti;
	[SerializeField]
	private AudioClip _victory;
	[SerializeField]
	private AudioSource _src;

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

		if (creaturezHP <= 0 && !won) {
			won = true;
			_src.clip = _victory;
			_src.loop = false;
			_src.Play();
			Invoke("StartConfetti", 4.3f);
			Invoke("ActivateCredits", 10f);
		}
	} 

	public void StartConfetti() {
		_confetti.Play();
	}

	public void ActivateCredits() {
		_credits.SetActive(true);
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
