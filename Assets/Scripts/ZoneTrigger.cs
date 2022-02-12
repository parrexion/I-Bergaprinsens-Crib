using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour {

	public Zone.Scene nextScene;
	public string nextArea;


	public void GoToNextScene() {
		FindObjectOfType<Zone>().GoToNextZone(nextScene, nextArea);
	}
}
