using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RawImage btnImg;
    public ItemData data;
    public TooltipObj tooltipObj;
    public TextMeshProUGUI countText;
    bool isHover;

    void Awake()
    {
        btnImg = GetComponent<RawImage>();
    }

    void OnEnable()
    {
        isHover = false;
        StartCoroutine(GetTexture(btnImg));
    }

    public void ItemClick()
    {
        // Item Click Event
        Debug.Log("Item Click");
    }

    IEnumerator GetTexture(RawImage img)
    {
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(data.link);
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            img.texture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
        tooltipObj.tooltip.SetActive(true);
        tooltipObj.tooltip.transform.position = Input.mousePosition;
        StartCoroutine(GetTexture(tooltipObj.itemImg));
        tooltipObj.name.text = data.itemName;
        switch (data.itemType)
        {
            case ITEMTYPE.EQUIP:
                tooltipObj.type.text = "Equip Item";
                break;
            case ITEMTYPE.CONSUM:
                tooltipObj.type.text = "Consum Item";
                break;
            case ITEMTYPE.ETC:
                tooltipObj.type.text = "ETC Item";
                break;
        }
        tooltipObj.info.text = data.info;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHover)
            tooltipObj.tooltip.SetActive(false);
    }
}
