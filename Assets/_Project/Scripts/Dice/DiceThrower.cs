using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
public class DiceThrower : MonoBehaviour
{
    // Start is called before the first frame update
    public Dice diceToThrow;
    public int diceAmount = 2;
    public float throwForce = 5;
    public float rollForce = 10;
    private ObjectPool<Dice> objectPool;
    private List<Dice> activeDice = new List<Dice>();
    private List<Dice> deactiveDice = new List<Dice>();
    void Start()
    {
        objectPool =new ObjectPool<Dice>(CreateDice,DiceOnGet,DiceOnRelease,DiceOnDestroy,true,50,100);

        EventManager.diceEvents.OnDiceSettled+=AreAllDiceSettled;
        EventManager.inputEvents.OnButtonClick+=RollDice;
        EventManager.inputEvents.OnDropDownSelected+=ChangeDiceAmount;
    }

    private Dice CreateDice()
    {
        Dice dice = Instantiate(diceToThrow,transform.position,transform.rotation);
        dice.Iniatilize(objectPool);
        dice.gameObject.SetActive(false);
        return dice;
    }
    private void ChangeDiceAmount(int amount)
    {
        
        diceAmount = amount + 1;
    }

    private void DiceOnGet(Dice dice)
    {
        dice.SubscribeDiceList(activeDice);
        dice.UnsubscribeDiceList(deactiveDice);
        dice.gameObject.SetActive(true);
    }
    private void DiceOnRelease(Dice dice)
    {
        dice.SubscribeDiceList(deactiveDice);
        dice.UnsubscribeDiceList(activeDice);
        dice.gameObject.SetActive(false);
    }
    private void DiceOnDestroy(Dice dice)
    {
        dice.UnsubscribeDiceList(activeDice);
        dice.UnsubscribeDiceList(deactiveDice);
        Destroy(dice.gameObject);
    }

    private void AreAllDiceSettled()
    {
        bool isSettled = true;

        for (int i = 0; i < activeDice.Count; i++)
        {
            if (!activeDice[i].IsStopRolling())
            {
                isSettled = false;
            }
        }

        if (isSettled)
        {
            EventManager.diceEvents.OnReadyToSum?.Invoke(activeDice);
        }
    }

    private async void RollDice()
    {
        EventManager.diceEvents.OnClearSum?.Invoke();

        if (activeDice.Count>diceAmount)
        {
            int activeDiceAmount = activeDice.Count;
            for (int i = 0; i < activeDiceAmount-diceAmount; i++)
            {
                objectPool.Release(activeDice[0]);
            }
            
        }

        if (activeDice.Count<diceAmount)
        {
            int activeDiceAmount = activeDice.Count;

            for (int i = 0; i < diceAmount-activeDiceAmount; i++)
            {
                Dice dice = objectPool.Get();
                //dice.RollDice(throwForce,rollForce,i);
            }
        }
        
        for (int i = 0; i < activeDice.Count; i++)
        {
            activeDice[i].gameObject.transform.position = transform.position;
            activeDice[i].gameObject.transform.rotation = transform.rotation;
            activeDice[i].RollDice(throwForce,rollForce,i);
            await Task.Yield();
        }
    }
}
