using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int sum = 0;
    
    void Start()
    {
        EventManager.diceEvents.OnClearSum+=OnResetHandler;
        EventManager.diceEvents.OnReadyToSum+=OnSumHandler;
    }

    private void OnResetHandler()
    {
        sum = 0;
    }

    private void OnSumHandler(List<Dice> activeDice)
    {
        for (int i = 0; i < activeDice.Count; i++)
        {
            sum+=activeDice[i].GetNumber();
        }
        
        EventManager.diceEvents.OnDiceSummed?.Invoke(sum);
        EventManager.diceEvents.OnReadyToMove?.Invoke();
        
        Debug.Log("sum: " + sum);
    }

    public int GetSum()
    {
        return sum;
    }

}
