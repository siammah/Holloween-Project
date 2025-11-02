using UnityEngine;
using TMPro;           // TextMeshPro
using UnityEngine.UI;  // Canvas, GraphicRaycaster

public class EndScreen : MonoBehaviour
{
    private TMP_Text endMessage;

    void Awake()
    {
        // Try to find an existing Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        // Create EndMessage only if it doesn't already exist
        if (canvas.transform.Find("EndMessage") == null)
        {
            GameObject textGO = new GameObject("EndMessage");
            textGO.transform.SetParent(canvas.transform);

            endMessage = textGO.AddComponent<TMP_Text>();

            endMessage.alignment = TextAlignmentOptions.Center;
            endMessage.fontSize = 80;
            endMessage.color = Color.white;

            RectTransform rt = endMessage.rectTransform;
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(1, 1);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            endMessage.gameObject.SetActive(false);
        }
        else
        {
            // If already exists in scene
            endMessage = canvas.transform.Find("EndMessage").GetComponent<TMP_Text>();
        }
    }

    public void ShowMessage(string message)
    {
        if (endMessage == null)
        {
            Debug.LogError("EndScreen: Could not create or find endMessage!");
            return;
        }

        endMessage.text = message;
        endMessage.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
