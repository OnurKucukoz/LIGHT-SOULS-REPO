using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public CinemachineVirtualCamera virtualCamera;
    public bool isFreeLookActive;
    public Transform player;
    public Transform target;

    public float lockOnOffsetDistance = 5f;

    void Start()
    {

        isFreeLookActive = true;
        freeLookCamera.Priority = 10;
        virtualCamera.Priority = 5;
    }

    void Update()
    {

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