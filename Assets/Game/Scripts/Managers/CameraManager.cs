using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : SceneBasedMonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private CinemachineVirtualCamera battleCam;
    [SerializeField] private CinemachineVirtualCamera startCam;
    [SerializeField] private CinemachineVirtualCamera finalCam;
    //[SerializeField] private CinemachineVirtualCamera finalPlayerCam;
    [SerializeField] private CinemachineVirtualCamera activeCam => CameraSwitcher.GetActiveCamera();
    private void OnEnable()
    {
        Debug.Log(activeCam);
        CameraSwitcher.AddCamera(playerCam);
        CameraSwitcher.AddCamera(battleCam);
        CameraSwitcher.AddCamera(startCam);
        CameraSwitcher.AddCamera(finalCam);
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
        else if (cameraName.Equals("StartCam") && !CameraSwitcher.IsActiveCamera(battleCam))
        {
            CameraSwitcher.SwitchCamera(startCam);
        }
        else if (cameraName.Equals("FinalCam") && !CameraSwitcher.IsActiveCamera(finalCam))
        {
            CameraSwitcher.SwitchCamera(finalCam);
        }
    }
}
