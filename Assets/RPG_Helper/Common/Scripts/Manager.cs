using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal static class YieldInstructionCache
{
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (!waitForSeconds.TryGetValue(seconds, out var wfs))
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }
}

public class Manager : MonoBehaviour
{
    private static Manager instance = null;

    public static Manager I
    {
        get
        {
            if (instance.Equals(null))
            {
                Debug.Log("instance is null");
            }
            return instance;
        }
    }

    [SerializeField] Cinemachine.CinemachineVirtualCamera vCam;

    [Header("Player Information")]
    public string playerName;
    public Transform playerTr;

    public GameObject[] charList;

    void Awake()
    {
        instance = this;
        ///////////////////////////////////////
        //GetPlayerTransform();
        ///////////////////////////////////////
    }

    /// <summary>
    /// Call to desired location (Current location: Awake)
    /// str: Player 
    /// </summary>
    public void GetPlayerTransform(string str)
    {
        playerTr = GameObject.Find(str).GetComponent<Transform>();            // Search Method: GameObject.name
        //playerTr = GameObject.FindGameObjectWithTag(str);                   // Search Method: Tag
    }

    public void CameraSetting()
    {
        vCam.Follow = playerTr;
        vCam.LookAt = playerTr;
    }
}
