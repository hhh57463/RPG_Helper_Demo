using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class NPC : MonoBehaviour
{
    public string npcName;
    [TextArea] public string[] npcDialog;
    public GameObject dialog;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogText;
    int dialogIdx;
    bool isRange = false;

    void Update()
    {
        if (isRange)
            Dialog();
    }

    void DialogSetting(int idx)
    {
        dialogText.text = npcDialog[idx];
    }

    void Dialog()
    {
        if (!PlayerMng.isDialog)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerMng.isDialog = true;
                CamMng.vCam.Follow = transform;
                CamMng.vCam.LookAt = transform;
                CamMng.vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = 10.0f;
                npcNameText.text = npcName;
                dialogIdx = 0;
                dialog.SetActive(true);
                DialogSetting(dialogIdx);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (dialogIdx < npcDialog.Length - 1)
                    DialogSetting(++dialogIdx);
                else
                {
                    dialog.SetActive(false);
                    PlayerMng.isDialog = false;
                    Transform playerTr = GameObject.FindGameObjectWithTag("Player").transform;
                    CamMng.vCam.Follow = playerTr;
                    CamMng.vCam.LookAt = playerTr;
                    CamMng.vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = -10.0f;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isRange = false;
        }
    }
}
