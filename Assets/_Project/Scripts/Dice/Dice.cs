using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Dice : MonoBehaviour
{
    [SerializeField] Transform[] sides;
    [SerializeField] Rigidbody rb;
    private int diceIndex=-1;
    private int topNumber;

    private bool _hasStopRolling;
    private bool _delayFinished;
    
    private ObjectPool<Dice> pool;

    public void Iniatilize(ObjectPool<Dice> pool)
    {
        this.pool = pool;
    }

    private void Update()
    {
        if(!_delayFinished) return;

        if(!_hasStopRolling && rb.velocity.sqrMagnitude==0)
        {
            _hasStopRolling=true;
            rb.isKinematic=true;
            
            CalculateNumber();
            EventManager.diceEvents.OnDiceRolled?.Invoke(diceIndex,topNumber);
        
            EventManager.diceEvents.OnDiceSettled?.Invoke();

        }
    }
    public bool IsStopRolling()
    {
        return _hasStopRolling;
    }
    
    private int CalculateNumber()
    {
        if(sides==null) return -1;

        var topFace=0;
        var lastYPosition = sides[0].transform.position.y;

        for (int i = 0; i < sides.Length; i++)
        {
            if (lastYPosition<sides[i].transform.position.y)
            {
                lastYPosition = sides[i].transform.position.y;
                topFace = i;
            }
        }
        topNumber = topFace + 1;
        

        Debug.Log("number rolled " + (topFace +1)  ,gameObject);
        return topNumber;
    }
    public int GetNumber()
    {
        return topNumber;
    }
    public void SubscribeDiceList(List<Dice> diceList)
    {
        if (!diceList.Contains(this))
        {
            diceList.Add(this);
        }
    }
    public void UnsubscribeDiceList(List<Dice> diceList)
    {
        if (diceList.Contains(this))
        {
            diceList.Remove(this);
        }
    }

    public void RollDice(float throwForce,float rollForce,int diceNum)
    {
        _delayFinished = false;
        rb.isKinematic=false;
        diceIndex = diceNum;
        float randomVar = Random.Range(-1,1);

        //rb.velocity= Vector3.zero;
       // rb.angularVelocity = Vector3.zero;

        rb.AddForce(transform.forward*(throwForce + randomVar),ForceMode.Impulse);

        float randX = Random.Range(0,1);
        float randY = Random.Range(0,1);
        float randZ = Random.Range(0,1);

        rb.AddTorque(new Vector3(randX,randY,randZ)*(rollForce+randomVar),ForceMode.Impulse);

        _hasStopRolling=false;

        DelayResult();
    }

    public void ReturnToPool()
    {
        pool.Release(this);
    }

    private async void DelayResult()
    {
        await Task.Delay(1000);
        _delayFinished = true;
        
    }
   
}
