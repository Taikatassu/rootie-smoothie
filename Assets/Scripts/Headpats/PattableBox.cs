using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace RootieSmoothie.Headpats
{
    [RequireComponent(typeof(Image))]
    public class PattableBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler, ISelectable
    {
        private IPattablePerson _pattablePerson;
        private bool _isHovered;
        
        public void OnValidate()
        {
            _pattablePerson = GetComponentInParent<IPattablePerson>();
        }

        public void Awake()
        {
            _pattablePerson = GetComponentInParent<IPattablePerson>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovered = true;
            HeadpatsKaren.Instance.DemandHeadpats();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
            HeadpatsKaren.Instance.StopDemanding();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isHovered)
            {
                _pattablePerson?.OnPatStart();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pattablePerson?.OnPatRelease();
        }

        public void SetSelected(bool value)
        {
            
        }
    }
}
