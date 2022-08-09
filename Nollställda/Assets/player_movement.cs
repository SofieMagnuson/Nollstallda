using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{

    //3DMovement
    public float speed, countdown;
    public Rigidbody RB;
    float currentTime = 0f;
    public bool doneFreeze, timeRunning;
    public objects_interact OI;
    public animationScript AS, PA;
    [SerializeField] LayerMask _aimLayerMask;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().PlayMusic();
        //3DMovement
        speed = 5f;
        countdown = 3f;
        currentTime = countdown;

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = (transform.right * x) + (transform.forward * z);
        transform.position += movement * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W))
            {
                PA.animWalking();
            }
        if (Input.GetKeyDown(KeyCode.S))
            {
                PA.animWalking();
            }
        if (Input.GetKeyDown(KeyCode.D))
            {
                PA.animWalking();
            }
        if (Input.GetKeyDown(KeyCode.A))
            {
                PA.animWalking();
            }
        if (Input.GetKeyUp(KeyCode.W))
            {
                PA.animIdle();
            }
        if (Input.GetKeyUp(KeyCode.S))
            {
                PA.animIdle();
            }
        if (Input.GetKeyUp(KeyCode.D))
            {
                PA.animIdle();
            }
        if (Input.GetKeyUp(KeyCode.A))
            {
                PA.animIdle();
            }
    }

    public void Freeze()
    {
        timeRunning = true;
        speed = 0f;
        countdown -= Time.deltaTime;
        currentTime -= 1 * Time.deltaTime;
        if (countdown < 0)
        {
            timeRunning = false;
            speed = 5f;
            timeRunning = false;
            if (!timeRunning)
            {
                countdown = 3f;
                currentTime = countdown;
                OI.FreezeDone();
            }

        }
    }

    public void FreezeON()
    {
        speed = 0f;
    }

    public void SpeedBack()
    {
        speed = 5;
    }
}
