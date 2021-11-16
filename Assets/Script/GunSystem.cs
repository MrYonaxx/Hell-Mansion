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
    public int magazineSizeInitial;
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
                //anim.SetLayerWeight(1, 1);
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
        
        //Raycast
        if (Physics.Raycast(_rb.transform.position, _rb.transform.forward, out rayHit,range))
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

        }

        if (!infiniteAmmo)
        {
            bulletLeft--;
        }
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
        if (bulletLeft <= 0 && AmmoReserve <= 0)
        {
            GetComponentInParent<PlayerControl>().SetArsenal("Gun");
        }
        else
        {
            readyToShoot = true;
        }
        
        
    }
    
    
}
