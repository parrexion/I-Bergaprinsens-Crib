using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Furnisher : MonoBehaviour {

	public static bool startedGame = true;

	public FadeIn fadeIn;
	public Image background;
	public Sprite cribSprite;
	public ItemData[] furnishing;
	public Image[] furnishImages;
	public GameObject hat;
	public GameObject creditsView;
	public GameObject peopleView;
	public AudioClip creditsSong;

	private bool canLeave;
	private bool clearedGame;
	private string nextEvent;


	private void Start() {
		canLeave = false;
		hat.SetActive(false);
		creditsView.SetActive(false);
		DialogueController.instance.eventTriggered += OnDialogueTrigger;
		DialogueController.instance.endDialogue += OnEndDialogue;
		Inventory.instance.onFurninshingUpdated += UpdateFurnishing;

		if (startedGame) {
			startedGame = false;
			nextEvent = "startgame";
			background.sprite = null;
			background.color = Color.black;
			StartCoroutine(DelayedStartDialogue());
		}
		else {
			background.sprite = cribSprite;
			background.color = Color.white;
			StartCoroutine(DelayedDialogue());
		}
	}

	private void OnDestroy() {
		DialogueController.instance.eventTriggered -= OnDialogueTrigger;
		DialogueController.instance.endDialogue -= OnEndDialogue;
		Inventory.instance.onFurninshingUpdated -= UpdateFurnishing;
	}

	private void Update() {
		if (!DialogueController.runningDialogue && canLeave && Input.GetKeyDown(KeyCode.Space)) {
			FindObjectOfType<Zone>().GoToNextZone(Zone.Scene.Area1Field, "CribEntrance");
		}
		if (clearedGame && Input.GetKeyDown(KeyCode.Space)) {
			FindObjectOfType<Zone>().GoToNextZone(Zone.Scene.MainMenuScene, "Main");
		}
	}

	private void UpdateFurnishing(List<ItemData> items) {
		for (int i = 0; i < furnishing.Length; i++) {
			furnishImages[i].enabled = items.Contains(furnishing[i]);
		}
	}

	private IEnumerator DelayedDialogue() {
		yield return new WaitForSeconds(1f);
		DialogueController.instance.StartDialogue(Area.crib, "crib_item_check");
	}

	private IEnumerator DelayedStartDialogue() {
		yield return new WaitForSeconds(1f);
		DialogueController.instance.StartDialogue(Area.crib, "crib_startgame");
	}

	private void OnDialogueTrigger(string trigger) {
		if (trigger == "start" || trigger == "endgame" || trigger == "credits") {
			canLeave = false;
			nextEvent = trigger;
		}
	}

	private void OnEndDialogue() {
		if (nextEvent == "startgame") {
			StartCoroutine(StartGameAnimation());
		}
		else if (nextEvent == "endgame") {
			StartCoroutine(EndGameAnimation());
		}
		else if (nextEvent == "credits") {
			StartCoroutine(RollCredits());
		}
		else {
			canLeave = true;
		}
		nextEvent = "";
	}

	private IEnumerator StartGameAnimation() {
		fadeIn.FadeFromBlack();
		background.sprite = cribSprite;
		background.color = Color.white;
		yield return new WaitForSeconds(FadeIn.FADE_OUT_SPEED + 1f);
		DialogueController.instance.StartDialogue(Area.crib, "crib_startgame2");
	}

	private IEnumerator EndGameAnimation() {
		fadeIn.speedMultiplier = 0.3f;
		fadeIn.FadeToBlack();
		yield return new WaitForSeconds(FadeIn.FADE_OUT_SPEED / 0.3f);
		for (int i = 0; i < furnishing.Length; i++) {
			furnishImages[i].enabled = false;
		}
		hat.SetActive(true);
		fadeIn.FadeFromBlack();
		yield return new WaitForSeconds(FadeIn.FADE_OUT_SPEED / 0.3f + 1f);

		DialogueController.instance.StartDialogue(Area.crib, "crib_endgame");
	}

	private IEnumerator RollCredits() {
		AudioController.instance.PlayMusic(creditsSong);
		fadeIn.speedMultiplier = 0.3f;
		fadeIn.FadeToBlack();
		yield return new WaitForSeconds(FadeIn.FADE_OUT_SPEED / 0.3f);
		peopleView.SetActive(false);
		creditsView.SetActive(true);
		fadeIn.FadeFromBlack();
		yield return new WaitForSeconds(FadeIn.FADE_OUT_SPEED / 0.3f);
		peopleView.SetActive(true);
		clearedGame = true;
		fadeIn.speedMultiplier = 1f;
	}
}
