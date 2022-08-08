using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_movement2 : MonoBehaviour
{
    public float speed, extraSpeed, currentSpeed, jump, currentTime;
    public Rigidbody RB;
    public bool timeRunning, isGrounded, running, crowd;
    public Slider sliderHP, sliderSweat;
    public Gradient gradient;
    public Image fillHP, fillSweat;
    public GameObject sweatLittle, sweatLot;
    public int sweatMuch, sweatPoints;
    public float timerSweat = 0;


    void Start()
    {
        speed = 5f;
        extraSpeed = 10f;
        currentSpeed = 0f;
        jump = 10f;
        currentTime = timerSweat;
        
    }

    void Update()
    {

        
        //Points
        int points = checkPoints.currentPoints;
        sliderHP.value = points;
        fillHP.color = gradient.Evaluate(sliderHP.normalizedValue);

        //Movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 movement = (transform.right * x) + (transform.forward * z);
        transform.position += movement * speed * Time.deltaTime;


        //SpeedUp
        if (Input.GetKey(KeyCode.X))
        {
            
            //currentTime += 1 * Time.deltaTime;
            if (!crowd)
            {
                running = true;
                speed = 10f;
                if (running)
                {
                    currentTime += Time.deltaTime;
                }
                if (currentTime > 3)
                {
                    sweatPoints += 3;
                }  
            }
            if (crowd)
            {
                speed = 5f;
                if (currentTime > 2)
                {
                    sweatPoints += 5;
                }

            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (!crowd)
            {
                running = false;
                speed = 5f;
                sweatPoints = 0;
                if (!running)
                {
                    currentTime = 0;
                }
            }
            if (crowd)
            {
                speed = 2f;
                sweatPoints = 0;
            }
        }


        //Jump
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                RB.velocity = new Vector2(RB.velocity.x, 0);
                RB.AddForce(new Vector2(0, jump), ForceMode.Impulse);
                isGrounded = false;
                Debug.Log("Jump");

            }
        }


        //Sweat
        if (sweatPoints > 3)
        {
            sweatLittle.gameObject.SetActive(true);
            sweatLot.gameObject.SetActive(true);

        }
        if (sweatPoints > 8)
        {
            sweatLittle.gameObject.SetActive(false);
            sweatLot.gameObject.SetActive(true);
        }
        else
        {
            sweatLittle.gameObject.SetActive(false);
            sweatLot.gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "Car")
        {
            checkPoints.currentPoints -= 5;
        }
        if (col.gameObject.tag == "Obstacle")
        {
            checkPoints.currentPoints -= 1;
        }
        if (col.gameObject.tag == "Crowd")
        {
            crowd = true;
            speed = 2f;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
        if (col.gameObject.tag == "Crowd")
        {
            crowd = false;
            speed = 5f;
        }
    }

}
