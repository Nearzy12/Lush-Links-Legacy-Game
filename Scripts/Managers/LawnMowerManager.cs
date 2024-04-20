using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class LawnMowerManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    private int fuelAmount;
    private int maxFuelAmount;

    // Start is called before the first frame update
    void Start()
    {
        maxFuelAmount = 100;
        fuelAmount = 100;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        // If this collision happens this means the player object that fired it was it.
        // If the object that is hit is the enemy object, then make the player it and make the enemy not it

        if (collision.gameObject.tag == "Grass")
        {
            collision.gameObject.SetActive(false);
        }
    }

    public void setFuelAmount(int amount)
    {
        fuelAmount = amount;
    }

    public int getFuelAmount()
    {
        return fuelAmount;
    }

    public void setMaxFuelAmount(int amount)
    {
        maxFuelAmount = amount;
    }

    public int getmaxFuelAmount()
    {
        return maxFuelAmount;
    }

    public string getFuelString()
    {
        string returnString;
        string fuelLevelString = fuelAmount.ToString();
        string maxFuelLevelString = maxFuelAmount.ToString();

        returnString = "Gas: " + fuelLevelString + "L / " + maxFuelLevelString + "L";

        return returnString;
    }

}
