using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int Health;
    [SerializeField] int maxHealth;

    [SerializeField] bool armoredUp = false;
    int armor = 2;
    
    CoinsSaver coinsave;

    public Image[] heart;
    public Sprite fullHeart;
    public Sprite emptyHealth;

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

        if (Health > maxHealth)
        {
            Health = maxHealth;
        }

        for (int i = 0; i < heart.Length; i++)
        {
            if (i < Health)
            {
                heart[i].sprite = fullHeart;
            }
            else
            {
                heart[i].sprite = emptyHealth;
            }
            if (i < maxHealth)
            {
                heart[i].enabled = true;
            }
            else
            {
                heart[i].enabled = false;
            }
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
