using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skid : MonoBehaviour
{
    [SerializeField] bool oil;
    [SerializeField] bool barrel;


    void Start()
    {

    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // bool oil futur
        {
            collision.GetComponent<PlayerController>().IsSkid();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" & barrel)
        {
            collision.GetComponent<PlayerHealth>().Hiting();
        }
    }
    public void ShieldBroke()
    {
        Destroy(gameObject);
    }
}
