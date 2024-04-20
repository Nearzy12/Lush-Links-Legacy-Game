using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public int day;
    private int money;
    public int time;

    public TMP_Text interactionText;
    public TMP_Text toolText;
    public TMP_Text descriptionText;
    public TMP_Text bottomRightText;

    public GameObject axeSpawnLocation;
    public GameObject shovelSpawnLocation;
    public GameObject pickaxeSpawnLocation;
    public GameObject lawnMowerSpawnLocation;

    public GameObject axePreFab;
    public GameObject shovelPreFab;
    public GameObject pickaxePreFab;
    public GameObject lawnMowerPreFab;

    [SerializeField]
    private bool busyOperating;

    private Transform playerTransform;
    private Transform lawnMowerTransform;

    [SerializeField]
    public float lawnMower_position_x;
    [SerializeField]
    public float lawnMower_position_y;
    [SerializeField]
    public float lawnMower_position_z;

    [SerializeField]
    public float lawnMower_rotation_x;
    [SerializeField]
    public float lawnMower_rotation_y;
    [SerializeField]
    public float lawnMower_rotation_z;


    [SerializeField]
    public float player_position_x;
    [SerializeField]
    public float player_position_y;
    [SerializeField]
    public float player_position_z;

    [SerializeField]
    public float player_rotation_x;
    [SerializeField]
    public float player_rotation_y;
    [SerializeField]
    public float player_rotation_z;

    public PlayerMovement playerMovement;
    public Interactor playerInteractor;
    public PlayerLook mowerLook;
    public PlayerLook playerLook;

    public Transform cameraTransform;



    public GameObject player;
    public GameObject lawnMower;

    public int distanceInFront;

    public bool operatingLawnMower;

    private Vector3 targetPosition;

    bool interactorInput;

    public GasManager gasManager;

    // 1 = Axe 2 = Pickaxe 3 = shovel
    private int playerTool;

    // Fuel Variables
    private WaitForSeconds waitTime = new WaitForSeconds(20f);
    [SerializeField]
    private LawnMowerManager mowerManager;

    



    private void Awake()
    {
        //Ensure that this object doesn't get destroyed on load.
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        busyOperating = false;
        operatingLawnMower = false;

        // get scripts that will need manipulating
        playerMovement = player.GetComponent<PlayerMovement>();
        playerInteractor = player.GetComponent<Interactor>();
        playerLook = player.GetComponent<PlayerLook>();

        

        descriptionText.text = "";
        playerTool = 0;

        money = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        if (operatingLawnMower && busyOperating)
        {
            interactorInput = Input.GetKey(KeyCode.Q);
            if(interactorInput)
            {
                getOffMower();
            }
            CalculateMachinePosition();
            UpdateMowerPosition();

            // Stop the mower if it runs out of fuel
            if (mowerManager.getFuelAmount() <= 0)
            {
                getOffMower();
            }

        }
    }

    public void ResetGame()
    {

    }

    public void OperateLawnMower()
    {
        playerInteractor.enabled = false;

        interactionText.text = "";
        descriptionText.text = "Press Q to stop operating Lawn Mower";

        lawnMower = GameObject.FindWithTag("LawnMower");
        mowerManager = lawnMower.GetComponent<LawnMowerManager>();

        lawnMowerTransform = lawnMower.GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();

        lawnMower_position_x = lawnMowerTransform.position.x;
        lawnMower_position_y = lawnMowerTransform.position.y;
        lawnMower_position_z = lawnMowerTransform.position.z;

        // Get the rotation of the lawn mower in Euler angles format
        Vector3 mowerEulerAngles = lawnMowerTransform.rotation.eulerAngles;

        lawnMower_rotation_x = mowerEulerAngles.x;
        lawnMower_rotation_y = mowerEulerAngles.y;
        lawnMower_rotation_z = mowerEulerAngles.z;

        player_position_x = playerTransform.position.x;
        player_position_y = playerTransform.position.y;
        player_position_z = playerTransform.position.z;

        Vector3 playerEulerAngles = playerTransform.rotation.eulerAngles;

        player_rotation_x = playerEulerAngles.x;
        player_rotation_y = playerEulerAngles.y;
        player_rotation_z = playerEulerAngles.z;

        Vector3 mowerPosition = new Vector3(lawnMower_position_x, lawnMower_position_y - 1.8f, lawnMower_position_z - 4);
        Vector3 playerPosition = playerTransform.position;

        player.transform.position = mowerPosition;
        player.transform.eulerAngles = mowerEulerAngles;

        operatingLawnMower = true;
        busyOperating = true;
        StartCoroutine(DecreaseMowerFuel());
        bottomRightText.text = mowerManager.getFuelString();
    }

    public void getOffMower()
    {
        operatingLawnMower = false;
        busyOperating = false;
        descriptionText.text = "";
        bottomRightText.text = "";
        playerInteractor.enabled = true;

    }

    private void CalculateMachinePosition()
    {
        // Get the player's current position and rotation
        Vector3 playerPosition = cameraTransform.position;
        Quaternion playerRotation = transform.rotation;

        // Calculate the forward direction vector based on the player's rotation
        Vector3 forwardDirection = playerRotation * Vector3.forward;

        // Calculate the target position in front of the player
        // targetPosition = playerPosition + forwardDirection * distanceInFront;

        // Vector3 playerPlane = new Vector3(cameraTransform.forward.x, cameraTransform.forward.y, cameraTransform.forward.z);

        targetPosition = playerPosition + player.transform.forward * distanceInFront;

        targetPosition.y -= 1.234f;

        // Debug draw to visualize the target position
        // Debug.DrawLine(playerPosition, targetPosition, Color.red);
    }

    private void UpdateMowerPosition()
    {
        lawnMower.transform.position = targetPosition;

        Quaternion playerRotation = player.transform.rotation;
        //lawnMower.transform.rotation = playerRotation;

        // Get the rotation of the player in Euler angles format
        Vector3 playerEulerAngles = player.transform.rotation.eulerAngles;

        player_rotation_x = playerEulerAngles.x;
        player_rotation_y = playerEulerAngles.y;
        player_rotation_z = playerEulerAngles.z;

        playerEulerAngles.y += 90;

        lawnMower.transform.eulerAngles = playerEulerAngles;
    }

    public IEnumerator DecreaseMowerFuel()
    {
        while (operatingLawnMower && mowerManager.getFuelAmount() > 0) 
        {
            yield return waitTime; // Wait for 20 seconds
            int fuelLevel = mowerManager.getFuelAmount();
            mowerManager.setFuelAmount(fuelLevel - 1);
            //Display updated amount
            bottomRightText.text = mowerManager.getFuelString();
        }
    }

    public int getPlayerTool()
    {
        return playerTool;
    }

    public void setPlayerTool(int tool)
    {
        playerTool = tool;
        if (tool == 0)
        {
            toolText.text = "";
        }
        else if (tool == 1)
        {
            toolText.text = "Axe";
        }
        else if (tool == 2)
        {
            toolText.text = "Pickaxe";
        }
        else if(tool == 3)
        {
            toolText.text = "Shovel";
        }
    }

    public void SpendMoney(int amount)
    {
        Debug.Log("Losing Money");
        money = money - amount;
    }

    public int GetMoney()
    {
        return money;
    }

    public void spawnAxe()
    {
        Instantiate(axePreFab, axeSpawnLocation.transform.position,axeSpawnLocation.transform.rotation);
    }
    public void spawnShovel()
    {
        Instantiate(shovelPreFab, shovelSpawnLocation.transform.position, shovelSpawnLocation.transform.rotation);
    }
    public void spawnPickaxe()
    {
        Instantiate(pickaxePreFab, pickaxeSpawnLocation.transform.position, pickaxeSpawnLocation.transform.rotation);
    }
    public void spawnMower()
    {
        Instantiate(lawnMowerPreFab, lawnMowerSpawnLocation.transform.position, lawnMowerSpawnLocation.transform.rotation);
    }
    public void addGas()
    {
        int currentGas = gasManager.getFuelAmount();
        
    }
}
