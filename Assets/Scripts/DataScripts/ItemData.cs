using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject {

	public string id;
	public string itemName;
	public Sprite itemIcon;
	public Color standInColor = Color.white;
	public bool isFurniture;
}
