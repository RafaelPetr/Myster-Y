using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag {
    Player,
    Character,
    Interactable,
};

public class CustomTags : MonoBehaviour {
    [SerializeField]private List<Tag> tags = new List<Tag>();

    public bool HasTag(Tag tag) {
        return tags.Contains(tag);
    }
}
