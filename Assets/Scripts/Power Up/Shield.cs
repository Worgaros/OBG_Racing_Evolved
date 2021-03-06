﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] bool shield;
    [SerializeField] bool broke;
    
    CoinsSaver coinsave;

    int shieldPrice = 5;

    void Start()
    {
        broke = false;
        coinsave = FindObjectOfType<CoinsSaver>();
        shield = false;
        ShieldOn();
    }

    void Update()
    {
        
    }
    public void ShieldOn()
    {
        if (!shield && coinsave.ReturnNbrOfCoins() >= shieldPrice)
        {
            coinsave.RemoveCoins(shieldPrice);
            shield = true;
            GetComponentInParent<PlayerController>().shieldUp();
        }
        if (broke)
        {
            gameObject.SetActive(false);
            shield = false;
            GetComponentInParent<PlayerController>().shieldUp();
        }
       else if (!shield && coinsave.ReturnNbrOfCoins() < shieldPrice) 
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Oil")
        {
            broke = true;
            collision.GetComponent<Skid>().ShieldBroke();
            ShieldOn();
        }
    }
    public void test()
    {
        
    }
}
