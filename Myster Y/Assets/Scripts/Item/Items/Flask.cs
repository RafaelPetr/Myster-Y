using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scriptable_item_flask", menuName = "Item/Flask")]
public class Flask : Item {
    private List<int> mixture = new List<int>();

    public void AddMixture(int mixtureNumber) {
        mixture.Add(mixtureNumber);

        if (mixture.Count == 3) {
            for (int i = 0; i < mixture.Count; i++) {
                for (int j = 0; j < mixture.Count - i - 1; j++) {
                    if (mixture[j] > mixture[j+1]) {
                        int aux = mixture[j];
                        mixture[j] = mixture[j+1];
                        mixture[j+1] = aux;
                    }
                }
            }
        }
    }

    public void ResetMixture() {
        mixture.Clear();
    }

    public List<int> GetMixture() {
        return mixture;
    }

    public int GetLiquid(int i) {
        return mixture[i];
    }

    public int GetCounter() {
        return mixture.Count;
    }
}