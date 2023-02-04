using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIHelpers
{
    public static void SetSelectableSelected(ISelectable selectable)
    {
        var transform = selectable.transform;
        var index = transform.GetSiblingIndex();
        var parent = transform.parent;
        var count = parent.childCount;
        for (var i = 0; i < count; ++i)
        {
            (parent.GetChild(i) as ISelectable)?.SetSelected(i == index);
        }
    }

    public static void SetGameObjectActive(this GameObject obj, bool value)
    {
        if (obj.activeSelf == value)
            return;
        obj.SetActive(value);
    }
}
