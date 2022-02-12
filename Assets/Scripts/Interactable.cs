using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Area { example_world }

public class Interactable : MonoBehaviour {

	public GameObject interactionMarker;
	public Area area = Area.example_world;
	public string dialogueId;


	protected virtual void Start() {
		PlayerLeave();
		interactionMarker.transform.rotation = Quaternion.identity;
	}

	public virtual void PlayerEnter() {
		interactionMarker.SetActive(true);
	}

	public virtual void PlayerLeave() {
		interactionMarker.SetActive(false);
	}

	public virtual void Interact() { }
}
