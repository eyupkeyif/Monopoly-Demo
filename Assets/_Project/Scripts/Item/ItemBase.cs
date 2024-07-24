using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

[Serializable]
public class ItemBase
{
    public ItemType itemType;
    public int itemAmount;
    public Image itemImage;
    public TextMeshProUGUI itemText;

}

public enum ItemType
{
    None,
    Apple,
    Pear,
    Strawberry
}
