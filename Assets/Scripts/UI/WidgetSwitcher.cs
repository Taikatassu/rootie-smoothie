using UnityEngine;

public class WidgetSwitcher : MonoBehaviour
{
    public void OnAwake()
    {
        SetActiveIndex(0);
    }

    public void SetActiveIndex(int index)
    {
        for (var i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).gameObject.SetGameObjectActive(i == index);
    }
}
