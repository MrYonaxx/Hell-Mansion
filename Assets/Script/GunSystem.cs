using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Audio;

public class GunSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, BulletsPerTap;
    public bool allowButtonHold;
    public Rigidbody _rb ;
    public Animator anim;
    public RaycastHit rayHit;
        
    // --- Audio ---
    public AudioClip GunShotClip;
    public AudioSource source;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    // --- Muzzle ---
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;

    
    private int bulletLeft, bulletsShot;
    private bool shooting, readyToShoot, reloading;
   
    
    //parent.GetComponent<Animator>().SetBool("Fire", false);
    public bool GetReloading()
    {
        return reloading;
    } 
    private void Awake()
    {
        if(source != null) source.clip = GunShotClip;
        bulletLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (_rb.GetComponent<PlayerControl>().getAlive())
        {
            if (allowButtonHold)
                shooting = Input.GetButton("Fire1");
            else
                shooting = Input.GetButtonDown("Fire1");
            // Reload
            if (Input.GetButton("Reload") && bulletLeft < magazineSize && !reloading)
                Reload();

            //Shoot
            if (readyToShoot && shooting && !reloading && bulletLeft > 0)
            {
                //anim.SetLayerWeight(1, 1);
                Shoot();
                Debug.Log("Shoot");
            }
        }
    }

    private void Reload()
    {
        Debug.Log("reload en cours");
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletLeft = magazineSize;
        reloading = false;
    }

    private void Shoot()
    {
        readyToShoot = false;
        if (muzzlePosition != null)
        {
            var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);
        }
        
        //Raycast
        if (Physics.Raycast(_rb.transform.position, _rb.transform.forward, out rayHit, range))
        {
            Debug.Log(rayHit.collider.name);
            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider?.GetComponent<Health>()?.TakeDamage(damage);
            Debug.Log("hit");
        }
        else
        {
            Debug.Log(" No hit");
        }
        // --- Handle Audio ---
        if (source != null)
        {
            AudioManager.Instance?.PlaySound(GunShotClip, 0.3f, audioPitch.x, audioPitch.y);
            // --- Instantiate prefab for audio, delete after a few seconds ---
            /*AudioSource newAS = Instantiate(source);
            
                // --- Change pitch to give variation to repeated shots ---
                newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);

                // --- Play the gunshot sound ---
                newAS.PlayOneShot(GunShotClip);

                // --- Remove after a few seconds. Test script only. When using in project I recommend using an object pool ---
                Destroy(newAS.gameObject, timeBetweenShooting);*/
            
            
        }
        bulletLeft--;
        anim.SetBool("Fire", true);
        Invoke("ResetShoot", timeBetweenShooting);
        
    }

    private void OnDrawGizmos()
    {
        bool isHit = Physics.Raycast(_rb.transform.position, _rb.transform.forward, out rayHit, range);
        float MaxDistance = 10f;
        if (isHit)
        {
            Gizmos.color= Color.red;
            Gizmos.DrawRay(_rb.transform.position, _rb.transform.forward * rayHit.distance);
            
        }  
        else
        {
            Gizmos.color= Color.blue;
            Gizmos.DrawRay(_rb.transform.position, _rb.transform.forward * MaxDistance);
            
        }  
    }

    private void ResetShoot()
    {
        readyToShoot = true;
        anim.SetBool("Fire", false);
    }
    
}
