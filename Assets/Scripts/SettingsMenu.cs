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

    
    public void ReturnToMainMenu()
    {
       
        settingsPanel.SetActive(false);
        
        mainMenuPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsButton);
        Cursor.visible = true;
    }
}
