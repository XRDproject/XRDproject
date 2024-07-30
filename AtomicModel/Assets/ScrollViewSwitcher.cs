using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween namespace
using System.Collections;
using UnityEngine.SceneManagement;

public class ScrollViewSwitcher : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Button switchButton;
    public RectTransform buttonRect;
    public Image buttonImage;
    public RectTransform contentRect;
    public RectTransform viewportRect;
    public int columnsInVerticalLayout = 4; // Fixed number of columns in vertical layout
    public int itemsPerRow = 1; // Number of items per row in horizontal layout
    public float animationDuration = 0.5f; // Duration of the animation
    public float expandedHeight = 600; // The target height for the expanded scroll view
    public ScaleManager scaleManager;
    public Vector2 expandedButtonScale = new Vector2(1.5f, 1.5f); // Scale of the button when expanded

    private bool isVertical = false;
    private GridLayoutGroup gridLayoutGroup;
    private Vector2 originalSize;
    private Vector2 originalPosition;
    private Vector2 buttonOriginalPosition;
    private Vector2 buttonOriginalScale;

    public bool IsVertical
    {
        get { return isVertical; }
        private set { isVertical = value; }
    }

    void Start()
    {
        if (switchButton != null)
        {
            switchButton.onClick.AddListener(() => ToggleScrollView(!isVertical));
            Debug.Log("Button listener added.");
        }
        else
        {
            Debug.LogError("Switch button is not assigned.");
        }

        gridLayoutGroup = contentRect.GetComponent<GridLayoutGroup>();
        if (gridLayoutGroup == null)
        {
            Debug.LogError("GridLayoutGroup component is missing on Content.");
        }
        else
        {
            // Initialize the GridLayoutGroup with default horizontal settings
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            gridLayoutGroup.constraintCount = itemsPerRow;
        }

        // Save original size and position of the ScrollRect
        originalSize = scrollRect.GetComponent<RectTransform>().sizeDelta;
        originalPosition = scrollRect.GetComponent<RectTransform>().anchoredPosition;
        buttonOriginalPosition = buttonRect.anchoredPosition;
        buttonOriginalScale = buttonRect.localScale;
    }

    public void ToggleScrollView(bool toVertical)
    {

        if (scrollRect == null || contentRect == null || gridLayoutGroup == null)
        {
            Debug.LogError("ScrollRect, Content or GridLayoutGroup is not assigned.");
            return;
        }

        isVertical = toVertical;

        AnimateLayoutSwitch();
    }

    void AnimateLayoutSwitch()
    {
        Vector2 targetCellSize = isVertical ? new Vector2(60, 60) : new Vector2(70, 70);
        Vector2 targetSpacing = isVertical ? new Vector2(10, 10) : new Vector2(7, 7);

        // Animate cellSize and spacing
        DOTween.To(() => gridLayoutGroup.cellSize, x => gridLayoutGroup.cellSize = x, targetCellSize, animationDuration);
        DOTween.To(() => gridLayoutGroup.spacing, x => gridLayoutGroup.spacing = x, targetSpacing, animationDuration).OnComplete(() =>
        {
            if (isVertical)
            {
                SwitchToVerticalLayout();
            }
            else
            {
                SwitchToHorizontalLayout();
            }

            // Smooth transition and handle scrollbar appearance
            StartCoroutine(SmoothTransitionAfterLayoutChange());
        });
    }

    void SwitchToVerticalLayout()
    {
        if (scrollRect.horizontalScrollbar != null)
        {
            scrollRect.horizontalScrollbar.gameObject.SetActive(false);
        }

        scaleManager.SetScaleSlider(false); //Hide slider in vertical view

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columnsInVerticalLayout;

        scrollRect.horizontal = false;
        scrollRect.vertical = true;

        UpdateContentSizeForVertical();
    }

    void SwitchToHorizontalLayout()
    {
        // Set up horizontal layout but keep scrollbar hidden initially
        if (scrollRect.horizontalScrollbar != null)
        {
            scrollRect.horizontalScrollbar.gameObject.SetActive(false);
        }

        scaleManager.SetScaleSlider(true); //Show slider 

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutGroup.constraintCount = itemsPerRow;

        scrollRect.horizontal = true;
        scrollRect.vertical = false;

        UpdateContentSizeForHorizontal();
    }


    void UpdateContentSizeForVertical()
    {
        //float itemHeight = contentRect.GetChild(0).GetComponent<RectTransform>().rect.height;
        float itemHeight = contentRect.GetChild(0).GetComponent<RectTransform>().rect.height + gridLayoutGroup.spacing.y;
        int rowCount = Mathf.CeilToInt((float)contentRect.childCount / columnsInVerticalLayout);
        contentRect.sizeDelta = new Vector2(viewportRect.rect.width, itemHeight * rowCount);

        // Adjust Viewport size as needed
        viewportRect.sizeDelta = new Vector2(viewportRect.sizeDelta.x, contentRect.sizeDelta.y);
    }

    void UpdateContentSizeForHorizontal()
    {
        //float itemWidth = contentRect.GetChild(0).GetComponent<RectTransform>().rect.width;
        float itemWidth = contentRect.GetChild(0).GetComponent<RectTransform>().rect.width + gridLayoutGroup.spacing.x;
        contentRect.sizeDelta = new Vector2(itemWidth * contentRect.childCount, viewportRect.rect.height);

        // Adjust Viewport size as needed
        viewportRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, viewportRect.sizeDelta.y);
    }

    IEnumerator SmoothTransitionAfterLayoutChange()
    {
        // Wait for layout to complete
        yield return new WaitForEndOfFrame();

        // Determine target height and position for the expanded view
        float targetHeight = isVertical ? expandedHeight : originalSize.y;
        Vector2 targetSize = new Vector2(originalSize.x, targetHeight);
        Vector2 targetPosition = isVertical ? new Vector2(originalPosition.x, originalPosition.y + (targetHeight - originalSize.y + 300)) : originalPosition;

        // Smoothly animate the height change
        DOTween.To(() => scrollRect.GetComponent<RectTransform>().sizeDelta, x => scrollRect.GetComponent<RectTransform>().sizeDelta = x, targetSize, animationDuration);

        // Smoothly adjust the scroll position to keep the bottom edge in place
        DOTween.To(() => scrollRect.GetComponent<RectTransform>().anchoredPosition, x => scrollRect.GetComponent<RectTransform>().anchoredPosition = x, targetPosition, animationDuration);

        // Animate the button rotation
        float targetRotation = isVertical ? 180f : 0f;
        buttonImage.transform.DORotate(new Vector3(0, 0, targetRotation), animationDuration);

        // Animate the button position
        Vector2 targetButtonPosition = isVertical ? new Vector2(buttonOriginalPosition.x, originalPosition.y + (targetHeight * 3) + 250) : buttonOriginalPosition;
        buttonRect.DOAnchorPos(targetButtonPosition, animationDuration);

        // Animate the button scale
        Vector2 targetButtonScale = isVertical ? expandedButtonScale : buttonOriginalScale;
        buttonRect.DOScale(targetButtonScale, animationDuration);

        // Adjust scroll position
        if (isVertical)
        {
            DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, 1, animationDuration);
            contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, 0);
        }
        else
        {
            DOTween.To(() => scrollRect.horizontalNormalizedPosition, x => scrollRect.horizontalNormalizedPosition = x, 0, animationDuration);
            contentRect.anchoredPosition = new Vector2(0, 0);
        }

        // Show the horizontal scrollbar only after smooth transition
        if (!isVertical && scrollRect.horizontalScrollbar != null)
        {
            yield return new WaitForSeconds(animationDuration);
            scrollRect.horizontalScrollbar.gameObject.SetActive(true);
        }

        Debug.Log(isVertical ? "Scroll view switched to vertical." : "Scroll view switched to horizontal.");
    }
}

















