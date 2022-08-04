using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objects_interact : MonoBehaviour
{
    public bool buisness, casual, sweat, dressed, checkPills, eatSandwich, closeSandwich;
    public GameObject buisnessC, casualC, sweatC, wardrobe, pills, story, sandwich;
    public int CountdownTime;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GetDressed();
        CheckObjects();

    }
    void GetDressed()
    {
        if (buisnessC.gameObject.activeSelf == true)
        {
            buisness = true;
            dressed = true;
            if (dressed)
            {
                Destroy(wardrobe);
            }
        }
        if (casualC.gameObject.activeSelf == true)
        {
            casual = true;
            dressed = true;
            if (dressed)
            {
                Destroy(wardrobe);
            }
        }
        if (sweatC.gameObject.activeSelf == true)
        {
            sweat = true;
            dressed = true;
            if (dressed)
            {
                Destroy(wardrobe);
            }
        }
    }


    void CheckObjects()
    {
        if (story.gameObject.activeSelf == true)
        {
            checkPills = true;
            dressed = true;
            if (checkPills)
            {
                Destroy(pills);
            }
        }
        else if (closeSandwich)
        {
            eatSandwich = true;
            Destroy(sandwich);
            StartCoroutine(CountdownStart());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        closeSandwich= true;
        sandwich.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider collision)
    {
        closeSandwich = false;
        sandwich.gameObject.SetActive(false);
    }

    IEnumerator CountdownStart()
    {
        while (CountdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            CountdownTime--;
        }
        yield return new WaitForSeconds(1f);
    }
}
