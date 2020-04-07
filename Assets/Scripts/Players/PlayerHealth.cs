using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int Health;

    [SerializeField] bool armoredUp = false;
    int armor = 2;

    void Start()
    {
       if (armoredUp)
        {
            Health += armor;
        }
    }

    // Update is called once per frame
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
        armoredUp = true;
    }
}
