using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    GameObject player;
    Hpbar PlayerHp;
    Animator anim;
    Enemyhp zombiehp;

    bool inRange;
   
    public int Damage;

    void Start()
    {
        PlayerHp = GameObject.Find("Player").GetComponent<Hpbar>();
        anim = GetComponent<Animator>();
        zombiehp = GetComponent<Enemyhp>();

    }


   void Update()
    {
        if(inRange)
        {
            Attack();
            Animating();
        }


    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("Hit");
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    void Attack()
    {
        if(PlayerHp.CurrentHp > 0)
        {
            PlayerHp.TakeDamage(Damage);
        }
    }

   void Animating()
    {
        
    }



}
