using System.Collections;
using UnityEngine;

public class VictoryPanel : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject victoryAchievedText;
 
    public void ShowVictoryPanel()
    {
        victoryAchievedText.SetActive(true);
        victoryPanel.SetActive(true);
    }
}
