using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skid : MonoBehaviour
{
    [SerializeField] bool oil;
    [SerializeField] bool barrel;
    [SerializeField] GameObject explosion;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" & oil) 
        {
            collision.GetComponent<PlayerController>().IsSkid();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" & barrel)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            collision.GetComponent<PlayerHealth>().Hiting();
            collision.GetComponent<PlayerController>().BreakHit();
            Destroy(gameObject);
        }
    }
    public void ShieldBroke()
    {
        Destroy(gameObject);
    }
}
