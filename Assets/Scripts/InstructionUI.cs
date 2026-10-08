
using UnityEngine;

public class InstructionUI : MonoBehaviour
{
    public GameObject instructionPanel;

    public void OpenInstructions()
    {
        instructionPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionPanel.SetActive(false);
    }
}
