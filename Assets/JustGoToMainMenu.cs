using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JustGoToMainMenu : MonoBehaviour
{
   public void JustMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
