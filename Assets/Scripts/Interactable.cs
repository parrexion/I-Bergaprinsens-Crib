using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Area { system, example_world, area1, area2, area3 }

public class Interactable : MonoBehaviour {

	public GameObject interactionMarker;
	public Canvas surpriseCanvas;


	protected virtual void Start() {
		PlayerLeave();
		interactionMarker.transform.rotation = Quaternion.identity;
		surpriseCanvas.worldCamera = Camera.main;
	}

	public virtual void PlayerEnter() {
		interactionMarker.SetActive(true);
	}

	public virtual void PlayerLeave() {
		interactionMarker.SetActive(false);
	}

	public virtual void Interact() { }
}
