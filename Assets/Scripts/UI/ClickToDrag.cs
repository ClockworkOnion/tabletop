using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject toDrag;
    public Canvas canvas;

    private Vector2 offset = new Vector2(0,0);

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            eventData.position,
            canvas.worldCamera,
            out position);

        toDrag.transform.position = canvas.transform.TransformPoint(position) + (Vector3)offset;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset =  toDrag.transform.position - Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
