using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer spriteRenderer;
    
    Vector2 direction;
    [SerializeField] float speed;
    const float inverse = -1.0f;
    bool isMoving = false;
    bool isLookingLeft = true;
    
    bool isOnFloor = true;

    [SerializeField] GameObject controlsPanel;
    
    [SerializeField] AudioSource tireSound;
    [SerializeField] AudioSource engineSound;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        PlayerMovements();

        if (Time.timeScale == 0)
        {
            engineSound.Stop();
        }
    }

    void PlayerMovements()
    {
        if (Input.GetButtonDown("ChangeDirection") && !isMoving)
        {
            direction = new Vector2( speed * inverse, speed);
            isMoving = true;
            controlsPanel.SetActive(false);
            engineSound.Play();
        }
        else if (Input.GetButtonDown("ChangeDirection") && isMoving && isLookingLeft && Time.timeScale > 0)
        {
            direction = new Vector2(body.velocity.x * inverse, body.velocity.y);
            transform.Rotate (Vector3.forward * -90);
            isLookingLeft = false;
            tireSound.Play();
        }
        else if (Input.GetButtonDown("ChangeDirection") && isMoving && !isLookingLeft && Time.timeScale > 0)
        {
            direction = new Vector2(body.velocity.x * inverse, body.velocity.y);
            transform.Rotate (Vector3.forward * 90);
            isLookingLeft = true;
            tireSound.Play();
        }
        else if (Input.GetButtonDown("ChangeDirection") && Time.timeScale == 0)
        {
            Application.Quit();
        }

        body.velocity = direction;
    }
}
