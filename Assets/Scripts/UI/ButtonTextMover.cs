using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootieSmoothie.UI
{
    public class ButtonTextMover : MonoBehaviour
    {
        public void MoveTextUp()
        {
            transform.localScale = Vector3.one;
        }

        public void MoveTextDown()
        {
            transform.localScale = new Vector3(1, 0.95f, 1);
        }
        
    }
}
