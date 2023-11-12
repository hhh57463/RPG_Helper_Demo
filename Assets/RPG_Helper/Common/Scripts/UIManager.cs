using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject selectPanel;
    [SerializeField] GameObject monsterManager;
    
    public void CharSelect(int num){
        Instantiate(Manager.I.charList[num], GameObject.Find("Game").transform);
        selectPanel.SetActive(false);
        monsterManager.SetActive(true);
    }
}
