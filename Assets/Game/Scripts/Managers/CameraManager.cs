using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private CinemachineVirtualCamera battleCam;
    //[SerializeField] private CinemachineVirtualCamera finalPlayerCam;
    private void OnEnable()
    {
        CameraSwitcher.AddCamera(playerCam);
        CameraSwitcher.AddCamera(battleCam);
    }

    public void SwitchCam(string cameraName)
    {
        //TODO ENUM SWITCH
        if (cameraName.Equals("PlayerCam") && !CameraSwitcher.IsActiveCamera(playerCam))
        {
            CameraSwitcher.SwitchCamera(playerCam);
        }
        else if (cameraName.Equals("BattleCam") && !CameraSwitcher.IsActiveCamera(battleCam))
        {
            CameraSwitcher.SwitchCamera(battleCam);
        }
    }
}
