using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeGestureHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public ScrollViewSwitcher scrollViewSwitcher;
    public RectTransform scrollViewRect;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float minSwipeDistance = 100f; // Minimum distance to detect a swipe
    private bool isVerticalSwipe;

    public void OnPointerDown(PointerEventData eventData)
    {
        startTouchPosition = eventData.position;
        isVerticalSwipe = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Detect the direction of the drag
        Vector2 delta = eventData.position - startTouchPosition;
        isVerticalSwipe = Mathf.Abs(delta.y) > Mathf.Abs(delta.x);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Not used, but needed for the interface
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(Mathf.Abs(endTouchPosition.y - startTouchPosition.y));
        if (!isVerticalSwipe)
        {
            // Let the ScrollRect handle horizontal scrolling
            return;
        }

        endTouchPosition = eventData.position;

        if (IsSwipeVertical())
        {
            if (endTouchPosition.y > startTouchPosition.y && IsSwipeOutsideScrollView())
            {
                // Swipe up outside scroll view
                scrollViewSwitcher.ToggleScrollView(true);
            }
            else if (endTouchPosition.y < startTouchPosition.y && IsSwipeOutsideScrollView())
            {
                // Swipe down outside scroll view
                scrollViewSwitcher.ToggleScrollView(false);
            }
        }
    }

    private bool IsSwipeVertical()
    {
        return Mathf.Abs(endTouchPosition.y - startTouchPosition.y) > minSwipeDistance;
    }

    private bool IsSwipeOutsideScrollView()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(scrollViewRect, startTouchPosition, null, out localPoint);
        return !scrollViewRect.rect.Contains(localPoint);
    }
}