//using UnityEngine;
//using UnityEngine.UI;
//using DG.Tweening; // DOTween namespace
//using System.Collections;

//public class ScrollViewSwitcher : MonoBehaviour
//{
//    public ScrollRect scrollRect;
//    public Button switchButton;
//    public RectTransform viewportRect;
//    public RectTransform contentRect;
//    public int columnsInVerticalLayout = 4; // Fixed number of columns in vertical layout
//    public int itemsPerRow = 1; // Number of items per row in horizontal layout
//    public float animationDuration = 0.5f; // Duration of the animation

//    private bool isVertical = false;
//    private GridLayoutGroup gridLayoutGroup;

//    void Start()
//    {
//        if (switchButton != null)
//        {
//            switchButton.onClick.AddListener(ToggleScrollView);
//            Debug.Log("Button listener added.");
//        }
//        else
//        {
//            Debug.LogError("Switch button is not assigned.");
//        }

//        gridLayoutGroup = contentRect.GetComponent<GridLayoutGroup>();
//        if (gridLayoutGroup == null)
//        {
//            Debug.LogError("GridLayoutGroup component is missing on Content.");
//        }
//        else
//        {
//            // Initialize the GridLayoutGroup with default horizontal settings
//            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
//            gridLayoutGroup.constraintCount = itemsPerRow;
//        }
//    }

