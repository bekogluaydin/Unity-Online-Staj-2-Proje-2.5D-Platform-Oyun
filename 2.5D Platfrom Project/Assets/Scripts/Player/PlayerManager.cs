using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static int numberOfCoins, maxHealth = 100;
    public static float currentHealth = 0;
    public TextMeshProUGUI numberOfCoinsText;
    public Slider healthBar;
    public static bool gameOver, winLevel, reducingHealth;
    public GameObject gameOverPanel;

    public GameObject nextLevelButton;
    void Start()
    {
        numberOfCoins = 0;
        gameOver = winLevel = reducingHealth = false;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfCoinsText.text = "Coins: " + numberOfCoins;

        healthBar.value = currentHealth;

        //game over
        if (reducingHealth)
        {
            currentHealth -= 40 * Time.deltaTime;
        }

        if (currentHealth < 0)
        {
            gameOver = true; 
        }
        if (gameOver)
        {
            GameOver();
        }

        if (FindObjectsOfType<Enemy>().Length == 0 && PlayerController.atTheDoor)
        {
            winLevel = true;

            //if (winLevel)
            //{
            //    LevelManager.GetLevel();
            //    nextLevelButton.SetActive(true);
            //}
        }       
    }

    public void GameOver()
    {
        Destroy(gameObject);
        gameOverPanel.SetActive(true);        
    }
}
