using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRankingVisual : MonoBehaviour
{
    [SerializeField] private List<GameObject> _stars;

    public void SetStarCount(int stars)
    {
        for (var i = 0; i < _stars.Count; ++i)
        {
            _stars[i].SetActive(stars >= i);
        }
    }
}
