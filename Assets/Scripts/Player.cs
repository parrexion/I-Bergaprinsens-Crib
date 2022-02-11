using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public CharacterController characterController;
	public float horizontalSpeed = 3f;
	public float verticalSpeed = 3f;

	private Interactable currentTarget;


	private void Update() {
		if (DialogueController.runningDialogue)
			return;

		//Movement
		Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		if (moveDirection.sqrMagnitude > 1f)
			moveDirection /= moveDirection.magnitude;
		moveDirection.x *= horizontalSpeed;
		moveDirection.z *= verticalSpeed;
		characterController.SimpleMove(moveDirection);

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (currentTarget != null) {
				currentTarget.Interact();
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Interactable") {
			if (currentTarget != null) {
				currentTarget.PlayerLeave();
			}
			Interactable interact = other.GetComponent<Interactable>();
			interact.PlayerEnter();
			currentTarget = interact;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Interactable") {
			Interactable interact = other.GetComponent<Interactable>();
			interact.PlayerLeave();
			if (interact == currentTarget)
				currentTarget = null;
		}
	}
}
