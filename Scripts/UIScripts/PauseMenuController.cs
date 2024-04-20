using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject shopCanvas;
    public GameObject progressionCanvas;

    public bool axeBought = false;
    public bool shovelBought = false;
    public bool pickaxeBought = false;
    public bool mowerBought = false;

    private int playerMoney;

    public GameManager gameManager;

    public PlayerLook playerLook;
    
    [SerializeField]
    private bool isPaused;

    public TMP_Text moneyText;

    void Start()
    {
        //Hide the UI since the game is not paused by default
        PauseGame(false);
        shopCanvas.SetActive(false);
        progressionCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }
    
    public void PauseGame(bool paused)
    {
        if (paused)
        { 
            //Show the pause menu
           pauseCanvas.SetActive(true);
           Cursor.lockState = CursorLockMode.None;
           Cursor.visible = true;

        }
        else
        {
           //Hide the pause menu
           Debug.Log("Game should be resuming");
           pauseCanvas.SetActive(false);
           shopCanvas.SetActive(false);
            progressionCanvas.SetActive(false);
           Cursor.lockState = CursorLockMode.Locked;
           Cursor.visible = false;
        }
        
        isPaused = paused;
        
        Time.timeScale = paused ? 0 : 1;
    }

    public void LoadScene(string sceneName)
    {

    }

    public void loadShopUI()
    {
        pauseCanvas.SetActive(false);
        progressionCanvas.SetActive(false);
        shopCanvas.SetActive(true);
        playerMoney = gameManager.GetMoney();
        moneyText.text = "$" + playerMoney.ToString();
        Debug.Log(playerMoney);
    }

    public void loadPauseUI()
    {
        pauseCanvas.SetActive(true);
        progressionCanvas.SetActive(false);
        shopCanvas.SetActive(false);
    }

    public void loadProgressionUI()
    {
        pauseCanvas.SetActive(false);
        progressionCanvas.SetActive(true);
        shopCanvas.SetActive(false);
    }

    public void buyAxe()
    {
        if(axeBought == false)
        {
            playerMoney = gameManager.GetMoney();
            if(playerMoney > 50)
            {
                // Create the Axe object
                gameManager.spawnAxe();
                axeBought = true;
                gameManager.SpendMoney(50);
                moneyText.text = "$" + ((playerMoney-50).ToString());
            }
        }
    }

    public void buyShovel()
    {
        if (shovelBought == false)
        {
            playerMoney = gameManager.GetMoney();
            if (playerMoney > 50)
            {
                // Create the Axe object
                gameManager.spawnShovel();
                shovelBought = true;
                gameManager.SpendMoney(50);
                moneyText.text = "$" + ((playerMoney - 50).ToString());
            }
        }
    }

    public void buyPickaxe()
    {
        if (pickaxeBought == false)
        {
            playerMoney = gameManager.GetMoney();
            if (playerMoney > 50)
            {
                // Create the Axe object
                gameManager.spawnPickaxe();
                pickaxeBought = true;
                gameManager.SpendMoney(50);
                moneyText.text = "$" + ((playerMoney - 50).ToString());
            }
        }
    }

    public void buyGas()
    {

    }

    public void buyMower()
    {
        if (mowerBought == false)
        {
            playerMoney = gameManager.GetMoney();
            if (playerMoney > 500)
            {
                // Create the Axe object
                gameManager.spawnMower();
                mowerBought = true;
                gameManager.SpendMoney(500);
                moneyText.text = "$" + ((playerMoney - 500).ToString());
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
