using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag {
    Player,
    NPC,
    Interactable,
};

public class CustomTags : MonoBehaviour {
    [SerializeField]private List<Tag> tags = new List<Tag>();

    public void AddTag(Tag tag) {
        tags.Add(tag);
    }

    public bool HasTag(Tag tag) {
        return tags.Contains(tag);
    }
}
