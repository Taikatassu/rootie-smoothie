using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RootieSmoothie.Character
{
    [RequireComponent(typeof(Image))]
    public class AlignRectTransformPivotToSprite : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        public void OnValidate()
        {
            _image = GetComponent<Image>();
            if (_image.sprite == null)
                return;
            (transform as RectTransform).pivot = _image.sprite.pivot / _image.sprite.textureRect.size;
        }
    }
}
