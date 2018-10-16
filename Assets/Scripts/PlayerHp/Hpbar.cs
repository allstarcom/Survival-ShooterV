using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour {

    public int MaxHp;
    public float CurrentHp;
    bool damaged;
    Animator anim;
    boiControls playerMovement;
    public Text deathText;
    public GameObject gun;
 

    bool isDead;
    public Slider Hp;

	void Start () {

        CurrentHp = MaxHp;
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<boiControls>();
        
    }
	
	// Update is called once per frame
	void Update () {
        Hp.value = CurrentHp;
        if (CurrentHp > MaxHp)
        {
            CurrentHp = MaxHp;
        }

        StartCoroutine(Freeze());

    }


    public void TakeDamage(int Damage)
    {
        damaged = true;

        CurrentHp -= Damage;
        Hp.value = CurrentHp;

        if (CurrentHp <= 0 && !isDead)
        {
            Death();
        }

      }
    IEnumerator Freeze()
    {
        if (isDead == true)
        {
            deathText.text = "Game Over";
            yield return new WaitForSeconds(3f);
            Time.timeScale = 0f;
        }
    }



    void Death()
    {
        isDead = true;
        anim.SetTrigger("IsDead");
        playerMovement.enabled = false;
        Destroy(gun);
        Destroy(gameObject,4f);
        
    }




        
}
