using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public GameObject helpPanel;

    private void Start()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
        }
    }

    public void ShowHelp()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(true);
        }
    }

    public void CloseHelp()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
        }
    }
}
