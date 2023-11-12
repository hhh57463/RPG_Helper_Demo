using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    
    ////////////////////////////////////////////////////
    /// <summary>
    /// Write a suitable player script
    /// </summary>
    public PlayerMng playerSc;
    ////////////////////////////////////////////////////

    void Start()
    {
        levelDatas = JsonUtility.FromJson<LevelDataMng>(levelJason.text);
        LevelTextSetting();
    }

    /// <summary>
    /// This is the function that triggers the level up event.
    /// </summary>
    public void LevelUpEvent(int level)
    {
        levelText.text = "Level: " + level.ToString();
        playerSc.exp = 0.0f;
        for (int i = 0; i < levelDatas.levelData.Length; i++)
        {
            if (level.Equals(levelDatas.levelData[i].level))
            {
                playerSc.maxExp = levelDatas.levelData[i].exp;
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
        levelImg.fillAmount = playerSc.exp / playerSc.maxExp;
        expText.text = "Exp: " + (playerSc.exp / playerSc.maxExp) * 100.0f + "% (" + playerSc.exp + " / " + playerSc.maxExp.ToString() + ")";
    }
}
