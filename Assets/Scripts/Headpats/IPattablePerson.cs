using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootieSmoothie.Headpats
{
    public interface IPattablePerson
    {
        void OnPatStart();
        void OnPatRelease();
    }
}