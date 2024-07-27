using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public CinemachineVirtualCamera virtualCamera;
    private bool isFreeLookActive;

    void Start()
    {
        // Baþlangýçta hangi kameranýn aktif olduðunu ayarlayýn
        isFreeLookActive = true;
        freeLookCamera.Priority = 10;
        virtualCamera.Priority = 5;
    }

    void Update()
    {
        // F tuþuna basýldýðýnda kamera deðiþtir
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