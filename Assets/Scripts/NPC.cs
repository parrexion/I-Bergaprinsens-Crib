using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable {
	
	public Area area = Area.example_world;
	public string dialogueId;
	public Transform avatar;


	public override void Interact(Transform player) {
		Vector3 dir = new Vector3(player.transform.position.x-transform.position.x, 0f, player.transform.position.z-transform.position.z);
		avatar.rotation = Quaternion.LookRotation(dir);
		DialogueController.instance.StartDialogue(area, dialogueId);
	}
}
