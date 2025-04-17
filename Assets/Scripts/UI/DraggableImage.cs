using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ImageType type = ImageType.None;

    private Vector3 initialPosition = Vector3.zero;
    private RectTransform rectTransform = null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        initialPosition = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, Camera.main, out Vector3 worldPos))
        {
            rectTransform.position = worldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggableSlot[] slots = GameObject.FindObjectsOfType<DraggableSlot>();

        float closestDistance = float.MaxValue;
        DraggableSlot closestSlot = null;

        foreach (DraggableSlot slot in slots)
        {
            float distance = Vector3.Distance(rectTransform.position, slot.transform.position);
            if (distance < closestDistance && distance < 0.7f) // 100 pixel threshold
            {
                closestDistance = distance;
                closestSlot = slot;
            }
        }

        rectTransform.position = initialPosition;
        if (closestSlot != null)
            closestSlot.SetState(type);
        
        rectTransform.position = initialPosition;
    }
}

public enum ImageType
{
    None = 0,
    True = 1,
    False = 2,
}