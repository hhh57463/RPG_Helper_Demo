using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object Asset/ItemData")]
public class ItemData : ScriptableObject
{
    public ITEMTYPE itemType;
    public string itemName;
    [TextArea] public string info;
    public int count;
    public string link;
}
