using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

	public Image[] slots;


	private void Start() {
		Inventory.instance.onInventoryUpdated += UpdateInventory;
		UpdateInventory(new List<ItemData>());
	}

	private void OnDestroy() {
		Inventory.instance.onInventoryUpdated -= UpdateInventory;
	}

	private void UpdateInventory(List<ItemData> items) {
		for (int i = 0; i < slots.Length; i++) {
			if (i >= items.Count) {
				slots[i].enabled = false;
			}
			else {
				slots[i].sprite = items[i].itemIcon;
				slots[i].color = items[i].standInColor;
				slots[i].enabled = true;
			}
		}
	}
}
