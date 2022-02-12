using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnisher : MonoBehaviour {

	public ItemData[] furnishing;
	public Image[] furnishImages;


	private void Start() {
		Inventory.instance.onFurninshingUpdated += UpdateFurnishing;
	}

	private void OnDestroy() {
		Inventory.instance.onFurninshingUpdated -= UpdateFurnishing;
	}

	private void UpdateFurnishing(List<ItemData> items) {
		for (int i = 0; i < furnishing.Length; i++) {
			furnishImages[i].enabled = items.Contains(furnishing[i]);
		}
	}
}
