﻿using System.Collections;
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
		onInventoryUpdated?.Invoke(currentItems);
		Debug.Log("Gained item: " + itemId);
	}

	public void AddItem(ItemData item) {
		currentItems.Add(item);
		onInventoryUpdated?.Invoke(currentItems);
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

	public void InstallItem(ItemData item) {
		for (int i = 0; i < currentItems.Count; i++) {
			if (currentItems[i].id == item.id) {
				currentItems.RemoveAt(i);
				furniture.Add(item);
				onInventoryUpdated?.Invoke(currentItems);
				onFurninshingUpdated?.Invoke(furniture);
				break;
			}
		}
	}
}
