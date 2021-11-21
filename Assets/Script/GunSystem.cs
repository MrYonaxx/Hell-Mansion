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
    public int magazineSizeInitial, bulletsPerTap;
    private int bulletShoot;
    public int AmmoReserve = 0 ;
    public bool allowButtonHold, infiniteAmmo;
    public Rigidbody _rb ;
    public Animator anim;
    public RaycastHit rayHit;
    public HUDController _hud;
    // --- Audio ---
    public AudioClip GunShotClip;
    public AudioSource source;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    public LayerMask LayerMaskRaycast;
    // --- Muzzle ---
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;

    
    public int bulletLeft, bulletsShot;
    private bool shooting, readyToShoot, reloading;
   
    
    private void Awake()
    {
        bulletLeft = magazineSizeInitial;
        readyToShoot = true;
        
        //Cursor.lockState = CursorLockMode.Confined;
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
            if (Input.GetButton("Reload") && bulletLeft < magazineSizeInitial && !reloading && AmmoReserve > 0)
            {
                Reload();
            }

            //Shoot
            if (readyToShoot && shooting && !reloading && (bulletLeft > 0 || infiniteAmmo ))
            {
                
                bulletShoot = bulletsPerTap;
                if (source != null)
                {
                    AudioManager.Instance?.PlaySound(GunShotClip, 0.3f, audioPitch.x, audioPitch.y);

                }
                Shoot();
            }
        }
    }

    private void Reload()
    {
        Debug.Log("reload en cours");
        StartCoroutine(_hud.StartReload(reloadTime));
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        int bulletReload = magazineSizeInitial - bulletLeft;
        if (AmmoReserve > bulletReload)
        {
            bulletLeft += bulletReload;
            AmmoReserve -= bulletReload;
        }
        else 
        {
            bulletLeft += AmmoReserve;
            AmmoReserve -= AmmoReserve;
        }
        reloading = false;
    }

    private void Shoot()
    {
        readyToShoot = false;
        if (muzzlePosition != null)
        {
            var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);
        }
        
        // spread 

        var x = Random.Range(-spread, spread);
        var z = Random.Range(-spread, spread);
        Vector3 direction = new Vector3(x, 0, z);
        
        
        //Raycast
        if (Physics.Raycast(_rb.transform.position, _rb.transform.forward + direction, out rayHit,range,LayerMaskRaycast))
        {
            Entity ennemyHit = rayHit.collider.GetComponentInParent<Entity>();
            Debug.Log(rayHit.collider.name);
            if (ennemyHit)
            {
                AttackDetails currentdamage;
                currentdamage.position = rayHit.collider.transform.position;
                currentdamage.damageAmount = damage;
                currentdamage.stunDamageAmount = 1;
                ennemyHit.Damage(currentdamage);
                Debug.Log("hit");
            }
            
        }
        else
        {
            Debug.Log(" No hit");
        }
        
        if (!infiniteAmmo)
        {
            bulletLeft--;
            
        }

        bulletShoot--;
        if (bulletShoot > 0 && bulletLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
        else if (bulletLeft > 0 && bulletLeft == 0)
        {
            Invoke("ResetShoot", timeBetweenShooting);
        }
        else
        {
            Invoke("ResetShoot", timeBetweenShooting);
        }
        
    }

    private void OnDrawGizmos()
    {
     
        bool isHit = Physics.Raycast(_rb.transform.position, _rb.transform.forward, out rayHit, range);
       
        if (isHit)
        {
            Gizmos.color= Color.red;
            Gizmos.DrawRay(_rb.transform.position, _rb.transform.forward  * rayHit.distance);
        }  
        else
        {
            Gizmos.color= Color.blue;
            Gizmos.DrawRay(_rb.transform.position, _rb.transform.forward * range);
        }
    }

    private void ResetShoot()
    {
        if (bulletLeft <= 0 && AmmoReserve <= 0)
        {
            GetComponentInParent<PlayerControl>().resetArsenal();
        }
        else
        {
            readyToShoot = true;
        }
    }
}
