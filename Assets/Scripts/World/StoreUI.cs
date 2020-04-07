using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    CoinsSaver coinsave;
    int coins = 0;
    [SerializeField] TextMeshProUGUI coinsUI;

    void Start()
    {
        coinsave = FindObjectOfType<CoinsSaver>();
    }

    void Update()
    {
        coinsUI.text = coinsave.ReturnNbrOfCoins().ToString();
    }
}
