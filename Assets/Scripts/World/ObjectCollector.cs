using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D playerBody;

    [SerializeField] GameObject victoryPanel;

    [SerializeField] AudioSource coinSound;

    CoinsSaver coinsave;

    //float maxTime = 3.0f;
    //float time = 0;
    
    int maxIndexLevels = 7;

    void Start()
    {
        playerBody = player.GetComponent<Rigidbody2D>();
        coinsave = FindObjectOfType<CoinsSaver>();
    }

    /*private void Update()
    {
        time -= Time.deltaTime;
    }*/

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
            victoryPanel.SetActive(true);

            //time = maxTime;

            if (SceneManager.GetActiveScene().buildIndex != maxIndexLevels /*&& time <= 0*/)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
