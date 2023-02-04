using System.Collections;
using System.Collections.Generic;
using RootieSmoothie.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace RootieSmoothie.Headpats
{
    public class HeadpatsKaren : Singleton<HeadpatsKaren>
    {
        [SerializeField] private Texture2D _cursorTexture;
        [SerializeField] private Texture2D _cursorClickTexture;
        [SerializeField] private Vector2 _cursorPivot;

        public void DemandHeadpats()
        {
            CursorManager.Instance.SetCursorSprites(_cursorTexture, _cursorClickTexture, _cursorPivot);
        }

        public void StopDemanding()
        {
            CursorManager.Instance.ResetCursorToDefault();
        }
    }
}
