using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour {

	public static System.Action<TextAsset> startDialogue;
	public static System.Action endDialogue;
	public static bool runningDialogue = false;

	public CharacterLibrary characterLibrary;
	public GameObject dialogueView;

	[Header("Left side")]
	public GameObject leftView;
	public Image portraitLeft;
	public TextMeshProUGUI speakerNameLeft;
	public TextMeshProUGUI dialogueTextLeft;

	[Header("Right side")]
	public GameObject rightView;
	public Image portraitRight;
	public TextMeshProUGUI speakerNameRight;
	public TextMeshProUGUI dialogueTextRight;

	private CoolStory currentStory;


	private void Awake() {
		startDialogue += OnStartDialogue;
		dialogueView.SetActive(false);
	}

	private void OnDestroy() {
		startDialogue -= OnStartDialogue;
	}

	private void Update() {
		if (runningDialogue) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				ContinueDialogue();
			}
		}
	}

	private void OnStartDialogue(TextAsset dialogue) {
		if (currentStory != null || runningDialogue)
			return;

		currentStory = new CoolStory(dialogue);
		Debug.Log(dialogue.text);
		leftView.SetActive(false);
		rightView.SetActive(false);
		dialogueView.SetActive(true);
		StartCoroutine(DelayedStart());
	}

	private void ContinueDialogue() {
		if (currentStory != null && !runningDialogue)
			return;

		if (currentStory.CanContinue) {
			CoolStory.LineData data = currentStory.GetNextText();
			if (!string.IsNullOrEmpty(data.characterId)) {
				CharacterData speaker = characterLibrary.GetCharacter(data.characterId);
				if (speaker.rightSided) {
					speakerNameRight.text = speaker.characterName;
					portraitRight.sprite = speaker.portrait;
					portraitRight.color = speaker.standInColor;
					dialogueTextRight.text = data.text;
					leftView.SetActive(false);
					rightView.SetActive(true);
				}
				else {
					speakerNameLeft.text = speaker.characterName;
					portraitLeft.sprite = speaker.portrait;
					portraitLeft.color = speaker.standInColor;
					dialogueTextLeft.text = data.text;
					leftView.SetActive(true);
					rightView.SetActive(false);
				}
			}
			else {
				speakerNameLeft.text = "";
				portraitLeft.sprite = null;
				portraitLeft.color = Color.white;
				dialogueTextLeft.text = data.text;
				leftView.SetActive(true);
				rightView.SetActive(false);
			}
		}
		else {
			FinishDialogue();
		}
	}

	private void FinishDialogue() {
		currentStory = null;
		dialogueView.SetActive(false);
		StartCoroutine(DelayedEnd());
	}

	private IEnumerator DelayedStart() {
		yield return null;
		runningDialogue = true;
		ContinueDialogue();
	}

	private IEnumerator DelayedEnd() {
		yield return null;
		runningDialogue = false;
		endDialogue?.Invoke();
	}
}
