using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class Inventory
{
    // Start is called before the first frame update
    [SerializeField] private List<ItemBase> itemList = new();

    public Inventory()
    {
        itemList = new List<ItemBase>();
    }

    public void AddItem(ItemBase item)
    {
        if(item.itemType==ItemType.None) return;

        bool alreadyInInventory=false;
        foreach (var _item in itemList)
        {
            if (_item.itemType == item.itemType)
            {
                _item.itemAmount += item.itemAmount;
                alreadyInInventory=true;
                EventManager.characterEvents.OnInventoryChanged?.Invoke(_item);
            }
        }

        if (!alreadyInInventory)
        {
            itemList.Add(item);
            EventManager.characterEvents.OnInventoryChanged?.Invoke(item);
        }

        
        
        
    }
    public List<ItemBase> GetItemList()
    {
        return itemList;
    }
}
