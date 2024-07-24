using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    [SerializeField] Character character;
    void Start()
    {
    
        MapGenerator.Instance.GenerateRandom();
        character.Initialize(MapGenerator.Instance.GetTileArray()[0]);

    }
    

}
