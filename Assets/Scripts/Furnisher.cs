using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnisher : MonoBehaviour {

	public ItemData[] furnishing;
	public Image[] furnishImages;

	private bool canLeave;


	private void Start() {
		canLeave = false;
		Inventory.instance.onFurninshingUpdated += UpdateFurnishing;
	}

	private void OnDestroy() {
		Inventory.instance.onFurninshingUpdated -= UpdateFurnishing;
	}

	private void Update() {
		if (!DialogueController.runningDialogue && canLeave) {
			FindObjectOfType<Zone>().GoToNextZone(Zone.Scene.DemoScene, "CribEntrance");
		}
	}

	private void UpdateFurnishing(List<ItemData> items) {
		for (int i = 0; i < furnishing.Length; i++) {
			furnishImages[i].enabled = items.Contains(furnishing[i]);
		}
	}

	private IEnumerator DelayedDialogue() {
		yield return new WaitForSeconds(1f);
		DialogueController.instance.StartDialogue(Area.crib, "crib_check");
		canLeave = true;
	}
}
