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
    [SerializeField] float breakHit;
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
    
    Vector2 startPosition;
    Vector2 endPosition;
    float swipeDistanceThreshold = 100.0f;
    float swipeThreshold = 100.0f;
    float movementSizeToChangeWay = 0.3f;

    float timer = 0.2f;

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
            Time.timeScale = 1;
            direction = new Vector2( speed * inverse, speed);
            isMoving = true;
            controlsPanel.SetActive(false);
            engineSound.Play();
        }

        if (!isMoving)
            return;
        
        body.velocity = direction;

        timer -= Time.deltaTime;
        if (timer > 0)
            return;
        
        CheckSlide();
        CheckClic();

        
    }

    void CheckClic()
    {
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Ended && touch.position.x < Screen.width/2 && isMoving && Time.timeScale > 0 && !Lock)
            {
                transform.position -= transform.right * movementSizeToChangeWay;
            }
            else if (touch.phase == TouchPhase.Ended && touch.position.x > Screen.width/2 && isMoving && Time.timeScale > 0 && !Lock)
            {
                transform.position += transform.right * movementSizeToChangeWay;
            } 
        }
    }

    void CheckSlide()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endPosition = touch.position;
                    AnalyzeGesture(startPosition, endPosition);
                    break;
            }
        }
        
    }
    
    void AnalyzeGesture(Vector2 start, Vector2 end)
    {
        if(Vector2.Distance(start, end) > swipeDistanceThreshold)
        {
            var leftToRight = start.x < end.x;
            var rightToLeft = start.x > end.x;
            
            if(leftToRight)
            {
                SwipeLeft();
            }
            else if (rightToLeft)
            {
                SwipeRight();
            }
        }
    }

    void SwipeLeft()
    {
        if (isMoving && isLookingLeft && Time.timeScale > 0 && !Lock)
        {
            direction = new Vector2(body.velocity.x * inverse, body.velocity.y);
            transform.Rotate (Vector3.forward * -90);
            isLookingLeft = false;
            tireSound.Play();
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.3f;
        }
    }

    void SwipeRight()
    {
        if (isMoving && !isLookingLeft && Time.timeScale > 0 && !Lock)
        {
            direction = new Vector2(body.velocity.x * inverse, body.velocity.y);
            transform.Rotate (Vector3.forward * 90);
            isLookingLeft = true;
            tireSound.Play();
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.7f;
        }
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
        yield return new WaitForSeconds(Cooldown);
        isSkid = false;
        if (!isLookingLeft)
        {
            direction = new Vector2(speed, speed);
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        if (isLookingLeft)
        {
            direction = new Vector2(speed * inverse, speed);
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        Lock = false;
    }

    public void BreakHit()
    {
        if (!shield)
        {
            Lock = true;
            if (!isLookingLeft)
            {
                direction = new Vector2(breakHit, breakHit);
            }
            if (isLookingLeft)
            {
                direction = new Vector2(breakHit * inverse, breakHit);
            }
            tireSound.Play();
            StartCoroutine(BreakCooldown());
        }
    }
    IEnumerator BreakCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        if (!isLookingLeft)
        {
            direction = new Vector2(speed, speed);
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        if (isLookingLeft)
        {
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
