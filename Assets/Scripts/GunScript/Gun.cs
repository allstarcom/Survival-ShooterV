using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    public int damagePerShot = 20;                
    public float FireRate = 0.16f;        
    public float range = 10f;
     
    public int maxAmmo = 180;
    public int currentBullet;
    public int bulletsperMag=30;
    public float reloadTime;

    public Text Current_Bullet;
    public Text Max_Ammo;

    float timer;                                    // A timer to determine when to fire.
    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    int unwalkableMask;
    ParticleSystem gunParticles;                   
    LineRenderer gunLine;                          
    AudioSource gunAudio;
    public AudioSource reloadSound;
    Light gunLight;                                
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
    



    void Start()
    {
        
        shootableMask = LayerMask.GetMask("Shootable");
        unwalkableMask = LayerMask.GetMask("unWalkable");
        
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        currentBullet = bulletsperMag;
       
    }

    void Update()
    {
        
        timer += Time.deltaTime;
        AmmoText();
        if (Input.GetButton("Fire1") && timer >= FireRate)
        {
            if (currentBullet > 0)
            {
                Shoot();
            }
            else { StartCoroutine(Reload()); }
        }
        

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= FireRate * effectsDisplayTime)
        {           
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        if (currentBullet <= 0)
            return;
        timer = 0f;
        

        gunAudio.Play();

        gunLight.enabled = true;

        
        gunParticles.Stop();
        gunParticles.Play();

        
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = -transform.right;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {

            Enemyhp enemyhp = shootHit.collider.GetComponent<Enemyhp>();

            if(enemyhp.currentHp != null)
            {
                enemyhp.TakeDamage(damagePerShot);
            }
            
            gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
        currentBullet--;
       

    }

    IEnumerator Reload()
    {
        reloadSound.Play();
        yield return new WaitForSeconds(reloadTime);
       
        if (maxAmmo > 0)
        {
            
            int bulletstoLoad = bulletsperMag - currentBullet;
            //if         then 1st       else    2nd
            int bulletstoDeduct = (maxAmmo >= bulletstoLoad) ? bulletstoLoad : maxAmmo;
            maxAmmo -= bulletstoDeduct;
            currentBullet += bulletstoDeduct;
        }
        
    }

    void AmmoText()
    {
        Max_Ammo.text = "/" + " "+ maxAmmo.ToString();
        Current_Bullet.text = currentBullet.ToString();
    }

}
