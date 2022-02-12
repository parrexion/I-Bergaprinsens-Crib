using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour {

	public static System.Action<Area, string> startDialogue;
	public static System.Action choiceMade;
	public static System.Action endDialogue;
	public static bool runningDialogue = false;

	public CharacterLibrary characterLibrary;
	public GameObject dialogueView;
	public TextAsset megaDialogue;

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

	[Header("Choice side")]
	public GameObject choiceView;
	public GameObject[] choices;
	public TextMeshProUGUI[] choiceTexts;

	private CoolStory story;
	private bool waitingForChoice;


	private void Awake() {
		startDialogue += OnStartDialogue2;
		story = new CoolStory(megaDialogue);
		dialogueView.SetActive(false);
	}

	private void OnDestroy() {
		startDialogue -= OnStartDialogue2;
	}

	private void Update() {
		if (runningDialogue && !waitingForChoice) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				ContinueDialogue();
			}
		}
	}

	public void MakeChoice(int index) {
		story.MakeChoice(index);
		ContinueDialogue();
		StartCoroutine(DelayedChoice());
	}

	private void OnStartDialogue2(Area area, string path) {
		if (runningDialogue)
			return;

		story.SetPath(area, path);
		StartCoroutine(DelayedStart());
		ShowUI();
	}

	private void ContinueDialogue() {
		if (!runningDialogue)
			return;

		if (story.HasChoices) {
			waitingForChoice = true;
			CoolStory.ChoiceData choiceData = story.GetChoices();
			DisplayChoices(choiceData);
		}
		else if (story.CanContinue) {
			CoolStory.LineData data = story.GetNextText();
			CharacterData speaker = (!string.IsNullOrEmpty(data.characterId)) ? characterLibrary.GetCharacter(data.characterId) : null;
			DisplayLine(speaker, data.text);
			HandleItems(data);
			HandleFlags(data);
		}
		else {
			FinishDialogue();
		}
	}

	private void HandleItems(CoolStory.LineData data) {
		for (int i = 0; i < data.itemTags.Count; i++) {
			if (data.itemTags[i].action == Inventory.InventoryAction.GET) {
				Inventory.instance.AddItem(data.itemTags[i].itemId);
				story.SetItemFlag(data.itemTags[i].itemId, true);
			}
			else if (data.itemTags[i].action == Inventory.InventoryAction.REMOVE) {
				Inventory.instance.RemoveItem(data.itemTags[i].itemId);
				story.SetItemFlag(data.itemTags[i].itemId, false);
			}
		}
	}

	private void HandleFlags(CoolStory.LineData data) {
		for (int i = 0; i < data.flagTags.Count; i++) {
			story.SetStoryFlag(data.flagTags[i].flag, data.flagTags[i].value);
		}
	}

	private void FinishDialogue() {
		dialogueView.SetActive(false);
		StartCoroutine(DelayedEnd());
	}



	#region Delays

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

	private IEnumerator DelayedChoice() {
		yield return null;
		waitingForChoice = false;
		choiceMade?.Invoke();
	}

	private IEnumerator UpdateTarget() {
		EventSystem.current.SetSelectedGameObject(null);
		yield return null;
		EventSystem.current.SetSelectedGameObject(choices[0]);
	}

	#endregion


	#region UI

	private void ShowUI() {
		leftView.SetActive(false);
		rightView.SetActive(false);
		choiceView.SetActive(false);
		dialogueView.SetActive(true);
	}

	private void DisplayLine(CharacterData speaker, string line) {
		if (speaker == null) {
			speakerNameLeft.text = "";
			portraitLeft.sprite = null;
			portraitLeft.color = Color.white;
			dialogueTextLeft.text = line;
			leftView.SetActive(true);
			rightView.SetActive(false);
			choiceView.SetActive(false);
		}
		else {
			if (speaker.rightSided) {
				speakerNameRight.text = speaker.characterName;
				portraitRight.sprite = speaker.portrait;
				portraitRight.color = speaker.standInColor;
				dialogueTextRight.text = line;
				leftView.SetActive(false);
				rightView.SetActive(true);
				choiceView.SetActive(false);
			}
			else {
				speakerNameLeft.text = speaker.characterName;
				portraitLeft.sprite = speaker.portrait;
				portraitLeft.color = speaker.standInColor;
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

	#endregion

}
