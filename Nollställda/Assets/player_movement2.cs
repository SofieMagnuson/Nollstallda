using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_movement2 : MonoBehaviour
{
    public float speed, extraSpeed, currentSpeed, jump, currentTime;
    public Rigidbody RB;
    public bool timeRunning, isGrounded, running, crowd, drugs, office, buyingDrugs, finalScene;
    public Slider sliderHP, sliderSweat;
    public Gradient gradient;
    public Image fillHP, fillSweat;
    public GameObject sweatLittle, sweatLot, buisness, casual, sweats, apotekG;
    public int sweatMuch, sweatPoints, points;
    public float timerSweat = 0, timer;
    public Text timerText;


    void Start()
    {
        speed = 5f;
        extraSpeed = 10f;
        currentSpeed = 0f;
        jump = 10f;
        currentTime = timerSweat;
        timer = 90f;
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();
    }

    void Update()
    {
        //Timer
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            Lose();
        }
        CheckTime();
        DisplayTime(timer);


        //Points
        int points = checkPoints.currentPoints;
        sliderHP.value = points;
        fillHP.color = gradient.Evaluate(sliderHP.normalizedValue);

        //Clothes
        int dressed = checkPoints.dressed;
        if (dressed == 1)
        {
            buisness.gameObject.SetActive(true);
        }
        if (dressed == 2)
        {
            casual.gameObject.SetActive(true);
        }
        if (dressed == 3)
        {
            sweats.gameObject.SetActive(true);
        }



        //Movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 movement = (transform.right * x) + (transform.forward * z);
        transform.position += movement * speed * Time.deltaTime;


        //SpeedUp
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            if (!crowd)
            {
                running = true;
                speed = 10f;
                if (running)
                {
                    currentTime += Time.deltaTime;
                    if (currentTime > 3)
                    {
                        sweatPoints += 3;
                        currentTime = 0;
                    }
                }
            }
            if (crowd)
            {
                speed = 5f;
                //sweatPoints += 5;


            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (!crowd)
            {
                running = false;
                speed = 5f;
                //sweatPoints = 0;
                if (!running)
                {
                    currentTime = 0;
                }
            }
            if (crowd)
            {
                speed = 2f;
                //sweatPoints = 0;
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
            }
        }


        //Sweat
        if (sweatPoints >= 3)
        {
            sweatLittle.gameObject.SetActive(true);
            sweatLot.gameObject.SetActive(true);


        }
        if (sweatPoints >= 8)
        {
            sweatLittle.gameObject.SetActive(false);
            sweatLot.gameObject.SetActive(true);

        }
        else
        {
            sweatLittle.gameObject.SetActive(false);
            sweatLot.gameObject.SetActive(false);
        }


        //Apotek
        if (drugs)
        {
            checkPoints.currentPoints += 10;
            drugs = false;
            Destroy(apotekG);
        }

    }
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    void CheckTime()
    {
        if (timer == 10)
        {
            checkPoints.currentPoints -= 10;
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
        if (col.gameObject.tag == "Apotek")
        {
            drugs = true;
        }
        if (col.gameObject.tag == "Office")
        {
            office = true;
            LoadNextScene();
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
        if (col.gameObject.tag == "Apotek")
        {
            drugs = false;
        }
        if (col.gameObject.tag == "Office")
        {
            office = false;
        }
    }
    public void LoadNextScene()
    {
        //Add wait 5 sec and freeze 5 sec
        SceneManager.LoadScene("LastScene");
    }

    void Lose()
    {
        SceneManager.LoadScene("Lose");
    }

}
