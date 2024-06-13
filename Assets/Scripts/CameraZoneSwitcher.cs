using Cinemachine;
using UnityEngine;

public class CameraZoneSwitcher : MonoBehaviour
{
    public string triggerTag;
    public CinemachineVirtualCamera  primaryCamera;
    public CinemachineVirtualCamera[] VirtualCameras;
    void Start()
    {
        SwitchCamera( primaryCamera); 
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag(triggerTag))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchCamera(targetCamera);
        }
    }

    void SwitchCamera(CinemachineVirtualCamera target)
    {
        foreach (CinemachineVirtualCamera camera in VirtualCameras)
        {
            camera.Priority = camera == target ? 10 : 0;
        }
    }

    [ContextMenu("Get All Virtual Cameras")]
    void GetAllVirtualCameras()
    {
        VirtualCameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
    }
}
