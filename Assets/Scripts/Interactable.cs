using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public GameObject interactionMarker;
	public TextAsset inkJSON;


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
