using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSaver : MonoBehaviour
{
    int coins = 0;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void AddCoins()
    {
        coins++;
    }

    public void RemoveCoins(int nbr)
    {
        coins -= nbr;
    }

    public int  ReturnNbrOfCoins()
    {
        return coins;
    }
}
