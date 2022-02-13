using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public FadeIn fadeIn;

	[Header("Main menu")]
	public GameObject mainMenu;
	public Button playButton;

	[Header("Settings")]
	public GameObject settingsMenu;
	public Slider musicSlider;
	public Slider sfxSlider;

	[Header("Credits")]
	public GameObject creditsMenu;
	public Button creditsReturnButton;

	private Selectable lastSelected;
	private bool started;


	private void Start() {
		musicSlider.value = AudioController.instance.musicVolume;
		sfxSlider.value = AudioController.instance.sfxVolume;
		GoToMain();
	}

	private void Update() {
		if (EventSystem.current.currentSelectedGameObject == null)
			lastSelected.Select();
	}

	public void GoToMain() {
		mainMenu.SetActive(true);
		settingsMenu.SetActive(false);
		creditsMenu.SetActive(false);

		playButton.Select();
		lastSelected = playButton;
	}

	public void GoToSettings() {
		mainMenu.SetActive(false);
		settingsMenu.SetActive(true);
		creditsMenu.SetActive(false);

		musicSlider.Select();
		lastSelected = musicSlider;
	}

	public void GoToCredits() {
		mainMenu.SetActive(false);
		settingsMenu.SetActive(false);
		creditsMenu.SetActive(true);

		creditsReturnButton.Select();
		lastSelected = creditsReturnButton;
	}

	public void StartGame() {
		if (!started) {
			started = true;
			Furnisher.startedGame = true;
			StartCoroutine(CoStartGame());
		}
	}

	private IEnumerator CoStartGame() {
		fadeIn.speedMultiplier = 0.3f;
		fadeIn.FadeToBlack();
		yield return new WaitForSeconds(FadeIn.FADE_OUT_SPEED / 0.3f);
		SceneManager.LoadScene("TheCrib");
	}

	public void UpdateMusic(float value) {
		AudioController.instance.SetMusicVolume(value);
	}

	public void UpdateSfx(float value) {
		AudioController.instance.SetSfxVolume(value);
	}

}
