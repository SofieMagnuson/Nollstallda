using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class totalScore : MonoBehaviour
{
    public int points;
    public Text pointsText;
    public GameObject bad, medium, good;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 3f;
        int points = checkPoints.currentPoints;
        pointsText.text = points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (points >= 12)
            {
                good.gameObject.SetActive(true);
                bad.gameObject.SetActive(false);
                medium.gameObject.SetActive(false) ;
            }
            if (points >= 8)
            {
                medium.gameObject.SetActive(true);
                good.gameObject.SetActive(false);
                bad.gameObject.SetActive(false);
            }
            if (points >= 0)
            {
                bad.gameObject.SetActive(true);
                medium.gameObject.SetActive(false);
                good.gameObject.SetActive(false);
            }
        }
    }
}
