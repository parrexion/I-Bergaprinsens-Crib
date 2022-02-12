using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	public static AudioController instance = null;

	private void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
			Setup();
		}
		else {
			Destroy(gameObject);
		}
	}

	public AudioSource bkgSource;
	public AudioSource bkgSourceFade;
	public AudioSource voiceSource;
	public AudioSource sfxSource;

	[Header("Settings")]
	public float fadeTime = 1f;
	public float musicVolume = 0.2f;
	public float voiceVolume = 0.2f;
	public float sfxVolume = 0.2f;


	private void Setup() {
		bkgSource.volume = bkgSourceFade.volume = musicVolume;
		bkgSource.loop = bkgSourceFade.loop = true;
		voiceSource.volume = voiceVolume;
		voiceSource.loop = false;
		sfxSource.volume = sfxVolume;
		sfxSource.loop = false;
	}

	public void SetMusicVolume(float volume) {
		musicVolume = volume;
		bkgSource.volume = bkgSourceFade.volume = musicVolume;
	}

	public void SetVoiceVolume(float volume) {
		voiceVolume = volume;
		voiceSource.volume = voiceVolume;
	}

	public void SetSfxVolume(float volume) {
		sfxVolume = volume;
		sfxSource.volume = sfxVolume;
	}

	public void PlayMusic(AudioClip music) {
		bkgSourceFade.clip = bkgSource.clip;
		bkgSourceFade.timeSamples = bkgSource.timeSamples;
		bkgSource.clip = music;
		bkgSource.Play();
		bkgSourceFade.Play();
		StartCoroutine(FadeMusic());
	}

	public void PlayVoiceOver(AudioClip voiceOver) {
		voiceSource.clip = voiceOver;
		voiceSource.Play();
	}

	public void PlaySfx(AudioClip sfx) {
		sfxSource.PlayOneShot(sfx);
	}

	private IEnumerator FadeMusic() {
		float duration = fadeTime;
		bkgSourceFade.volume = musicVolume;
		bkgSource.volume = 0f;
		while (duration > 0f) {
			yield return null;
			duration -= Time.deltaTime;
			bkgSourceFade.volume = duration / fadeTime * musicVolume;
			bkgSource.volume = (1f - duration / fadeTime) * musicVolume;
		}
		bkgSourceFade.Stop();
	}
}
