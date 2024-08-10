using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragSkill : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector3 initialPosition;
    private Transform initialParent;

    private GameObject draggingIcon; // Icon for dragging
    private Image iconImage; // Image component of the icon

    // Reference to the AssignedSkillAtSkillTreeAndSkillSO script to access SkillSO
    private AssignedSkillAtSkillTreeAndSkillSO skillTreeScript;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Find the parent Canvas
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas not found in parent hierarchy.");
        }

        // Get the skill tree script
        skillTreeScript = GetComponent<AssignedSkillAtSkillTreeAndSkillSO>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvas == null || rectTransform == null || canvasGroup == null || skillTreeScript == null)
        {
            Debug.LogError("Missing essential components for dragging.");
            return;
        }

        initialPosition = rectTransform.position;
        initialParent = rectTransform.parent;
        canvasGroup.blocksRaycasts = false; // Disable Raycast blocking

        // Create icon for dragging
        draggingIcon = new GameObject("Dragging Icon");
        draggingIcon.transform.SetParent(canvas.transform, false);
        draggingIcon.transform.SetAsLastSibling(); // Bring icon to the front

        // Add Image component and set sprite from original object
        Image draggingIconImage = draggingIcon.AddComponent<Image>();
        iconImage = GetComponent<Image>();
        if (iconImage != null)
        {
            draggingIconImage.sprite = iconImage.sprite;
        }
        draggingIconImage.raycastTarget = false; // Disable Raycast interaction with icon

        RectTransform draggingIconRect = draggingIcon.GetComponent<RectTransform>();
        draggingIconRect.sizeDelta = new Vector2(50, 50); // Set icon size
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingIcon == null)
        {
            return;
        }

        // Update icon position to follow the cursor
        RectTransform iconRectTransform = draggingIcon.GetComponent<RectTransform>();
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        iconRectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Restore Raycast blocking

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        bool skillMoved = false;

        foreach (RaycastResult result in results)
        {
            SkillSlot targetSlot = result.gameObject.GetComponent<SkillSlot>();
            if (targetSlot != null && skillTreeScript.skillSO != null)
            {
                // Move the object into SkillSlot and replace the image
                targetSlot.MoveChildFromCategorySkill(draggingIcon.transform);

                // Set the SkillSO in the target slot
                targetSlot.SetSkill(skillTreeScript.skillSO);

                skillMoved = true;
                break;
            }
        }

        // Destroy the icon after dragging ends
        Destroy(draggingIcon);

        if (!skillMoved)
        {
            // If the icon wasn't moved, return the original to its initial position
            rectTransform.position = initialPosition;
            rectTransform.SetParent(initialParent);
        }
    }
}