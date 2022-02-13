using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolStory {

	[System.Serializable]
	public class LineData {
		public string characterId;
		public string text;
		public List<ItemTag> itemTags = new List<ItemTag>();
		//public List<FlagTag> flagTags = new List<FlagTag>();
		public string eventTrigger = "";
	}

	[System.Serializable]
	public class ChoiceData {
		public Choice[] choices;
	}

	[System.Serializable]
	public class ItemTag {
		public Inventory.InventoryAction action;
		public string itemId;
	}

	[System.Serializable]
	public class FlagTag {
		public string flag;
		public bool value;
	}

	public bool HasStory => currentStory != null;
	public bool HasChoices => currentStory.currentChoices.Count > 0;
	public bool CanContinue => currentStory.canContinue;

	private Story currentStory;


	public CoolStory(TextAsset inkJSON) {
		currentStory = new Story(inkJSON.text);
	}

	public void ShowPickupItem(ItemData item) {
		currentStory.variablesState["pickup"] = item.itemName;
		currentStory.ChoosePathString($"{Area.system}.item_pickup");
	}

	public void SetPath(Area area, string path) {
		currentStory.ChoosePathString($"{area}.{path}");
	}

	public void SetItemFlag(string itemId, bool value) {
		currentStory.variablesState[$"Item_{itemId}"] = value;
	}

	public void SetStoryFlag(string flagId, bool value) {
		currentStory.variablesState[flagId] = value;
	}

	public void SetStoryValue(string flagId, int value) {
		currentStory.variablesState[flagId] = value;
	}

	public LineData GetNextText() {
		LineData line = new LineData();
		string text = currentStory.Continue();
		string[] speakerSplit = text.Split(':');

		if (speakerSplit.Length > 1) {
			if (speakerSplit[0].StartsWith("true") || speakerSplit[0].StartsWith("false")) {
				speakerSplit[0] = speakerSplit[0].Split(' ')[1];
			}
			line.characterId = speakerSplit[0];
			line.text = speakerSplit[1].Trim();
		}
		else {
			line.text = text.Trim();
		}

		List<string> tags = currentStory.currentTags;
		for (int i = 0; i < currentStory.currentTags.Count; i++) {
			Debug.Log("Tag: " + currentStory.currentTags[i]);
			string[] tagSplit = currentStory.currentTags[i].Split(':');
			if (tagSplit.Length != 2) {
				Debug.LogError("Wrong tag format for tag: " + currentStory.currentTags[i]);
				continue;
			}
			if (tagSplit[0].StartsWith("Item")) {
				if (tagSplit[0].EndsWith("get")) {
					line.itemTags.Add(new ItemTag {
						action = Inventory.InventoryAction.GET,
						itemId = tagSplit[1]
					});
				}
				else if (tagSplit[0].EndsWith("remove")) {
					line.itemTags.Add(new ItemTag {
						action = Inventory.InventoryAction.REMOVE,
						itemId = tagSplit[1]
					});
				}
				else if (tagSplit[0].EndsWith("place")) {
					line.itemTags.Add(new ItemTag {
						action = Inventory.InventoryAction.PLACE,
						itemId = tagSplit[1]
					});
				}
				else {
					Debug.LogError("Unknown item tag: " + tagSplit[0]);
				}
			}
			//else if (tagSplit[0].StartsWith("Flag")) {
			//	if (tagSplit[0].EndsWith("set")) {
			//		line.flagTags.Add(new FlagTag {
			//			flag = tagSplit[0],
			//			value = false
			//		});
			//	}
			//	else if (tagSplit[0].EndsWith("clear")) {
			//		line.flagTags.Add(new FlagTag {
			//			flag = tagSplit[1],
			//			value = false
			//		});
			//	}
			//	else {
			//		Debug.LogError("Unknown flag tag: " + tagSplit[0]);
			//	}
			//}
			else if (tagSplit[0].StartsWith("Event")) {
				line.eventTrigger = tagSplit[1];
			}
		}

		return line;
	}

	public ChoiceData GetChoices() {
		return new ChoiceData { choices = currentStory.currentChoices.ToArray() };
	}

	public void MakeChoice(int path) {
		currentStory.ChooseChoiceIndex(path);
	}
}
