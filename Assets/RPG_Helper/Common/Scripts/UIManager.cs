using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject selectPanel;
    [SerializeField] GameObject monsterManager;
    [SerializeField] GameObject expObj;
    
    public void CharSelect(int num){
        Manager.I.playerSc = Instantiate(Manager.I.charList[num], GameObject.Find("Game").transform).GetComponent<PlayerMng>();
        selectPanel.SetActive(false);
        monsterManager.SetActive(true);
        expObj.SetActive(true);
        Invoke("LevelSet", 1);
    }

    void LevelSet(){
        Manager.I.levelMng.LevelTextSetting();
    }
}
