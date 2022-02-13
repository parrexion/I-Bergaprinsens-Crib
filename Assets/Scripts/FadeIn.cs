using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {
	public const float FADE_OUT_SPEED = 0.5f;

	public CanvasGroup fadeGroup;
	public float speedMultiplier = 1f;


	private void Awake() {
		Zone.onSpawnStarting += FadeToBlack;
		Zone.onSpawnComplete += FadeFromBlack;
	}

	private void OnDestroy() {
		Zone.onSpawnStarting -= FadeToBlack;
		Zone.onSpawnComplete -= FadeFromBlack;
	}

	public void FadeToBlack() {
		fadeGroup.alpha = 0f;
		fadeGroup.blocksRaycasts = true;
		StartCoroutine(CoFadeToBlack());
	}

	public void FadeFromBlack() {
		fadeGroup.alpha = 1f;
		fadeGroup.blocksRaycasts = true;
		StartCoroutine(CoFadeFromBlack());
	}

	private IEnumerator CoFadeToBlack() {
		float duration = FADE_OUT_SPEED;
		while(duration > 0f) {
			yield return null;
			duration -= Time.deltaTime * speedMultiplier;
			fadeGroup.alpha = 1f - duration / FADE_OUT_SPEED;
		}
		fadeGroup.alpha = 1f;
		yield break;
	}

	private IEnumerator CoFadeFromBlack() {
		float duration = FADE_OUT_SPEED;
		while (duration > 0f) {
			yield return null;
			duration -= Time.deltaTime * speedMultiplier;
			fadeGroup.alpha = duration / FADE_OUT_SPEED;
		}
		fadeGroup.alpha = 0f;
		fadeGroup.blocksRaycasts = false;
		yield break;
	}
}
