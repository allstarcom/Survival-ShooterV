using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour {

    Gun PlayerAmmo;

    void Start()
    {
        PlayerAmmo = GameObject.Find("Player").GetComponentInChildren<Gun>();
    }




    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerAmmo.maxAmmo <= 180)
            {
                PlayerAmmo.maxAmmo += 30;
                if (PlayerAmmo.maxAmmo > 180)
                {
                    PlayerAmmo.maxAmmo = 180;
                }
                Destroy(gameObject);
            }
        }

    }
}
