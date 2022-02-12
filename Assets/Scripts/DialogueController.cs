using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour {

	public static DialogueController instance = null;
	public static bool runningDialogue = false;

	public System.Action startDialogue;
	public System.Action endDialogue;
	public System.Action choiceMade;
	public System.Action<CharacterData, string> onUpdatedText;
	public System.Action<CoolStory.ChoiceData> onUpdatedChoice;

	public CharacterLibrary characterLibrary;
	public TextAsset megaDialogue;

	private CoolStory story;
	private bool waitingForChoice;


	private void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
			story = new CoolStory(megaDialogue);
		}
		else {
			Destroy(gameObject);
		}
	}

	private void Update() {
		if (runningDialogue && !waitingForChoice) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				ContinueDialogue();
			}
		}
	}

	public void StartDialogue(Area area, string path) {
		if (runningDialogue)
			return;

		story.SetPath(area, path);
		StartCoroutine(DelayedStart());
		startDialogue?.Invoke();
	}

	public void StartPickupItem(ItemData item) {
		if (runningDialogue)
			return;

		story.ShowPickupItem(item);
		StartCoroutine(DelayedStart());
		startDialogue?.Invoke();
	}

	public void MakeChoice(int index) {
		story.MakeChoice(index);
		ContinueDialogue();
		StartCoroutine(DelayedChoice());
	}

	private void ContinueDialogue() {
		if (!runningDialogue)
			return;

		if (story.HasChoices) {
			waitingForChoice = true;
			CoolStory.ChoiceData choiceData = story.GetChoices();
			onUpdatedChoice?.Invoke(choiceData);
		}
		else if (story.CanContinue) {
			CoolStory.LineData data = story.GetNextText();
			CharacterData speaker = (!string.IsNullOrEmpty(data.characterId)) ? characterLibrary.GetCharacter(data.characterId) : null;
			onUpdatedText?.Invoke(speaker, data.text);
			HandleItems(data);
			//HandleFlags(data);
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

	#endregion


}
