using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    int CheckAlreadyItem(ITEMTYPE type, string name)
    {
        if (type.Equals(ITEMTYPE.CONSUM))
        {
            for (int i = 0; i < Inventory.itemDatas.consumItem.Count; i++)
            {
                if (Inventory.itemDatas.consumItem[i].name.Equals(name))
                {
                    return i;
                }
            }
        }
        else if (type.Equals(ITEMTYPE.ETC))
        {
            for (int i = 0; i < Inventory.itemDatas.etcItem.Count; i++)
            {
                if (Inventory.itemDatas.etcItem[i].name.Equals(name))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    void AddItem(ITEMTYPE type)
    {
        switch (type)
        {
            case ITEMTYPE.EQUIP:
                Inventory.itemDatas.equipItem.Add(new ItemsData(data.itemName, data.info, data.count, data.link));
                break;
            case ITEMTYPE.CONSUM:
            {
                int idx = CheckAlreadyItem(ITEMTYPE.CONSUM, data.itemName);
                if (idx.Equals(-1))
                    Inventory.itemDatas.consumItem.Add(new ItemsData(data.itemName, data.info, data.count, data.link));
                else
                    Inventory.itemDatas.consumItem[idx].count += data.count;
                break;
            }
            case ITEMTYPE.ETC:
            {
                int idx = CheckAlreadyItem(ITEMTYPE.ETC, data.itemName);
                if (idx.Equals(-1))
                    Inventory.itemDatas.etcItem.Add(new ItemsData(data.itemName, data.info, data.count, data.link));
                else
                    Inventory.itemDatas.etcItem[idx].count += data.count;
                break;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
           AddItem(data.itemType);
        }
    }
}
