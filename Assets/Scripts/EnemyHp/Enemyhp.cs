using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhp : MonoBehaviour {


    Hpbar PlayerHp;
    LootScript Loot;
    public int FullHp;
    int scoreValue = 10;


    public Transform Target;
    public int currentHp;
    bool isDead;
    public Vector3 direction;
    Animator anim;
    BoxCollider boxCollider;
    

    void Start () {

        PlayerHp = GetComponent<Hpbar>();
        anim = GetComponent<Animator>();
        Loot = GetComponent<LootScript>();
        boxCollider = GetComponent<BoxCollider>();
        currentHp = FullHp;
        
    }
	
	// Update is called once per frame
	void Update () {
        TurnRate();
       

    }

    public void  TakeDamage(int DmgShot)
    {
        if(isDead)
        {
            return;
        }

        currentHp -= DmgShot;
      
        if (currentHp<=0)
        {

            ScoreManager.score += scoreValue;
            Death();


        }

   }


    void Death()
    {
        Loot.CalculateDrop();
        isDead = true;
        anim.SetBool("IsWalking", false);
        anim.SetTrigger("IsDead");
        boxCollider.isTrigger = true;
        Destroy(this.gameObject.GetComponent<FollowWaypoints>());
        Destroy(gameObject, 2f);
    }

    void TurnRate()
    {

        if (isDead != true)
        {
            direction = Target.transform.position - this.transform.position;
            direction.y = 0f;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                      

            anim.SetBool("IsAttacking", false);
            if (direction.magnitude > 2)
            {
                anim.SetBool("IsAttacking", false);
                anim.SetBool("IsWalking", true);
            }
            else
            {
                anim.SetBool("IsAttacking", true);
                anim.SetBool("IsWalking", false);
            }
           
          
        }
    }


}
   