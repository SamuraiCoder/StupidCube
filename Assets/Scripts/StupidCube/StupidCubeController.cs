using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Constants;


public class StupidCubeController : MonoBehaviour {

    public float jumpHeight;
    public float jumpSpeed;
    public float fallSpeed;
    
    public bool isGrounded = false;
    
    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        if (GameManager.SINGLETON.GameState == GameState.START || GameManager.SINGLETON.GameState == GameState.GAME) 
        {
            MoveStupidCube();
        }
	}
    void MoveStupidCube()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Force);
            isGrounded = false;
        }
        else
        {
            if(rb.velocity.y > 0.0f) //jumping
            {
                rb.velocity += Vector3.up * Physics.gravity.y * jumpSpeed * Time.deltaTime;
            }
            else if(rb.velocity.y <= 0.0f && !isGrounded) //falling we apply more speed 
            {
                rb.velocity -= Vector3.down * Physics.gravity.y * fallSpeed * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("platformStart"))
        {
            isGrounded = true;
        }
    }
}
