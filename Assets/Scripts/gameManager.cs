using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public float boatsSailing = 0f;
    public float boatsLost;
    public float boatsWin;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boatsLost*5 >= boatsWin)
        {
            //game lost!!
        }
        if(boatsSailing == 0f)
        {
            //end of day/next boat wave
        }

    }
    public void LoseABoat()
    {
        boatsSailing--;
        boatsLost++;
    }
    public void WinABoat()
    {
        boatsSailing--;
        boatsWin++;
    }
}
