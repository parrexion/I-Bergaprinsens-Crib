using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolStory {

	[System.Serializable]
	public class LineData {
		public string characterId;
		public string text;
	}

	public bool HasStory => currentStory != null;
	public bool CanContinue => currentStory.canContinue;

	private Story currentStory;


	public CoolStory(TextAsset inkJSON) {
		currentStory = new Story(inkJSON.text);
	}

	public LineData GetNextText() {
		string text = currentStory.Continue();
		string[] split = text.Split(':');

		if (split.Length > 1) {
			return new LineData {
				characterId = split[0],
				text = split[1]
			};
		}
		else {
			return new LineData {
				characterId = "",
				text = text
			};
		}
	}
}
