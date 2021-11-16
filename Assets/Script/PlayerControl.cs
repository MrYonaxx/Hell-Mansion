using System;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using Audio;
using UnityEditor;


[RequireComponent (typeof (Animator))]
public class PlayerControl : MonoBehaviour {

	public Transform rightGunBone;
	public Transform rightRifleBone;
	public Arsenal[] arsenal;
	public AudioSource audiosource;
	public Camera Cam;
	public bool alive = true;
	public Texture2D cursor;
	public Texture2D cursorAim;
	public HUDController hud;
	
	[SerializeField] private Rigidbody _rb;
	[SerializeField] private float _run = 6;
	private Vector3 _input;
	private Ray AimingRay;
	private Animator animator;
	
	
	public Image [] Amo;
	public Sprite amo;


	public bool getAlive()
	{
		return alive;
	}
 
	public void setAlive(bool state)
	{
		alive = state;
	}
	
	void Awake()
	{
		audiosource = GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		if (arsenal.Length > 0)
			SetArsenal (arsenal[0].name);
		Debug.Log(arsenal[0].name);
		ChangeCursor(cursor);
	}
	
	private void Update()
	{
		GatherInput();
		Look();

//		for (int i = 0; i < Amo.Length; i++)
//        {
//            if(arsenal.Length > 0){
//				if (i< arsenal[0].bulletLeft)
//               		Amo[i].sprite = amo;
//				else 
//					Amo[i].sprite = empty;
//				
//				if(i < arsenal[0].rightGun.GetComponent<GunSystem>().bulletLeft)
//                	Amo[i].enabled = true;
//            	else
//                	Amo[i].enabled = false;
//            }		
//            
//        }
//		
	}
	
