using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data.Common;

public class LevelMng : MonoBehaviour
{

    [System.Serializable]
    public class LevelDataMng
    {
        public LevelData[] levelData;
    }

    [System.Serializable]
    public class LevelData
    {
        public int level;
        public float exp;
    }
    [SerializeField] TextAsset levelJason;
    public LevelDataMng levelDatas;
    [SerializeField] Image levelImg;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expText;

    void Start()
    {
        levelDatas = JsonUtility.FromJson<LevelDataMng>(levelJason.text);
    }

    /// <summary>
    /// This is the function that triggers the level up event.
    /// </summary>
    public void LevelUpEvent(int level)
    {
        levelText.text = "Level: " + level.ToString();
        Manager.I.playerSc.exp = 0.0f;
        for (int i = 0; i < levelDatas.levelData.Length; i++)
        {
            if (level.Equals(levelDatas.levelData[i].level))
            {
                Manager.I.playerSc.maxExp = levelDatas.levelData[i].exp;
                break;
            }
        }
        LevelTextSetting();
    }

    /// <summary>
    /// This function is called when the experience value fluctuates.
    /// </summary>
    public void LevelTextSetting()
    {
        levelImg.fillAmount = Manager.I.playerSc.exp / Manager.I.playerSc.maxExp;
        expText.text = "Exp: " + ((Manager.I.playerSc.exp / Manager.I.playerSc.maxExp) * 100.0f).ToString() + "% (" + Manager.I.playerSc.exp.ToString() + " / " + Manager.I.playerSc.maxExp.ToString() + ")";
    }
}
