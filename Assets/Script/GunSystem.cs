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
    public bool CanInput = true;


    public delegate void ActionShoot(Vector3 impact);
    public event ActionShoot OnShoot;

    private void Awake()
    {
        bulletLeft = magazineSizeInitial;
        readyToShoot = true;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if(CanInput)
            MyInput(); 
        if(_hud)
            _hud.UpdatePanelBullet(this);
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
        if (_hud.MessagePanelReload)
        {
            _hud.CloseMessagePanelReload();
        }
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
        if (Physics.Raycast(new Vector3(_rb.transform.position.x,0.5f,_rb.transform.position.z), _rb.transform.forward + direction, out rayHit,range,LayerMaskRaycast))
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
                OnShoot?.Invoke(new Vector3(ennemyHit.aliveGameObject.transform.position.x,
                                            ennemyHit.aliveGameObject.transform.position.y + (rayHit.point.y + ennemyHit.aliveGameObject.transform.position.y) * 0.5f, 
                                            ennemyHit.aliveGameObject.transform.position.z)
                                );

            }
            else
            {
                Debug.Log("No hit");
                OnShoot?.Invoke(rayHit.point);
            }

        }
        else
        {
            //Debug.Log(" No hit");
            OnShoot?.Invoke(Vector3.zero);
        }
        
        if (!infiniteAmmo)
        {
            bulletLeft--;
            
        }

        bulletShoot--;
        _hud.UpdatePanelBullet(this);
        if (bulletShoot > 0 && bulletLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
        else
        {
            Invoke("ResetShoot", timeBetweenShooting);
        }
        if(bulletLeft == 0 && AmmoReserve > 0)
        {
            _hud.OpenMessagePanelReload();
        }
        
    }

    private void OnDrawGizmos()
    {
     
        bool isHit = Physics.Raycast(new Vector3(_rb.transform.position.x,0.5f,_rb.transform.position.z), _rb.transform.forward, out rayHit, range);
       
        if (isHit)
        {
            Gizmos.color= Color.red;
            Gizmos.DrawRay(new Vector3(_rb.transform.position.x,0.5f,_rb.transform.position.z), _rb.transform.forward  * rayHit.distance);
        }  
        else
        {
            Gizmos.color= Color.blue;
            Gizmos.DrawRay(new Vector3(_rb.transform.position.x,0.5f,_rb.transform.position.z), _rb.transform.forward * range);
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
