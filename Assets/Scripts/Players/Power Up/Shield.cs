using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] bool shield;

    void Start()
    {
        if (!shield)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
    public void ShieldOn()
    {
        if (!shield)
        {
            gameObject.SetActive(true);
            shield = true;
        }
        GetComponentInParent<PlayerController>().shieldUp();
        Debug.Log("ShieldUp");
        if (shield)
        {
            gameObject.SetActive(false);
            shield = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Oil")
        {
            collision.GetComponent<Skid>().ShieldBroke();
            ShieldOn();
        }
    }
}
