using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable {

	public Canvas surpriseCanvas;


	protected override void Start() {
		base.Start();
		surpriseCanvas.worldCamera = Camera.main;
	}

	public override void Interact() {
		base.Interact();
		DialogueController.startDialogue?.Invoke(area, dialogueId);
	}
}
