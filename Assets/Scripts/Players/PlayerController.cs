using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer spriteRenderer;

    Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] float lowSpeed;
    [SerializeField] float normalSpeed;
    [SerializeField] float nitroSpeed;

    const float inverse = -1.0f;
    bool isMoving = false;
    bool isLookingLeft = true;
    
    bool isOnFloor = true;

    bool canTurn = true;

    bool isSkid = false;
    bool Lock = false;
    [SerializeField] float speedRot;
    [SerializeField] float Cooldown;

    [SerializeField] GameObject controlsPanel;
    
    [SerializeField] AudioSource tireSound;
    [SerializeField] AudioSource engineSound;

    [SerializeField] CinemachineVirtualCamera vcam;

    [SerializeField] bool shield;
    [SerializeField] bool StoreUp;

    const int nitroPrice = 10;
    CoinsSaver coinsave;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        StoreUp = true;
        coinsave = FindObjectOfType<CoinsSaver>();
    }
    
    void Update()
    {
        PlayerMovements();

        if (Time.timeScale == 0)
        {
            engineSound.Stop();
        }
        if (isSkid)
        {
            transform.Rotate(new Vector3(0, 0, speedRot * Time.deltaTime));
        }
    }

    void PlayerMovements()
    {
        
        
        if (Input.GetMouseButtonDown(0) && !isMoving && !StoreUp)
        {
            direction = new Vector2( speed * inverse, speed);
            isMoving = true;
            controlsPanel.SetActive(false);
            engineSound.Play();
            Time.timeScale = 1;

        }
        else if (Input.GetMouseButtonDown(0) && isMoving && isLookingLeft && Time.timeScale > 0 && !Lock)
        {
            direction = new Vector2(body.velocity.x * inverse, body.velocity.y);
            transform.Rotate (Vector3.forward * -90);
            isLookingLeft = false;
            tireSound.Play();
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.3f;
        }
        else if (Input.GetMouseButtonDown(0) && isMoving && !isLookingLeft && Time.timeScale > 0 && !Lock)
        {
            direction = new Vector2(body.velocity.x * inverse, body.velocity.y);
            transform.Rotate (Vector3.forward * 90);
            isLookingLeft = true;
            tireSound.Play();
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.7f;
        }
        

        body.velocity = direction;
    }
    public void IsSkid()
    {
        if (!shield)
        {
            isSkid = true;
            Lock = true;
            if (!isLookingLeft)
            {
                direction = new Vector2(lowSpeed, lowSpeed);
            }
            if (isLookingLeft)
            {
                direction = new Vector2(lowSpeed * inverse, lowSpeed);
            }
            tireSound.Play();
            StartCoroutine(cooldown());
        }
    }
    IEnumerator cooldown()
    {
        Debug.Log("cooldown");
        yield return new WaitForSeconds(Cooldown);
        Debug.Log("stopSkid");
        isSkid = false;
        if (!isLookingLeft)
        {
            Debug.Log(isLookingLeft);
            direction = new Vector2(speed, speed);
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        if (isLookingLeft)
        {
            Debug.Log(isLookingLeft);
            direction = new Vector2(speed * inverse, speed);
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        Lock = false;
    }
    public void shieldUp()
    {
        if (!shield)
        {
            shield = true;
        }
        else if (shield)
        {
            shield = false;
        }
    }
    public void nitro()
    {
        if (speed == normalSpeed && coinsave.ReturnNbrOfCoins() >= nitroPrice)
        {
            coinsave.RemoveCoins(nitroPrice);
            speed = nitroSpeed;
        }
    }
    public void storeDown()
    {
        StoreUp = false;
    }
}
