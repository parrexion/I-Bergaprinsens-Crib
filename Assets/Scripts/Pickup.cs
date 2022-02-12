using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable {

	public static Dictionary<string, bool> pickupDictionary = new Dictionary<string, bool>();

	public string pickupId;
	public ItemData pickupItem;

	protected override void Start() {
		if (pickupDictionary.ContainsKey(pickupId)) {
			Destroy(gameObject);
		}
		else {
			base.Start();
		}
	}

	public override void Interact() {
		base.Interact();

		DialogueController.instance.endDialogue += Cleanup;
		DialogueController.instance.StartPickupItem(pickupItem);
	}

	private void Cleanup() {
		DialogueController.instance.endDialogue -= Cleanup;
		pickupDictionary[pickupId] = true;
		Inventory.instance.AddItem(pickupItem);
		DialogueController.instance.AddItem(pickupItem);
		Destroy(gameObject);
	}
}
