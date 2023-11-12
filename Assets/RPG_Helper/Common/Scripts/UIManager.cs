using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject selectPanel;
    [SerializeField] GameObject monsterManager;
    
    public void CharSelect(int num){
        Manager.I.playerSc = Instantiate(Manager.I.charList[num], GameObject.Find("Game").transform).GetComponent<PlayerMng>();
        selectPanel.SetActive(false);
        monsterManager.SetActive(true);
        Invoke("LevelSet", 1);
    }

    void LevelSet(){
        Manager.I.levelMng.LevelTextSetting();
    }
}
