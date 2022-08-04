using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class objects_interact : MonoBehaviour
{
    public bool dressed, checkPills, eatSandwich, closeSandwich, closeWardrobe, closeToilet, closeZink, closeShower, closePills, pee, showering, washing, peedone, exitOn;
    public GameObject  wardrobe, pills, story, sandwich, toilet, shower, zink, exit, clothes, test;
    public int CountdownTime, points, currentPoints;
    public player_movement PL;
    public Text pointsText, timerText;

    public float timer = 0;


    void Start()
    {
        points = 0;
        currentPoints = 0;
        timer = 90f;
    }

    void Update()
    {
        CheckObjects();
        DelayTime();
        pointsText.text = points.ToString() + " POINTS";
        
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        DisplayTime(timer);

    }

    void DisplayTime (float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    void CheckObjects()
    {
        if (closePills && story.gameObject.activeSelf == true)
        {
            PL.FreezeON();
            checkPills = true;
            if (checkPills)
            {
                Destroy(pills);
                if (story.gameObject.activeSelf == false)
                {
                    PL.SpeedBack();
                }
            }

        }

        if (closeSandwich && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.collider.tag == "Sandwich")
                {
                    eatSandwich = true;
                    points += 1;
                    timer -= 5;
                    Destroy(sandwich);
                }
        }

        if (closeToilet && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && !pee)
            {
                if (hit.collider.tag == "Toilet")
                {
                    pee = true;
                    if (pee)
                    {
                        points += 1;
                        timer -= 5;
                    }
                }
            }
        }

        if (closeShower && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && !showering)
            {
                if (hit.collider.tag == "Shower")
                    showering = true;
                    if (showering)
                    {
                        points += 1;
                        timer -= 5;
                    }

            }
        }

        if (closeZink && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && !washing)
            {
                if (hit.collider.tag == "Zink")
                {
                    washing = true;
                    if (washing)
                    {
                        points += 1;
                        timer -= 5;
                    }
                    
                }
            }
        }

        if (closeWardrobe && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Wardrobe" && !dressed)
                {
                    clothes.gameObject.SetActive(true);
                    PL.FreezeON();
                    dressed = true;
                }
            }
        }
        if (exitOn && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Exit" && dressed)
                {
                    PL.FreezeON();
                    LoadNextScene();
                }
            }
        }

    }

    public void CheckCurrentPoints()
    {
        currentPoints = points;
    }

    void DelayTime()
    {
        if (pee || showering || washing || eatSandwich)
        {
            PL.Freeze();
        }
    }

    public void CheckBuisness()
    {
        points += 10;
        timer -= 30;
    }
    public void CheckCasual()
    {
        points += 5;
        timer -= 15;
    }
    public void CheckSweat()
    {
        points += 2;
        timer -= 5;
    }

    public void FreezeDone()
    {
        pee = false;
        showering = false;
        washing = false;
        eatSandwich = false;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Pills" && !checkPills)
        {
            closePills = true;
            pills.gameObject.SetActive(true);
        }
        if (col.gameObject.tag == "Sandwich" && checkPills)
        {
            closeSandwich = true;
        }
        if (col.gameObject.tag == "Toilet" && checkPills)
        {
            closeToilet = true;
        }
        if (col.gameObject.tag == "Shower" && checkPills)
        {
            closeShower = true;
        }
        if (col.gameObject.tag == "Zink" && checkPills)
        {
            closeZink = true;
        }
        if (col.gameObject.tag == "Wardrobe" && checkPills)
        {
            closeWardrobe = true;
        }
        if (col.gameObject.tag == "Exit" && dressed)
        {
            exitOn = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Pills" && !checkPills)
        {
            closePills = false;
            pills.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "Sandwich")
        {
            closeSandwich = false;
        }
        if (col.gameObject.tag == "Toilet" && !pee)
        {
            closeToilet = false;
        }
        if (col.gameObject.tag == "Shower" && !showering)
        {
            closeShower = false;
        }
        if (col.gameObject.tag == "Zink" && !washing)
        {
            closeZink = false;
        }
        if (col.gameObject.tag == "Wardrobe" && !dressed)
        {
            closeWardrobe = false;
        }
    }
    void LoadNextScene()
    {
        //Add wait 5 sec and freeze 5 sec
        SceneManager.LoadScene("Scene2");
    }
}
