using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D playerBody;

    [SerializeField] GameObject victoryPanel;

    [SerializeField] AudioSource coinSound;

    CoinsSaver coinsave;
    
    int maxIndexLevels = 7;

    void Start()
    {
        playerBody = player.GetComponent<Rigidbody2D>();
        coinsave = FindObjectOfType<CoinsSaver>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Coin"))
        {
            coinsave.AddCoins();
            coinSound.Play();
            Destroy(gameObject);
        }

        else if (gameObject.CompareTag("Cup"))
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            if (SceneManager.GetActiveScene().buildIndex != maxIndexLevels)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            victoryPanel.SetActive(true);
        }
    }
}
