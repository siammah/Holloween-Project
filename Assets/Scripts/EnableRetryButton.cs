using System.Collections;
using UnityEngine;

public class EnableRetryButton : MonoBehaviour
{
    // Drag your retry button here in the Inspector
    public GameObject retryButton;

    void Start()
    {
        // Disable the button for one frame to avoid timing issues
        retryButton.SetActive(false);
        StartCoroutine(EnableButtonNextFrame());
    }

    IEnumerator EnableButtonNextFrame()
{
    yield return null;
    retryButton.SetActive(true);
}

}
