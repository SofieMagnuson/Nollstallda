using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement2 : MonoBehaviour
{
    public float speed, extraSpeed, currentSpeed, jump;
    public Rigidbody RB;
    public bool timeRunning, isGrounded, running;
    public objects_interact OI;



    void Start()
    {
        speed = 5f;
        extraSpeed = 10f;
        currentSpeed = 0f;
        jump = 10f;
    }


    void Update()
    {
        //Movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 movement = (transform.right * x) + (transform.forward * z);
        transform.position += movement * speed * Time.deltaTime;


        //SpeedUp
        if (Input.GetKeyDown(KeyCode.X))
        {
            speed = 10f;
            Debug.Log("sprint");
            running = true;

        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            speed = 5;
            running = false;
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


    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


    }
}
