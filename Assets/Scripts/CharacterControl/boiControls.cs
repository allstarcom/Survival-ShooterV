using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boiControls : MonoBehaviour {

	static Animator anim;

	public float speed = 3.0F;
    
    int floorMask;
    public float camRaylength;
    Vector3 movement;
    Rigidbody rb;
    

    void Awake () 
	{
        floorMask = LayerMask.GetMask("Walkable");
        rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float V = Input.GetAxis("Vertical");
        float H = Input.GetAxis("Horizontal");
        Move(H, V);


        Turning();

        Animating(H,V);
    }

    void Move(float H, float V)
    {
        movement.Set(H, 0f, V);

        movement = movement.normalized * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
                       
    }

    void Turning()
    {

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(camRay, out hit, camRaylength, floorMask))
        {
            Vector3 playerTopoint = hit.point - transform.position;
            playerTopoint.y = 0f;

            Quaternion Rotation = Quaternion.LookRotation(playerTopoint); //takes z axis or forward;
            rb.MoveRotation(Rotation);


        }



    }




    void Animating(float H, float V)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("isGunPlay", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
        }

        if (V != 0 || H != 0 )
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isGunPlay", false);

        }
        else 
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isGunPlay", true);


        }




    }

}
