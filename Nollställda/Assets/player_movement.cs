using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Rigidbody rb;
    Vector3 movement;
    public SpriteRenderer SR;
    public bool buisness;
    public GameObject buisnessC;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            SR.flipX = false;
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            SR.flipX = true;
        }

        if (buisness)
        {
            buisnessC.gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
