using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemLibrary : ScriptableObject {

	public ItemData[] items;


	public ItemData GetItem(string id) {
		for (int i = 0; i < items.Length; i++) {
			if (items[i].id == id)
				return items[i];
		}

		Debug.LogError("Could not find item with id: " + id);
		return null;
	}
}
