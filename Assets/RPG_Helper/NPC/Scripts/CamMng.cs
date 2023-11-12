using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMng : MonoBehaviour
{
    public static Cinemachine.CinemachineVirtualCamera vCam;

    void Start()
    {
        vCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
}
