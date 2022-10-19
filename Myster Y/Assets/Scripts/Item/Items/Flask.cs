using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scriptable_item_flask", menuName = "Item/Flask")]
public class Flask : Item {
    public List<int> mixture = new List<int>();
}
