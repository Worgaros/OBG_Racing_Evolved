﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] bool shield;
    [SerializeField] bool Broke;

    void Start()
    {
        Broke = false;
    }

    void Update()
    {
        
    }
    public void ShieldOn()
    {
        if (!shield)
        {
            shield = true;
            GetComponentInParent<PlayerController>().shieldUp();
        }
        if (Broke)
        {
            gameObject.SetActive(false);
            shield = false;
            GetComponentInParent<PlayerController>().shieldUp();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Oil")
        {
            Broke = true;
            collision.GetComponent<Skid>().ShieldBroke();
            ShieldOn();
        }
    }
}
