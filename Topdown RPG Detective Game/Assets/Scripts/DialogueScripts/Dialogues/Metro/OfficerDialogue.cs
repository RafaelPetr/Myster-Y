﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerDialogue : GetDialogue
{
    public Dialogue defaultDialogue;
    public Dialogue keyDialogue;

    public override Dialogue DefineDialogue()
    {
        if (PlayerInventory.key)
        {
            return keyDialogue;
        }
        else
        {
            PlayerInventory.key = true;
            return defaultDialogue;
        }
    }


}
