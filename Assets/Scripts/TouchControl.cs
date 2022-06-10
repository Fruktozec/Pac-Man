using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Movement move;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 0)
            {
                move.SetDirection(Vector2.right);
            }
            else
            {
                move.SetDirection(Vector2.left);
            }
        }
        else
        {
            if (eventData.delta.y > 0)
            {
                move.SetDirection(Vector2.up);
            }
            else
            {
                move.SetDirection(Vector2.down);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}