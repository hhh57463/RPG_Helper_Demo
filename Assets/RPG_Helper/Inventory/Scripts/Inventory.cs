using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ITEMTYPE
{
    EQUIP,
    CONSUM,
    ETC
}

[System.Serializable]
public struct TooltipObj
{
    public GameObject tooltip;
    public RawImage itemImg;
    public TextMeshProUGUI type;
    public TextMeshProUGUI name;
    public TextMeshProUGUI info;
}
[System.Serializable]
public class ItemsData
{
    public string name;
    public string info;
    public int count;
    public string link;

    public ItemsData(string name, string info, int count, string link)
    {
        this.name = name;
        this.info = info;
        this.count = count;
        this.link = link;
    }
}
public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class ItemDataMng
    {
        public List<ItemsData> equipItem = new List<ItemsData>();
        public List<ItemsData> consumItem = new List<ItemsData>();
        public List<ItemsData> etcItem = new List<ItemsData>();
    }
    [SerializeField] TextAsset itemJson;
    public static ItemDataMng itemDatas;

    /////////////////////////////////////////////////////////
    /// <summary> 
    /// Inventory-related variables.
    /// </sumary>
    [SerializeField] GameObject inventroy;
    [SerializeField] Transform inventorySlotParent;
    [SerializeField] GameObject slotPrefab;
    public List<Slot> slots = new List<Slot>();
    [SerializeField] TooltipObj tooltipObj;
    [SerializeField] int inventoryType;
    /////////////////////////////////////////////////////////

    void Start()
    {
        itemDatas = JsonUtility.FromJson<ItemDataMng>(itemJson.text);
        for (int i = 0; i < 50; i++)
        {
            slots.Add(Instantiate(slotPrefab, inventorySlotParent).GetComponent<Slot>());
            slots[i].tooltipObj = tooltipObj;
            slots[i].gameObject.SetActive(false);
        }
        inventoryType = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventroy.SetActive(true);
            tooltipObj.tooltip.SetActive(false);
            SettingItem(inventoryType);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InventoryClose();
        }
    }

    void OnApplicationQuit()
    {
        string saveData = JsonUtility.ToJson(itemDatas);

        string fileName = "ItemData.json";
        string path = Application.dataPath + "/Inventory/Json/" + fileName;

        System.IO.File.WriteAllText(path, saveData);
    }

    public void InventoryClose()
    {
        tooltipObj.tooltip.SetActive(false);
        inventroy.SetActive(false);
    }

    public void ItemTypeBtn(int type)
    {
        inventoryType = type;
        SettingItem(inventoryType);
    }

    void SettingItem(int type)
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].gameObject.SetActive(false);
        switch (type)
        {
            case 0:
                for (int i = 0; i < itemDatas.equipItem.Count; i++)
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].data = Resources.Load<ItemData>("ScriptableObject/Item/" + itemDatas.equipItem[i].name);
                    slots[i].countText.text = "x " + itemDatas.equipItem[i].count.ToString();
                }
                break;
            case 1:
                for (int i = 0; i < itemDatas.consumItem.Count; i++)
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].data = Resources.Load<ItemData>("ScriptableObject/Item/" + itemDatas.consumItem[i].name);
                    slots[i].countText.text = "x " + itemDatas.consumItem[i].count.ToString();
                }
                break;
            case 2:
                for (int i = 0; i < itemDatas.etcItem.Count; i++)
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].data = Resources.Load<ItemData>("ScriptableObject/Item/" + itemDatas.etcItem[i].name);
                    slots[i].countText.text = "x " + itemDatas.etcItem[i].count.ToString();
                }
                break;
        }
    }
}