using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterLibrary : ScriptableObject {

	public CharacterData[] characters;


	public CharacterData GetCharacter(string id) {
		for (int i = 0; i < characters.Length; i++) {
			if (characters[i].id == id)
				return characters[i];
		}

		Debug.LogError("Could not find character with id: " + id);
		return null;
	}
}
