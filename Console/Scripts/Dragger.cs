using UnityEngine;
using UnityEngine.EventSystems;

namespace Prosto.Console
{
    public class Dragger : UIBehaviour, IBeginDragHandler, IDragHandler
    {
        [Header("Resources")]
        public RectTransform dragArea;
        public RectTransform dragObject;

        [Header("Settings")]
        public bool getParentArea;

        private Vector2 originalLocalPointerPosition;
        private Vector3 originalPanelLocalPosition;

        public new void Start()
        {
            if (dragArea == null)
            {
                if (getParentArea == true) { dragArea = transform.parent.GetComponent<RectTransform>(); }
                else
                {
                    var canvas = (Canvas) GameObject.FindObjectsOfType(typeof(Canvas))[0];
                    dragArea = canvas.GetComponent<RectTransform>();
                }
            }
        }

        private RectTransform dragObjectInternal
        {
            get
            {
                if (dragObject == null) { return (transform as RectTransform); }
                else { return dragObject; }
            }
        }

        private RectTransform dragAreaInternal
        {
            get
            {
                if (dragArea == null)
                {
                    RectTransform canvas = transform as RectTransform;

                    while (canvas.parent != null && canvas.parent is RectTransform)
                    {
                        canvas = canvas.parent as RectTransform;
                    }

                    return canvas;
                }

                else { return dragArea; }
            }
        }

        public void OnBeginDrag(PointerEventData data)
        {
            originalPanelLocalPosition = dragObjectInternal.localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out originalLocalPointerPosition);
        }

        public void OnDrag(PointerEventData data)
        {

            Vector2 localPointerPosition;
           
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out localPointerPosition))
            {
                Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
                dragObjectInternal.localPosition = originalPanelLocalPosition + offsetToOriginal;
            }

            ClampToArea();
        }

        public void ClampToArea()
        {
            Vector3 pos = dragObjectInternal.localPosition;
            Vector3 minPosition = dragAreaInternal.rect.min - dragObjectInternal.rect.min;
            Vector3 maxPosition = dragAreaInternal.rect.max - dragObjectInternal.rect.max;
            pos.x = Mathf.Clamp(dragObjectInternal.localPosition.x, minPosition.x, maxPosition.x);
            pos.y = Mathf.Clamp(dragObjectInternal.localPosition.y, minPosition.y, maxPosition.y);
            dragObjectInternal.localPosition = pos;
        }
    }
}