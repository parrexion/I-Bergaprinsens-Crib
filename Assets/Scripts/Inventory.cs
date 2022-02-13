using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public static Inventory instance = null;
	private void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
			Setup();
		}
		else {
			Destroy(gameObject);
		}
	}

	public enum InventoryAction { GET, REMOVE, PLACE };

	public System.Action<List<ItemData>> onInventoryUpdated;
	public System.Action<List<ItemData>> onFurninshingUpdated;

	public ItemLibrary itemLibrary;
	public AudioClip[] gainItemSfx;

	public int PlacedFurnitures => furniture.Count;

	private List<ItemData> currentItems = new List<ItemData>();
	private List<ItemData> furniture = new List<ItemData>();


	private void Setup() {
		Zone.onSpawnComplete += RefreshContent;
	}

	private void OnDestroy() {
		Zone.onSpawnComplete -= RefreshContent;
	}

	private void RefreshContent() {
		onInventoryUpdated?.Invoke(currentItems);
		onFurninshingUpdated?.Invoke(furniture);
	}

	public bool HasItem(string itemId) {
		for (int i = 0; i < currentItems.Count; i++) {
			if (currentItems[i].id == itemId)
				return true;
		}
		return false;
	}

	public void AddItem(string itemId) {
		ItemData item = itemLibrary.GetItem(itemId);
		currentItems.Add(item);
		currentItems.Sort(SortItems);
		onInventoryUpdated?.Invoke(currentItems);
		Debug.Log("Gained item: " + itemId);
	}

	public void AddItem(ItemData item) {
		currentItems.Add(item);
		currentItems.Sort(SortItems);
		onInventoryUpdated?.Invoke(currentItems);
		AudioController.instance.PlaySfx(gainItemSfx[Random.Range(0, gainItemSfx.Length)]);
		Debug.Log("Gained item: " + item.id);
	}

	public void RemoveItem(string itemId) {
		for (int i = 0; i < currentItems.Count; i++) {
			if (currentItems[i].id == itemId) {
				currentItems.RemoveAt(i);
				onInventoryUpdated?.Invoke(currentItems);
				Debug.Log("Removed item: " + itemId);
				return;
			}
		}
	}

	public void PlaceItem(string itemId) {
		for (int i = 0; i < currentItems.Count; i++) {
			if (currentItems[i].id == itemId) {
				furniture.Add(currentItems[i]);
				currentItems.RemoveAt(i);
				AudioController.instance.PlaySfx(gainItemSfx[Random.Range(0, gainItemSfx.Length)]);
				onInventoryUpdated?.Invoke(currentItems);
				onFurninshingUpdated?.Invoke(furniture);
				break;
			}
		}
	}

	private int SortItems(ItemData x, ItemData y) {
		if (x.isFurniture == y.isFurniture)
			return -1;
		if (x.isFurniture)
			return 1;
		return -1;
	}
}
