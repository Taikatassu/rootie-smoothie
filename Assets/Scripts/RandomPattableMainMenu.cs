using System;
using System.Collections;
using System.Collections.Generic;
using RootieSmoothie;
using UnityEngine;

public class RandomPattableMainMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pattables;

    public void Start()
    {
        SpawnPattable();
    }

    public void SpawnPattable()
    {
        var count = transform.childCount;
        for (var i = count - 1; i >= 0; --i)
        {
            var child = transform.GetChild(i);
            Destroy(child.gameObject);
        }

        if (_pattables.Count <= 0)
            return;

        var rndIndex = (int)RandomHelper.GetRandomRange(0, _pattables.Count);
        var go = Instantiate(_pattables[rndIndex]);
        var t = go.transform;
        t.SetParent(transform);
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }
}
