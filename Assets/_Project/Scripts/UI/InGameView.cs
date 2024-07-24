using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameView : View
{
    [SerializeField] TextMeshProUGUI appleText;
    [SerializeField] TextMeshProUGUI pearText;
    [SerializeField] TextMeshProUGUI strawberryText;
    [SerializeField] TextMeshProUGUI dice1Text,dice2Text;
    [SerializeField] Button RollButton;
    [SerializeField] TMP_Dropdown dropdown;
    public override void Initialize()
    {
        EventManager.characterEvents.OnInventoryChanged+=UpdateInventoryUI;
        EventManager.diceEvents.OnReadyToSum+=UpdateDiceValues;
        RollButton.onClick.AddListener(()=>{EventManager.inputEvents.OnButtonClick?.Invoke();});
        CheckInventoryData();
    }

    public void UpdateInventoryUI(ItemBase item)
    {
        switch (item.itemType)
        {

            case ItemType.Apple: appleText.text = "x" + item.itemAmount;
            break;
            case ItemType.Pear: pearText.text = "x" + item.itemAmount;
            break;
            case ItemType.Strawberry: strawberryText.text = "x" + item.itemAmount;
            break;
            default: break;
        }
    }

    void CheckInventoryData()
    {
        CharacterData characterData = SaveSystem.LoadData();

        if (characterData!=null)
        {
            foreach (var item in characterData.inventory.GetItemList())
            {
                UpdateInventoryUI(item);
            }
        }
        else
        {
            appleText.text="";
            pearText.text="";
            strawberryText.text="";
        }
    }
    public void UpdateDiceValues(List<Dice> dices)
    {
        if (dices.Count==2)
        {
            dice1Text.text = "Dice1: " + dices[0].GetNumber().ToString();
            dice2Text.text = "Dice2: " + dices[1].GetNumber().ToString();
        }
        else if (dices.Count==1)
        {
            dice1Text.text ="Dice" + dices[0].GetNumber().ToString();
            dice2Text.text = "";
        }
        else
        {
            int total = 0;

            foreach (var item in dices)
            {
                total+=item.GetNumber();
            }

            dice1Text.text = "Total: " + total;
            dice2Text.text = "";
        }
        
    }

    public void ChangeDiceAmount()
    {
        EventManager.inputEvents.OnDropDownSelected?.Invoke(dropdown.value);
    }
}