//    void ToggleScrollView()
//    {
//        Debug.Log("Button clicked!");

//        if (scrollRect == null || contentRect == null || viewportRect == null || gridLayoutGroup == null)
//        {
//            Debug.LogError("ScrollRect, Content, Viewport RectTransform or GridLayoutGroup is not assigned.");
//            return;
//        }

//        isVertical = !isVertical;

//        AnimateLayoutSwitch();
//    }

//    void AnimateLayoutSwitch()
//    {
//        // Determine target properties for animation based on layout
//        Vector2 targetCellSize = isVertical ? new Vector2(60, 60) : new Vector2(70, 70);
//        Vector2 targetSpacing = isVertical ? new Vector2(10, 10) : new Vector2(7, 7);

//        // Animate cellSize and spacing
//        DOTween.To(() => gridLayoutGroup.cellSize, x => gridLayoutGroup.cellSize = x, targetCellSize, animationDuration);
//        DOTween.To(() => gridLayoutGroup.spacing, x => gridLayoutGroup.spacing = x, targetSpacing, animationDuration).OnComplete(() =>
//        {
//            if (isVertical)
//            {
//                SwitchToVerticalLayout();
//            }
//            else
//            {
//                SwitchToHorizontalLayout();
//            }

//            // Ensure smooth transition
//            StartCoroutine(SmoothTransitionAfterLayoutChange());
//        });
//    }

//    void SwitchToVerticalLayout()
//    {

//        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
//        gridLayoutGroup.constraintCount = columnsInVerticalLayout;

//        scrollRect.horizontal = false;
//        scrollRect.vertical = true;

//        UpdateContentSizeForVertical();
//    }

//    void SwitchToHorizontalLayout()
//    {

//        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
//        gridLayoutGroup.constraintCount = itemsPerRow;

//        scrollRect.horizontal = true;
//        scrollRect.vertical = false;

//        UpdateContentSizeForHorizontal();

//    }

//    void UpdateContentSizeForVertical()
//    {
//        float itemHeight = contentRect.GetChild(0).GetComponent<RectTransform>().rect.height;
//        int rowCount = Mathf.CeilToInt((float)contentRect.childCount / columnsInVerticalLayout);
//        contentRect.sizeDelta = new Vector2(viewportRect.rect.width, itemHeight * rowCount);

//        viewportRect.sizeDelta = new Vector2(viewportRect.sizeDelta.x, contentRect.sizeDelta.y);
//    }

//    void UpdateContentSizeForHorizontal()
//    {
//        float itemWidth = contentRect.GetChild(0).GetComponent<RectTransform>().rect.width;
//        contentRect.sizeDelta = new Vector2(itemWidth * contentRect.childCount, viewportRect.rect.height);

//        viewportRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, viewportRect.sizeDelta.y);
//    }

//    IEnumerator SmoothTransitionAfterLayoutChange()
//    {
//        // Wait for layout to complete
//        yield return new WaitForEndOfFrame();


//        // Smoothly scroll to the top
//        DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, 1, animationDuration);
//        DOTween.To(() => scrollRect.horizontalNormalizedPosition, x => scrollRect.horizontalNormalizedPosition = x, 0, animationDuration);

//        // Smoothly scroll to the top
//        if (isVertical)
//        {
//            // For vertical, scroll to the top
//            DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, 1, animationDuration).OnComplete(() =>
//            {
//                if (scrollRect.horizontalScrollbar != null)
//                {
//                    scrollRect.horizontalScrollbar.gameObject.SetActive(false);
//                }
//            });
//        }
//        else
//        {
//            // For horizontal, scroll to the left
//            DOTween.To(() => scrollRect.horizontalNormalizedPosition, x => scrollRect.horizontalNormalizedPosition = x, 0, animationDuration).OnComplete(() =>
//            {
//                if (scrollRect.horizontalScrollbar != null)
//                {
//                    scrollRect.horizontalScrollbar.gameObject.SetActive(true);
//                }
//            });
//        }

//        Debug.Log(isVertical ? "Scroll view switched to vertical." : "Scroll view switched to horizontal.");
//    }
//}
