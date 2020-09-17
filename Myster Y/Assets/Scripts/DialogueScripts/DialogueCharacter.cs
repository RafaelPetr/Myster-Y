using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Objects/Dialogue Character")]
public class DialogueCharacter : ScriptableObject {
    public string name;
    public Sprite icon;
    public AudioSource characterVoice;
}
