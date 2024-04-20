using System.Collections;
using System.Collections.Generic;
//using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float currentHealth = 100;
    public int maxHealth = 100;
    public int healthGenRate = 10;
    public float timeSinceLastIncrease = 0.0f;
    //public GameManager gameManagerObject;
    //private GameManager gameManager;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastIncrease += Time.deltaTime;
        UpdateHealthBar();
        // Increase health every 5 seonds
        if (timeSinceLastIncrease >= 5.0f && currentHealth < maxHealth) 
        {
            IncreaseHealth();
            // Reset the timer
            timeSinceLastIncrease = 0.0f;
        }
    }

    public void Hit(float damage)
    {
        timeSinceLastIncrease = 0f;
        // Debug.Log("Player Hit: " + currentHealth);
        currentHealth = currentHealth - damage;

        if(currentHealth <= 0)
        {
            PlayerDeath();
        }
    }
    private void PlayerDeath()
    {
        // Debug.Log("The player has died");
        //gameManager.GameOver();
    }

    private void IncreaseHealth()
    {
        currentHealth += healthGenRate;
        Debug.Log("Player health increased to: " + currentHealth);
    }

    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Normalize health to a value between 0 and 1
            healthBar.value = (float)currentHealth / maxHealth; 
        }
    }
}
