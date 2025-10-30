using UnityEngine;

public class InstructionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;

    private void Start()
    {
        if (instructionsPanel != null)
            instructionsPanel.SetActive(false);
    }

    public void ToggleInstructions()
    {
        if (instructionsPanel != null)
            instructionsPanel.SetActive(!instructionsPanel.activeSelf);
    }
}
