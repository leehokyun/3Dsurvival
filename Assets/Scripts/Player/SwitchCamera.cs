using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    //Ä«¸Þ¶ó
    public GameObject Camera_FPSview;
    public GameObject Camara_TPSview;
    public GameObject Camara_EquipCam;
    public int managerNum = 0;

    void Cam_FPS()
    {
        Camera_FPSview.SetActive(true);
        Camara_TPSview.SetActive(false);
        Camara_EquipCam.SetActive(true);
    }

    void Cam_TPS()
    {
        Camera_FPSview.SetActive(false);
        Camara_TPSview.SetActive(true);
        Camara_EquipCam.SetActive(false);
    }

    public void OnCameraSwitch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (managerNum == 0)
            {
                Cam_FPS();
                managerNum = 1;
            }
            else
            {
                Cam_TPS();
                managerNum = 0;
            }
        }
    }
}
