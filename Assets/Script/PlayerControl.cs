using System;
using UnityEngine;
using System.Collections;
using System.Numerics;
using System.Xml.Serialization;
using Audio;
using UnityEditor;
using UnityEngine.UI;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


[RequireComponent (typeof (Animator))]
public class PlayerControl : MonoBehaviour
{

	public Transform cameraTransform;
	public Transform[] rightBone;
	public Arsenal[] arsenal;
	public AudioSource audiosource;
	public Camera Cam;
	public bool alive = true;
	public Texture2D cursor;
	public Texture2D cursorAim;
	public Texture2D cursorReady;
	public HUDController hud;
	
	[SerializeField] private Rigidbody _rb;
	[SerializeField] private float _run = 6;
	private Vector3 _input;
	private Ray AimingRay;
	private Animator animator;
	
	public PauseHUD Pause;
	public Image[] ListGun;
//	public Image [] Amo;
//	public Sprite amo;
	public TextController TextBox;

	public AudioClip audioFootstep;
	public bool startNoWeapon = false;

	CharacterController characterController;
	private bool canInput = true;

	public bool getAlive()
	{
		return alive;
	}
 
	public void setAlive(bool state)
	{
		alive = state;
	}

	public void CanInputPlayer(bool b)
	{
		canInput = b;
		if(GetComponentInChildren<GunSystem>() != null)
			GetComponentInChildren<GunSystem>().CanInput = b;
	}

	void Awake()
	{
		characterController = GetComponent<CharacterController>();
		//audiosource = GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		if (arsenal.Length > 0)
			resetArsenal();
		if(startNoWeapon)
			SetArsenal(arsenal[5]);
		ChangeCursor(cursor);
	}
	
	private void Update()
	{
		if (canInput)
		{
			GatherInput();
			Look();
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause.OnPause(this);
        }
	}
	
	void FixedUpdate()
	{
		if(canInput)
			Move();
		
	}

	void GatherInput()
	{
		_input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		// On calcul input en fonction de la rotation de la camera
		Vector3 forward = Cam.transform.forward;
		forward.y = 0;
		forward.Normalize();
		Vector3 right = Cam.transform.right;
		_input = right * _input.x + forward * _input.z;
		_input.Normalize();

		/*if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == -1)
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
			_input = new Vector3(-1,0,-1);*/



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
		Vector3 pointToLook;
		AimingRay.origin += new Vector3(0, 0.4f, 0);
		if (groundPlane.Raycast(AimingRay, out rayLength) && alive)
		{
			pointToLook = AimingRay.GetPoint(rayLength);
			//Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);
			RaycastHit rayHit;
			if (Physics.Raycast(AimingRay, out rayHit))
			{
				var distanceFromEnnemy = Vector3.Distance(
             						new Vector3(rayHit.collider.transform.position.x, transform.position.y,
             							rayHit.collider.transform.position.z), transform.position);

				if (GetComponentInChildren<GunSystem>() != null)
				{
					if (rayHit.collider.CompareTag("Enemy") && distanceFromEnnemy <= GetComponentInChildren<GunSystem>().range)
					{
						ChangeCursor(cursorReady);
						transform.LookAt(new Vector3(rayHit.collider.transform.position.x, transform.position.y, rayHit.collider.transform.position.z));
					}
				}

				if (rayHit.collider.CompareTag("Enemy"))
				{
					ChangeCursor(cursorAim);
					transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
				}
				else
				{
					ChangeCursor(cursor);
					transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

				}
					
			}
			else // Si on pointe sur du vide on regarde dans la direction de la souris
			{
				transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
			}

			// Set Camera Transform
			float distanceMin = 8;
			float distanceMax = 40;
			float elasticity = 3f;
			float distance = (pointToLook - this.transform.position).sqrMagnitude;
			if (distance > distanceMin)
			{
				float pos = elasticity * Mathf.Clamp((distance - distanceMin) / (distanceMax - distanceMin), 0, 1);
				cameraTransform.localPosition = new Vector3(0, 0, pos);

			}
			else
			{
				cameraTransform.localPosition = Vector3.zero;
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
			
			animator.SetFloat("X", direction.z);
			animator.SetFloat("Y", direction.x);


			if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // 2 Direction
			{
				//transform.position = transform.position +
				//                     _input * (_input.magnitude * _run  * Time.deltaTime);
				//_rb.velocity = _input * (_input.magnitude * _run * Time.deltaTime); //(transform.position + transform.forward * (_input.magnitude * _run/1.5f * Time.deltaTime));
				//CharacterController c = GetComponent<CharacterController>();
				_input.y = -1;
				characterController.Move(_input * _run * Time.deltaTime);
				//_rb.AddForce(_input * (_input.magnitude * _run * Time.deltaTime), ForceMode.Force);
			}
			/*else if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0) // 1 Direction
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
				characterController.Move(_input * (_input.magnitude * _run * Time.deltaTime));
			}*/
			else // rien à l'arrêt
            {
				Vector3 gravity = new Vector3(0, -1f, 0);
				characterController.Move(gravity * Time.deltaTime);
			}
		}
		else
		{
			animator.SetBool("Running", false);

			Vector3 gravity = new Vector3(0, -1f, 0);
			characterController.Move(gravity * Time.deltaTime);
		}
	}

	// function for reEquip Gun
	public void resetArsenal()
	{
		SetArsenal(arsenal[0]);
	}
	// Function for equip new weapon
	public void SetArsenal(Arsenal arsenalEquip) {

		if (startNoWeapon)
			return;
		for (int i = 0; i < arsenal.Length-1; i++)
		{
			if (arsenal[i].name != arsenalEquip.name)
			{
				
				arsenal[i].IsEquip = false;
				ListGun[i].enabled = false;

				if (arsenal[i].PlayerBones.childCount > 0)
				{
					Debug.Log("destroy");
					Destroy(arsenal[i].PlayerBones.GetChild(0).gameObject);
				}
					
			}
			else if (arsenal[i].name == arsenalEquip.name && arsenal[i].IsEquip)
			{
				GetComponentInChildren<GunSystem>().AmmoReserve +=
					GetComponentInChildren<GunSystem>().magazineSizeInitial;
			}
			else
			{
				Debug.Log(arsenal[i].name);
				arsenal[i].IsEquip = true;
				ListGun[i].enabled = true;
				Debug.Log(ListGun[i].enabled);
			}
		}
		if (arsenalEquip.rightGun != null && arsenalEquip.PlayerBones != null && !arsenalEquip.IsEquip )
		{
			GameObject newRightWeapon=  (GameObject) Instantiate(arsenalEquip.rightGun);
            newRightWeapon.GetComponent<GunSystem>()._rb = _rb;
            newRightWeapon.GetComponent<GunSystem>()._hud = hud;
            newRightWeapon.GetComponent<GunSystem>().anim = animator;
            newRightWeapon.transform.parent = arsenalEquip.PlayerBones;
            newRightWeapon.transform.localPosition = Vector3.zero;
            newRightWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
            for(int i=1;i< animator.layerCount;i++)
            {
	            animator.SetLayerWeight(i, 0);
            }
            animator.SetLayerWeight( animator.GetLayerIndex(arsenalEquip.name),1);
		}
			
	}


	public void PlayFootstep()
    {
		AudioManager.Instance?.PlaySound(audioFootstep, 0.1f, 0.9f, 1.1f);
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
		public Transform PlayerBones;
	}
}
