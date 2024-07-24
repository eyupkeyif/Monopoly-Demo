using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Tile currentTile;
    [SerializeField] float speed;
    private int moveStep;
    private bool moving=false;
    private bool isReadyToMove=false;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float g=9.81f; 
    [SerializeField] private float launchAngle=45;
    [SerializeField] private float time=0/5f;
    public void Initialize(Tile currentTile)
    {
        this.currentTile = currentTile;
        transform.position = new Vector3(currentTile.gameObject.transform.position.x,transform.position.y,currentTile.gameObject.transform.position.z);

    }

    private void Start()
    {
        EventManager.diceEvents.OnDiceSummed+=DiceSumHandler;
        EventManager.diceEvents.OnReadyToMove+=ReadyToMoveHandler;
    }

    private void ReadyToMoveHandler()
    {
        isReadyToMove=true;
    }
    private void DiceSumHandler(int step)
    {
        moveStep = step;
    }

    private void Update()
    {
        if(!isReadyToMove) return;

        if (isReadyToMove && !moving)
        {
            
            isReadyToMove=false;
            
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        if (moving)
        {
            yield break;
        }

        moving = true;

        while (moveStep>0)
        {
            
            
            float timer = 0;
            while (timer<time)
            {

                
                float yPos = speed*timer*Mathf.Sin(launchAngle) - 0.5f * g *Mathf.Pow(timer,2);
                float heightOffset = Mathf.Clamp(yPos,0,10);

                Vector3 targetPos = currentTile.nextTile.gameObject.transform.position;
                Vector3 movePosition = Vector3.Lerp(transform.position,targetPos,timer);

                
                movePosition.y = heightOffset;
                transform.position = movePosition;

                timer+=Time.fixedDeltaTime;
                
                yield return new WaitForFixedUpdate();
            }
            
            Vector3 finalPos = currentTile.nextTile.gameObject.transform.position;
            transform.position = finalPos;

            currentTile = currentTile.nextTile;

            yield return new WaitForSeconds(0.1f);

            elapsedTime=0;
            moveStep--;
        }

        moving=false;
        EventManager.characterEvents.OnStop?.Invoke(currentTile);
        
    }


}
