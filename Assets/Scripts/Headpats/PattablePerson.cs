using System.Collections;
using System.Collections.Generic;
using RootieSmoothie.Headpats;
using UnityEngine;

public class PattablePerson : MonoBehaviour, IPattablePerson
{
    [SerializeField]
    private Transform _visualRoot;

    public void OnPatStart()
    {
        _visualRoot.localScale = new Vector3(1f, 0.95f, 1f);
    }

    public void OnPatRelease()
    {
        _visualRoot.localScale = new Vector3(1f, 1f, 1f);
    }
}
