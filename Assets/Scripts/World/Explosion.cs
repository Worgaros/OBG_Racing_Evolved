using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
