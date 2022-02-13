using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngameSettings : MonoBehaviour {

	public static bool inSettings = false;
	public static IngameSettings instance = null;

	public GameObject settingsView;
	public Slider musicSlider;
	public Slider sfxSlider;


	private void Start() {
		instance = this;
		Hide();
	}

	public void Show() {
		inSettings = true;
		musicSlider.value = AudioController.instance.musicVolume;
		sfxSlider.value = AudioController.instance.sfxVolume;
		settingsView.SetActive(true);
	}

	public void Hide() {
		inSettings = false;
		settingsView.SetActive(false);
	}

	private void Update() {
		if (EventSystem.current.currentSelectedGameObject == null)
			musicSlider.Select();
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Hide();
		}
	}



	public void UpdateMusic(float value) {
		AudioController.instance.SetMusicVolume(value);
	}

	public void UpdateSfx(float value) {
		AudioController.instance.SetSfxVolume(value);
	}
}
