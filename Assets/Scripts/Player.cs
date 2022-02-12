using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public CharacterController characterController;
	public Transform playerAvatar;
	public Animator animatorController;
	public float horizontalSpeed = 3f;
	public float verticalSpeed = 3f;
	public float rotateSpeed = 90f;

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
		animatorController.SetFloat("Movespeed", moveDirection.sqrMagnitude);
		if (moveDirection.sqrMagnitude > 0f) {
			Quaternion forwardRot = Quaternion.LookRotation(moveDirection);
			playerAvatar.rotation = Quaternion.RotateTowards(playerAvatar.rotation, forwardRot, rotateSpeed * 60f * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (currentTarget != null) {
				Vector3 dir = new Vector3(currentTarget.transform.position.x - playerAvatar.position.x, 0f, currentTarget.transform.position.z - playerAvatar.position.z);
				playerAvatar.rotation = Quaternion.LookRotation(dir);
				animatorController.SetFloat("Movespeed", 0f);
				currentTarget.Interact(playerAvatar);
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
		else if (other.tag == "ZoneTrigger") {
			enabled = false;
			other.GetComponent<ZoneTrigger>().GoToNextScene();
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

	public void Spawn(Vector3 position) {
		transform.position = position;
	}
}
