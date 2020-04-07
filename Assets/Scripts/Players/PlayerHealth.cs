using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int Health;

    [SerializeField] bool armoredUp = false;
    int armor = 2;
    
    CoinsSaver coinsave;

    const int armorPrice = 15;

    void Start()
    {
        coinsave = FindObjectOfType<CoinsSaver>();
    }
    
    void Update()
    {
        if (Health < 0)
        {
            Health = 0;
        }
        if (Health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void Hiting()
    {
        Health--;
    }
    public void armored()
    {
        if (!armoredUp && coinsave.ReturnNbrOfCoins() >= armorPrice)
        {
            coinsave.RemoveCoins(armorPrice);
            Health += armor;
            armoredUp = true;
        }
    }
}
