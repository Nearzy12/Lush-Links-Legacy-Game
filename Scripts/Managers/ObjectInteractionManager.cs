using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionManager : MonoBehaviour
{
    // Start is called before the first frame update
    // If the object does not use certain variables then they will be set to NULL or -1;
    public int object_health;
    public string interaction_text;
    public string interaction_button;

    public Transform tree_location;
    public float tree_rotation_x;
    public float tree_rotation_y;
    public float tree_rotation_z;

    private float tree_location_x;
    private float tree_location_y;
    private float tree_location_z;

    private Vector3 newTreePosition;
    private Vector3 newTreeRoation;

    public float first_tree_location_y_offset;
    public float second_tree_location_y_offset;

    public GameObject stumpPreFab;
    public GameObject logPreFab;

    private GameObject gameMangerObj;

    [SerializeField]
    private TimeManager timeManager;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GasManager gsManager;

    private int playerTool;

    void Start()
    {
        gameMangerObj = GameObject.FindWithTag("GameManager");
        gameManager = gameMangerObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function to take away the health of the object
    public void hitObject(int damage)
    {
        object_health = object_health - damage;

        // if the object health is now 0, then desroy object
        if(object_health == 0 && tag == "Tree")
        {
 
            // Object is a tree
            tree_location_x = gameObject.GetComponent<Transform>().position.x;
            tree_location_y = gameObject.GetComponent<Transform>().position.y;
            tree_location_z = gameObject.GetComponent<Transform>().position.z;
            tree_location = gameObject.GetComponent<Transform>();
            Destroy(gameObject.transform.parent.gameObject);
            //Destroy(gameObject);

            newTreePosition = new Vector3(tree_location_x, tree_location_y + first_tree_location_y_offset, tree_location_z);
            // newTreeRoation = new Vector3(tree_roation_x, tree_rotation_y, tree_rotation_z);

            // Create two log objects and a stump at the location that the tree just was
            // Start of with making a stump
            Instantiate(stumpPreFab, tree_location.position, tree_location.rotation);

            tree_location.rotation = Quaternion.Euler(tree_rotation_x, tree_rotation_y, tree_rotation_z);
            tree_location.position = newTreePosition;

            Instantiate(logPreFab, tree_location.position, tree_location.rotation);

            newTreePosition = new Vector3(tree_location_x, tree_location_y + second_tree_location_y_offset, tree_location_z);
            tree_location.position = newTreePosition;

            Instantiate(logPreFab, tree_location.position, tree_location.rotation);

        }

        else if(object_health == 0 && tag == "Rock")
        {
            Destroy(gameObject);
        }

        // Log
        else if(object_health == -2) 
        {
            // Add object to inventory then destory it 
            // Add a tag to the object and then set up an inventory in player manager
            // Player manager will have a function to add an object to the inventory
            // object is getting picked up
            Destroy(gameObject);
        }

        // Stump Object
        else if (object_health == -3)
        {
            // Just remove stump
            Destroy(gameObject);
        }

        // Bed is pressed
        else if (object_health == -4)
        {
            // Sleep
            timeManager.advanceDay();
            object_health++;
        }

        // Lawn Mower activated
        else if (object_health == -5)
        {
            // Lawn mower logic
            Debug.Log("Operate Lawn Mower");
            object_health++;
            gameManager.OperateLawnMower();
        }

        // Pick Up Axe
        else if(object_health == -6)
        {
            object_health++;
            gameManager.setPlayerTool(1);
        }

        // Pick Up Pickaxe
        else if (object_health == -7)
        {
            object_health++;
            gameManager.setPlayerTool(2);
        }

        // Pick up Shovel
        else if (object_health == -8)
        {
            object_health++;
            gameManager.setPlayerTool(3);
        }

        // Interact with gas tank
        else if (object_health == -9)
        {
            object_health++;
            string fuelAmount = gsManager.getFuelAmount().ToString();
            string maxFuelAmount = gsManager.getmaxFuelAmount().ToString();

            string displayText = "Fuel Level: " + fuelAmount + "L / " + maxFuelAmount + "L";
            gameManager.descriptionText.text = displayText;
            // Fill up object that is near by

        }

        else
        {
            Debug.Log("This logic isn't implemented yet");
        }
    }

    public int getObjectHealth()
    {
        return object_health;
    }

    public string getObjectText()
    {
        return interaction_text;
    }

    public string getInteractionButton()
    {
        return interaction_button;
    }

    public void setInteractionText(string text)
    {
        interaction_text = text;
    }
}
