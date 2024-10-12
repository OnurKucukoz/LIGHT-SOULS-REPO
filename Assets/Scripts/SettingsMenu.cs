using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;  
    public GameObject settingsPanel;
    public GameObject controlsButton;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    // Ana men�ye d�nmek i�in bir fonksiyon olu�tur
    public void ReturnToMainMenu()
    {
       
        settingsPanel.SetActive(false);
        
        mainMenuPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsButton);
    }
}
