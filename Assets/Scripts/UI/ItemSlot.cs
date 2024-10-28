using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    public ItemData item;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private Outline outline;

    public UIInventory Inventory;

    public int index;
    public bool equipped;
    public int quantity;


    // Start is called before the first frame update
    private void Awake()
    {
        outline = GetComponent<Outline>();   
    }

    private void OnEnable()
    {   
        outline.enabled = equipped; //장착되어있을 때 enabled된다.
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        //방어코드
        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }
    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        Inventory.SelectItem(index);
    }
}
