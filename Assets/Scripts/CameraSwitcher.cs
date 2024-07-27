using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public CinemachineVirtualCamera virtualCamera;
    private bool isFreeLookActive;

    void Start()
    {
        // Ba�lang��ta hangi kameran�n aktif oldu�unu ayarlay�n
        isFreeLookActive = true;
        freeLookCamera.Priority = 10;
        virtualCamera.Priority = 5;
    }

    void Update()
    {
        // F tu�una bas�ld���nda kamera de�i�tir
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (isFreeLookActive)
        {
            freeLookCamera.Priority = 5;
            virtualCamera.Priority = 10;
        }
        else
        {
            freeLookCamera.Priority = 10;
            virtualCamera.Priority = 5;
        }
        isFreeLookActive = !isFreeLookActive;
    }
}