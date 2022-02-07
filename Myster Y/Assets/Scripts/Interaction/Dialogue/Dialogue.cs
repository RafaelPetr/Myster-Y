using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum elementTypes {Sentence,Choice};

[CreateAssetMenu(menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject {
    public string key;

    public List<DialogueSentence> sentences = new List<DialogueSentence>();
    public DialogueChoice choice;

    //public List<int> elementsOrder = new List<int>();
    //public string[] elementTypes = new string[]{"Sentence","Choice"};
}
