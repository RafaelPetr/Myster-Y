using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LocalizationData {
    public List<LocalizationGroup> groups = new List<LocalizationGroup>();
}

[System.Serializable]
public class LocalizationGroup {
    public string key;
    public List<LocalizationItem> items;

    public LocalizationGroup(string key, List<LocalizationItem> items) {
        this.key = key;
        this.items = items;
    }
}

[System.Serializable]
public class LocalizationItem {
    public string key;
    public string[] value;

    public LocalizationItem(string key, string[] value) {
        this.key = key;
        this.value = value;
    }
}