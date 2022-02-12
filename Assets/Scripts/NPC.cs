using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable {
	
	public Area area = Area.example_world;
	public string dialogueId;


	public override void Interact() {
		base.Interact();
		DialogueController.instance.StartDialogue(area, dialogueId);
	}
}
