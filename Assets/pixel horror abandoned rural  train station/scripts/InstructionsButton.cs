using UnityEngine;

public class InstructionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;

    private void Start()
    {
        // Make sure the panel starts hidden
        if (instructionsPanel != null)
            instructionsPanel.SetActive(false);
    }

    // This function toggles visibility each time the button is clicked
    public void ToggleInstructions()
    {
        if (instructionsPanel != null)
            instructionsPanel.SetActive(!instructionsPanel.activeSelf);
    }
}