	void FixedUpdate()
	{
		Move();
		
	}
	void GatherInput()
	{
		if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == -1)
        	_input = new Vector3(-1,0,0);
		if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 0)
			_input = new Vector3(-1,0,1);
		if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 1)
			_input = new Vector3(0,0,1);
		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 1)
			_input = new Vector3(1,0,1);
		if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 1)
			_input = new Vector3(1,0,0);
		if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") ==0)
			_input = new Vector3(1,0,-1);
		if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == -1)
			_input = new Vector3(0,0,-1);
		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == -1)
			_input = new Vector3(-1,0,-1);
		//Debug.Log(Input.GetAxisRaw("Vertical"));
		//Debug.Log(Input.GetAxisRaw("Horizontal"));
		//_input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		if (Input.GetButton("Revive"))
		{
			Debug.Log("revive");
			GetComponent<Health>().Revive();
		}
		
	}

	void Look()
	{

		float rayLength;
		AimingRay = Cam.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		if (groundPlane.Raycast(AimingRay, out rayLength) && alive)
		{
			Vector3 pointToLook = AimingRay.GetPoint(rayLength);
			//Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);
			RaycastHit rayHit;
			if (Physics.Raycast(AimingRay, out rayHit))
			{
				if (rayHit.collider.CompareTag("Enemy"))
				{
					ChangeCursor(cursorAim);
					transform.LookAt(new Vector3(rayHit.collider.transform.position.x, transform.position.y, rayHit.collider.transform.position.z));
				}
				else
				{
					transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
					ChangeCursor(cursor);
				}
					
			}
			
			
		}
	}
	// ReSharper disable Unity.PerformanceAnalysis
	void Move()
	{
		
		if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && alive)
		{
			animator.SetBool("Running", true);
			Vector3 forward = transform.forward;
			forward.y = 0;
			forward.Normalize();
			Vector3 right = transform.right;
			Vector3 direction = right * _input.x + forward * _input.z;
			direction.Normalize();
			
			animator.SetFloat("X", direction.x);
			animator.SetFloat("Y", direction.z);


			if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0) // 2 Direction
			{
				//transform.position = transform.position +
				//                     _input * (_input.magnitude * _run  * Time.deltaTime);
				//_rb.velocity = _input * (_input.magnitude * _run * Time.deltaTime); //(transform.position + transform.forward * (_input.magnitude * _run/1.5f * Time.deltaTime));
				CharacterController c = GetComponent<CharacterController>();
				c.Move(_input * (_input.magnitude * _run*1.5f * Time.deltaTime));
				//_rb.AddForce(_input * (_input.magnitude * _run * Time.deltaTime), ForceMode.Force);
			}
			else if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0) // 1 Direction
			{
				//transform.position =
				//	transform.position + _input * (_input.magnitude * _run / 2f * Time.deltaTime);
				//_rb.velocity = _input * (_input.magnitude * _run / 2f * Time.deltaTime); // _input * (_input.magnitude * _run * Time.deltaTime); //
				//_rb.AddForce(_input * (_input.magnitude * _run  * Time.deltaTime), ForceMode.Force);
				CharacterController c = GetComponent<CharacterController>();
				c.Move(_input * (_input.magnitude * _run /1.5f * Time.deltaTime));
			}
			else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") != 0)
			{
				CharacterController c = GetComponent<CharacterController>();
				c.Move(_input * (_input.magnitude * _run * Time.deltaTime));
			}
			else // rien à l'arrêt
            {
				CharacterController c = GetComponent<CharacterController>();
				c.Move(Vector3.zero);
				//_rb.velocity = Vector3.zero;
			}
		}
		else
		{
			animator.SetBool("Running", false);
		}
	}



	
	public void SetArsenal(string name) {
		for (int i = 0; i < arsenal.Length; i++)
		{
			if (arsenal[i].name != name)
			{
				arsenal[i].IsEquip = false; 
			}
			else if (arsenal[i].IsEquip)
			{
				GetComponentInChildren<GunSystem>().AmmoReserve +=
					GetComponentInChildren<GunSystem>().magazineSizeInitial;
				return;
			}
			else{
				if (rightGunBone.childCount > 0)
				{ 
					Destroy(rightGunBone.GetChild(0).gameObject);
				}
				if (rightRifleBone.childCount > 0)
				{
					Destroy(rightRifleBone.GetChild(0).gameObject);
				}
					
				if (name == "Gun" && arsenal[i].rightGun != null)
				{
					arsenal[i].IsEquip = true;
					GameObject newRightGun = (GameObject) Instantiate(arsenal[i].rightGun);
					newRightGun.GetComponent<GunSystem>()._rb = _rb;
					newRightGun.GetComponent<GunSystem>()._hud = hud;
					newRightGun.GetComponent<GunSystem>().anim = animator;
					newRightGun.transform.parent = rightGunBone;
					newRightGun.transform.localPosition = Vector3.zero;
					newRightGun.transform.localRotation = Quaternion.Euler(0, 0, 0);
					animator.SetLayerWeight(1,1);
					animator.SetLayerWeight(2,0);
				}
				if (name == "Rifle" && arsenal[i].rightGun != null)
				{
					arsenal[i].IsEquip = true;
					GameObject newRightRifle = (GameObject) Instantiate(arsenal[i].rightGun);
					newRightRifle.GetComponent<GunSystem>()._rb = _rb;
					newRightRifle.GetComponent<GunSystem>()._hud = hud;
					newRightRifle.GetComponent<GunSystem>().anim = animator;
					newRightRifle.transform.parent = rightRifleBone;
					newRightRifle.transform.localPosition = Vector3.zero;
					newRightRifle.transform.localRotation = Quaternion.Euler(0, 0, 0);
					animator.SetLayerWeight(1,0);
					animator.SetLayerWeight(2,1);
				}
				
			}
		}
	}


	
	
	public void OnDrawGizmos()
	{
		Gizmos.color= Color.red;
		Gizmos.DrawRay(AimingRay);
	}
	
	private void ChangeCursor(Texture2D cursorType)
	{
		Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
		Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
        
	}


	
	[System.Serializable]
	public struct Arsenal {
		public string name;
		public GameObject rightGun;
		public bool IsEquip;
	}
}
