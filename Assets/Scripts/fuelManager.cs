using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuelManager : MonoBehaviour
{
    public float fuel;
    public bool light_OnOf;
    public LHController lightHouseController;
    
    // Start is called before the first frame update
    void Start()
    {
        fuel = 100f;   
    }

    // Update is called once per frame
    void Update()
    {
        if(light_OnOf)
        {
            if (fuel >= 0f)
            {   fuel = fuel - Time.deltaTime;
                lightHouseController.fueledUp = true;
            }
        }
        if (fuel <= 0f)
            lightHouseController.fueledUp = false;
        
    }

    public void Refuel()
    {
        //adds fuel to tank
    }
}
