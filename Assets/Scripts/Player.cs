﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public CharacterController characterController;
	public Transform playerAvatar;
	public Animator animatorController;
	public float horizontalSpeed = 3f;
	public float verticalSpeed = 3f;
	public float rotateSpeed = 90f;
	public AudioClip footstepSfx;

	private Interactable currentTarget;
	private bool walking;


	private void Update() {
		if (DialogueController.runningDialogue || IngameSettings.inSettings) {
			animatorController.SetFloat("Movespeed", 0f);
			if (walking) {
				walking = false;
				AudioController.instance.StopFootstepSfx();
			}
			return;
		}

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

			if (!walking) {
				walking = true;
				AudioController.instance.PlayFootstepSfx(footstepSfx);
			}
		}
		else {
			if (walking) {
				walking = false;
				AudioController.instance.StopFootstepSfx();
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (currentTarget != null) {
				Vector3 dir = new Vector3(currentTarget.transform.position.x - playerAvatar.position.x, 0f, currentTarget.transform.position.z - playerAvatar.position.z);
				playerAvatar.rotation = Quaternion.LookRotation(dir);
				currentTarget.Interact(playerAvatar);
			}
		}
		else if (Input.GetKeyDown(KeyCode.Escape)) {
			IngameSettings.instance.Show();
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
			animatorController.SetFloat("Movespeed", 0f);
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
