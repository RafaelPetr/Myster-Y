using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scriptable_character_", menuName = "Dialogue/Character")]
public class DialogueCharacter : ScriptableObject {
    new public string name;
    public Sprite icon;
}
