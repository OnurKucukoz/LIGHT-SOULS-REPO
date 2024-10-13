using System.Collections;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    public GameObject losePanel;
    public GameObject loseText;

    public void ShowLosePanel()
    {
        loseText.SetActive(true);
        losePanel.SetActive(true);
    }
}
