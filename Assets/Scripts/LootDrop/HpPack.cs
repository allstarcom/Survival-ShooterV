using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPack : MonoBehaviour {

    Hpbar Playerhp;

    void Start()
    {
        Playerhp = GameObject.Find("Player").GetComponent<Hpbar>();
    }




    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Playerhp.CurrentHp += 10;
            Destroy(gameObject);
        }

    }
}
