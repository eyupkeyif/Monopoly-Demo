using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    // Start is called before the first frame update
    private InGameView inGame;
    void Start()
    {
        inGame = ViewController.Instance.GetView<InGameView>();

        inGame.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
