using UnityEngine;
using Cinemachine;

public class CameraSetup : MonoBehaviour
{
    private void Start()
    {
        CinemachineVirtualCamera cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = FindObjectOfType<Player>().transform;
    }
}