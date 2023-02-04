using UnityEngine;

public interface ISelectable
{
    void SetSelected(bool value);
    GameObject gameObject { get; }
    Transform transform { get; }
}
