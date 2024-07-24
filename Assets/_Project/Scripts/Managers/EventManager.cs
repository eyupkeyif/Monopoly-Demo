using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
   public static DiceEvents diceEvents = new();
   public static CharacterEvents characterEvents = new CharacterEvents();
   public static InputEvents inputEvents = new();
}

public class DiceEvents
{
    public Action<int,int> OnDiceRolled;
    public Action OnClearSum;
    public Action OnDiceSettled;
    public Action<List<Dice>> OnReadyToSum;
    public Action OnReadyToMove;
    public Action<int> OnDiceSummed;
}
public class CharacterEvents
{
    public Action<Tile> OnStop;
    public Action<ItemBase> OnInventoryChanged;
}
public class InputEvents
{
    public Action OnButtonClick;
    public Action<int> OnDropDownSelected;
}
