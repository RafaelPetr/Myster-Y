using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionButton : ButtonMenu {
    public int choiceNumber;

    public void SendOption() {
        DialogueManager.instance.PickOption(choiceNumber);
    }
}