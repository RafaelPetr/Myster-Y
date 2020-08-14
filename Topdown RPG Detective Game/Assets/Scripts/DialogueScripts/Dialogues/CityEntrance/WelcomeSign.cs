using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeSign : DialogueSource
{
    public Dialogue defaultDialogue;
    public override Dialogue DefineDialogue()
    {
        return defaultDialogue;
    }


}
