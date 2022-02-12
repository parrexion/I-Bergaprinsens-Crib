using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zone : MonoBehaviour {

	public enum Scene { DemoScene, DemoScene2, TheCrib }

	public static string nextSpawn = "Test";
	public static System.Action onSpawnComplete;
	public static System.Action onSpawnStarting;


	[System.Serializable]
	public class SpawnPoint {
		public string id;
		public Transform spawnPoint;
	}

	public AudioClip zoneMusic;
	public SpawnPoint[] spawnPoints = new SpawnPoint[0];


	private void Start() {
		AudioController.instance.PlayMusic(zoneMusic);

		if (!string.IsNullOrEmpty(nextSpawn)) {
			StartCoroutine(DelayedSpawn());
		}
	}

	private IEnumerator DelayedSpawn() {
		yield return null;

		if (!string.IsNullOrEmpty(nextSpawn)) {
			for (int i = 0; i < spawnPoints.Length; i++) {
				if (spawnPoints[i].id == nextSpawn) {
					FindObjectOfType<Player>().Spawn(spawnPoints[i].spawnPoint.position);
					break;
				}
			}
		}
		onSpawnComplete?.Invoke();
	}

	public void GoToNextZone(Scene scene, string zone) {
		onSpawnStarting?.Invoke();
		nextSpawn = zone;
		StartCoroutine(DelayedNextZone(scene));
	}

	private IEnumerator DelayedNextZone(Scene scene) {
		yield return new WaitForSeconds(FadeIn.FADE_OUT_SPEED);
		SceneManager.LoadScene(scene.ToString());

	}
}
