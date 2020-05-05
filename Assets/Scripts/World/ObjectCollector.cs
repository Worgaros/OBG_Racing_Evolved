using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D playerBody;

    [SerializeField] GameObject victoryPanel;

    [SerializeField] AudioSource coinSound;

    CoinsSaver coinsave;

    float minPitch = 0.3f;
    float maxPitch = 1.3f;

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
            coinSound.pitch = Random.Range(minPitch, maxPitch);
            coinSound.Play();
            Destroy(gameObject);
        }

        else if (gameObject.CompareTag("Cup"))
        {
            Destroy(gameObject);
            Time.timeScale = 0.001f;
            victoryPanel.SetActive(true);

            if (SceneManager.GetActiveScene().buildIndex != maxIndexLevels)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
