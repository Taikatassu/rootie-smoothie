using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Core.Blending;
using UnityEngine;
using UnityEngine.UI;

namespace RootieSmoothie.View.Blending
{
    public class BlenderView : MonoBehaviour
    {
        [SerializeField]
        private Image _smoothieImage = null;

        private Blender _blender;

        public void Initialize(Blender blender)
        {
            blender.ThrowIfNullArgument(nameof(blender));

            _blender = blender;
            _blender.OnSmoothieUpdated += OnSmoothieUpdated;
        }

        private void OnSmoothieUpdated(Color newSmoothieColor)
        {
            _smoothieImage.color = newSmoothieColor;
        }
    }
}
