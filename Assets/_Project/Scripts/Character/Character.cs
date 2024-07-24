using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public Inventory inventory;

    public CharacterData(Character character)
    {
        inventory = character.inventory;
    }
}
public class Character : MonoBehaviour
{
    [SerializeField] CharacterMovement characterMovement;
    public Inventory inventory;
    

    public void Initialize(Tile currentTile)
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterMovement.Initialize(currentTile);

        if (SaveSystem.LoadData()!=null)
        {
            inventory = SaveSystem.LoadData().inventory;
        }
        else
        {
            inventory = new Inventory();
        }
        
    }

    private void Start()
    {
        EventManager.characterEvents.OnStop+=AddItemToInventory;
    }

    private void AddItemToInventory(Tile tile)
    {
        inventory.AddItem(tile.item);
        SaveSystem.SaveData(this);
    }
}
