using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    [Header("UI")]
    public Image img;
    public TMP_Text meter;

    [Header("Target")]
    public Transform target;
    public Vector3 offset;

    [Header("References")]
    public Canvas canvas;
    public Camera mainCamera;

    private RectTransform canvasRect;
    private RectTransform iconRect;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        canvasRect = canvas.GetComponent<RectTransform>();
        iconRect = img.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        // Distance text (use CAMERA position, not UI)
        float distance = Vector3.Distance(mainCamera.transform.position, target.position);
        meter.text = Mathf.RoundToInt(distance) + "m";

        // If target is behind the camera
        if (screenPos.z < 0)
        {
            screenPos.x = Screen.width - screenPos.x;
            screenPos.y = Screen.height - screenPos.y;
            screenPos.z = 0;
        }

        // Clamp to screen bounds
        float padding = 50f;
        screenPos.x = Mathf.Clamp(screenPos.x, padding, Screen.width - padding);
        screenPos.y = Mathf.Clamp(screenPos.y, padding, Screen.height - padding);

        // Convert screen position to UI position
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera,
            out uiPos
        );

        iconRect.localPosition = uiPos;
    }
}
