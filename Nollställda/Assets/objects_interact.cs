using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class objects_interact : MonoBehaviour
{
    public bool dressed, checkPills, eatSandwich, closeSandwich, closeWardrobe, closeToilet, closeZink, closeShower, closePills, pee, showering, washing, peedone, exitOn, playedSound, casual, business, sweats;
    public GameObject  pills, story, sandwich, toilet, shower, zink, exit, clothes, test, zinkB, showerB, wardrobeM, sandM, toiletM, zinkM, showerM, doorM, blurred, playerBody, fade, inShowerB, inShowerC, inShowerS, inShower;
    public int CountdownTime;
    public player_movement PL;
    public float timer, timerLose, timerAnim;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public AudioSource showerSound, eatingSound, peeSound, washSound, doorSound, wardrobeSound;
    public Text timerText;
    public animationScript AS;


    void Start()
    {
        timer = 90f;
        timerLose = 2f;
        checkPoints.currentPoints = 0;
    }

    void Update()
    {
        CheckObjects();
        DelayTime();
        CheckTime();

        int points = checkPoints.currentPoints;
        slider.value = points;
        fill.color = gradient.Evaluate(slider.normalizedValue);

        DisplayTime(timer);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            Lose();
        }

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
                    checkPoints.currentPoints += 1;
                    timer -= 5;
                    Destroy(sandwich);
                    sandM.gameObject.SetActive(false);
                    eatingSound.Play();
                    AS.animTimer();
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
                    Debug.Log("Toilet");
                    pee = true;
                    toiletM.gameObject.SetActive(false);
                    if (pee)
                    {
                        checkPoints.currentPoints += 1;
                        timer -= 5;
                        blurred.gameObject.SetActive(true);
                        peeSound.Play();
                        AS.animTimer();
                    }
                    if (!pee)
                    {
                        toiletM.gameObject.SetActive(true);
                        blurred.gameObject.SetActive(false);
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
                if (hit.collider.tag == "Shower" && !washing)
                {
                    showering = true;
                    if (showering)
                    {
                        checkPoints.currentPoints += 1;
                        timer -= 5;
                        showerB.gameObject.SetActive(true);
                        showerM.gameObject.SetActive(false);
                        showerSound.Play();
                        AS.animTimer();
                        AS.animShower();
                        inShower.gameObject.SetActive(false);
                        inShowerB.gameObject.SetActive(false);
                        inShowerC.gameObject.SetActive(false);
                        inShowerS.gameObject.SetActive(false);
                    }
                    if (!showering)
                    {
                        showerB.gameObject.SetActive(false);
                        sandM.gameObject.SetActive(true);
                    }
                }

            }
        }

        if (closeZink && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && !washing)
            {
                if (hit.collider.tag == "Zink" && !showering)
                {
                    washing = true;
                    if (washing)
                    {
                        checkPoints.currentPoints += 1;
                        timer -= 5;
                        zinkB.gameObject.SetActive(true);
                        zinkM.gameObject.SetActive(false);
                        washSound.Play();
                        AS.animTimer();
                    }
                    if(!washing)
                    {
                        zinkB.gameObject.SetActive(false);
                        zinkM.gameObject.SetActive(true);
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
                    wardrobeM.gameObject.SetActive(false);
                    wardrobeSound.Play();
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

    void DelayTime()
    {
        if (pee || showering || washing || eatSandwich)
        {
            PL.Freeze();
        }
    }

    void CheckTime()
    {
        if (timer < 50)
        {
            checkPoints.currentPoints -= 1;
        }
        if (timer < 30)
        {
            checkPoints.currentPoints -= 3;
        }
        if (timer < 10)
        {
            checkPoints.currentPoints -= 5;
        }
    }

    public void CheckBuisness()
    {
        checkPoints.currentPoints += 30;
        checkPoints.dressed += 1;
        timer -= 30;
        wardrobeSound.Play();
        Destroy(wardrobeM);
        AS.animTimer();
        business = true;
    }
    public void CheckCasual()
    {
        checkPoints.currentPoints += 15;
        timer -= 15;
        wardrobeSound.Play();
        Destroy(wardrobeM);
        AS.animTimer();
        checkPoints.dressed += 2;
        casual = true;
    }
    public void CheckSweat()
    {
        checkPoints.currentPoints += 5;
        timer -= 5;
        wardrobeSound.Play();
        Destroy(wardrobeM);
        AS.animTimer();
        checkPoints.dressed += 3;
        sweats = true;
    }

    public void FreezeDone()
    {
        Debug.Log("done");
        pee = false;
        showering = false;
        washing = false;
        eatSandwich = false;
        showerB.gameObject.SetActive(false);
        zinkB.gameObject.SetActive(false);
        blurred.gameObject.SetActive(false);
        AS.animTimerOff();
        AS.animShowerDone();
        if (casual)
        {
            inShowerC.gameObject.SetActive(true);
        }
        if (business)
        {
            inShowerB.gameObject.SetActive(true);
        }
        if (sweats)
        {
            inShowerS.gameObject.SetActive(true);
        }

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
            sandM.gameObject.SetActive(true);
        }
        if (col.gameObject.tag == "Toilet" && checkPills)
        {
            closeToilet = true;
            toiletM.gameObject.SetActive(true);
        }
        if (col.gameObject.tag == "Shower" && checkPills)
        {
            closeShower = true;
            showerM.gameObject.SetActive(true);
        }
        if (col.gameObject.tag == "Zink" && checkPills)
        {
            closeZink = true;
            zinkM.gameObject.SetActive(true);
        }
        if (col.gameObject.tag == "Wardrobe" && checkPills)
        {
            closeWardrobe = true;
            wardrobeM.gameObject.SetActive(true);
        }
        if (col.gameObject.tag == "Exit" && dressed)
        {
            exitOn = true;
            doorSound.Play();
            doorM.gameObject.SetActive(true);
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
            sandM.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "Toilet" && !pee)
        {
            closeToilet = false;
            toiletM.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "Shower" && !showering)
        {
            closeShower = false;
            showerM.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "Zink" && !washing)
        {
            closeZink = false;
            zinkM.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "Wardrobe" && !dressed)
        {
            closeWardrobe = false;
            wardrobeM.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "Exit" && !dressed)
        {
            doorM.gameObject.SetActive(false);
        }

    }
    void Lose()
    {
        fade.SetActive(true);
        if (timerLose > 0)
        {
            timerLose -= Time.deltaTime;
            if (timerLose < 0)
            {
                SceneManager.LoadScene("Lose");
            }
        }

    }

    void LoadNextScene()
    {
        //Add wait 5 sec and freeze 5 sec
        SceneManager.LoadScene("Scene2");
    }
}
