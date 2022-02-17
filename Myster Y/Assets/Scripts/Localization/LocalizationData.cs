using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LocalizationData {
    public List<LocalizationGroup> groups = new List<LocalizationGroup>();
}

[System.Serializable]
public class LocalizationGroup {
    public string key;
    public List<LocalizationElement> elements;

    public LocalizationGroup(string key, List<LocalizationElement> elements) {
        this.key = key;
        this.elements = elements;
    }
}

[System.Serializable]
public class LocalizationElement {
    public string key;
    public string[] values;

    public LocalizationElement(string key, string[] values) {
        this.key = key;
        this.values = values;
    }
}