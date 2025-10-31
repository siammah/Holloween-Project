using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    // Optional: assign a Text in inspector. If left null the script will create a Canvas+Text.
    public Text scoreText;
    public bool persistAcrossScenes = false;

    int score = 0;

    void Awake()
    {
        // singleton (prefer instance that already has a bound Text)
        if (Instance == null)
        {
            Instance = this;
            if (persistAcrossScenes) DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            if (Instance.scoreText == null && this.scoreText != null)
            {
                Destroy(Instance.gameObject);
                Instance = this;
                if (persistAcrossScenes) DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        // Try to bind any existing UI, otherwise create one
        TryBindOrCreateUI();
        UpdateScoreDisplay();
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        if (scoreText == null) TryBindOrCreateUI();
        UpdateScoreDisplay();
    }

    void TryBindOrCreateUI()
    {
        if (scoreText != null) return;

        // 1) Try to find a Text named "ScoreText"
        var go = GameObject.Find("ScoreText");
        if (go != null)
        {
            var t = go.GetComponent<Text>();
            if (t != null) { scoreText = t; return; }
        }

        // 2) Try to find any Text with "score" in its name
        var all = FindObjectsOfType<Text>(true);
        foreach (var t in all)
        {
            if (t.gameObject.name.ToLower().Contains("score"))
            {
                scoreText = t;
                return;
            }
        }

        // 3) Create Canvas + ScoreText
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            var canvasGO = new GameObject("ScoreManager_Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
            canvas.sortingOrder = 1000;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            var scaler = canvasGO.GetComponent<CanvasScaler>();
            if (scaler != null)
            {
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
                scaler.matchWidthOrHeight = 0.5f;
            }

            if (persistAcrossScenes) DontDestroyOnLoad(canvasGO);
        }

        var textGO = new GameObject("ScoreText");
        textGO.transform.SetParent(canvas.transform, false);

        var rect = textGO.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0f, 1f); // top-left
        rect.anchorMax = new Vector2(0f, 1f);
        rect.pivot = new Vector2(0f, 1f);
        rect.anchoredPosition = new Vector2(10f, -10f);
        rect.sizeDelta = new Vector2(320f, 60f);

        var txt = textGO.AddComponent<Text>();
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        txt.fontSize = 32;
        txt.color = Color.white;
        txt.alignment = TextAnchor.UpperLeft;
        txt.raycastTarget = false;
        txt.text = "Score: 0";

        // outline to improve contrast
        var outline = textGO.AddComponent<Outline>();
        outline.effectColor = new Color(0f, 0f, 0f, 0.8f);
        outline.effectDistance = new Vector2(1f, -1f);

        scoreText = txt;

        if (persistAcrossScenes) DontDestroyOnLoad(textGO);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    // debug helper: press K during play to add 1 point
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) AddScore(1);
    }
}