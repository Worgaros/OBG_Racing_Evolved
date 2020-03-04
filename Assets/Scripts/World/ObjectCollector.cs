using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D playerBody;
    
//    int coinsNumber;
    
    [SerializeField] GameObject victoryPanel;
    
    [SerializeField] AudioSource coinSound;
    
//    [SerializeField] TextMeshProUGUI collectedCoinsUI;

    void Start()
    {
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Coin"))
        {
//            coinsNumber++;
            coinSound.Play();
            Destroy(gameObject);
        }

        else if (gameObject.CompareTag("Cup"))
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            victoryPanel.SetActive(true);
//            collectedCoinsUI.text = coinsNumber.ToString();
        }
    }
}
