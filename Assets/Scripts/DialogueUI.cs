using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour {

	public GameObject dialogueView;

	[Header("Left side")]
	public GameObject leftView;
	public Image portraitLeft;
	public Image portraitBackgroundLeft;
	public TextMeshProUGUI speakerNameLeft;
	public TextMeshProUGUI dialogueTextLeft;

	[Header("Right side")]
	public GameObject rightView;
	public Image portraitRight;
	public Image portraitBackgroundRight;
	public TextMeshProUGUI speakerNameRight;
	public TextMeshProUGUI dialogueTextRight;

	[Header("Choice side")]
	public GameObject choiceView;
	public GameObject[] choices;
	public TextMeshProUGUI[] choiceTexts;


	private void Start() {
		DialogueController.instance.startDialogue += ShowUI;
		DialogueController.instance.endDialogue += HideUI;
		DialogueController.instance.onUpdatedText += DisplayLine;
		DialogueController.instance.onUpdatedChoice += DisplayChoices;
		HideUI();
	}

	private void OnDestroy() {
		DialogueController.instance.startDialogue -= ShowUI;
		DialogueController.instance.endDialogue -= HideUI;
		DialogueController.instance.onUpdatedText -= DisplayLine;
		DialogueController.instance.onUpdatedChoice -= DisplayChoices;
	}

	public void MakeChoice(int index) {
		DialogueController.instance.MakeChoice(index);
	}


	#region UI

	private void ShowUI() {
		leftView.SetActive(false);
		rightView.SetActive(false);
		choiceView.SetActive(false);
		dialogueView.SetActive(true);
	}

	private void HideUI() {
		dialogueView.SetActive(false);
	}

	private void DisplayLine(CharacterData speaker, string line) {
		if (speaker == null) {
			speakerNameLeft.text = "";
			portraitLeft.sprite = null;
			portraitBackgroundLeft.color = Color.white;
			dialogueTextLeft.text = line;
			leftView.SetActive(true);
			rightView.SetActive(false);
			choiceView.SetActive(false);
		}
		else {
			if (speaker.rightSided) {
				speakerNameRight.text = speaker.characterName;
				portraitRight.sprite = speaker.portrait;
				portraitBackgroundRight.color = speaker.standInColor;
				dialogueTextRight.text = line;
				leftView.SetActive(false);
				rightView.SetActive(true);
				choiceView.SetActive(false);
			}
			else {
				speakerNameLeft.text = speaker.characterName;
				portraitLeft.sprite = speaker.portrait;
				portraitBackgroundLeft.color = speaker.standInColor;
				dialogueTextLeft.text = line;
				leftView.SetActive(true);
				rightView.SetActive(false);
				choiceView.SetActive(false);
			}
		}
	}

	private void DisplayChoices(CoolStory.ChoiceData choiceData) {
		for (int i = 0; i < choices.Length; i++) {
			if (i >= choiceData.choices.Length) {
				choices[i].SetActive(false);
			}
			else {
				choiceTexts[i].text = choiceData.choices[i].text;
				choices[i].SetActive(true);
			}
		}
		leftView.SetActive(false);
		rightView.SetActive(false);
		choiceView.SetActive(true);
		StartCoroutine(UpdateTarget());
	}

	private IEnumerator UpdateTarget() {
		EventSystem.current.SetSelectedGameObject(null);
		yield return null;
		EventSystem.current.SetSelectedGameObject(choices[0]);
	}

	#endregion
}
