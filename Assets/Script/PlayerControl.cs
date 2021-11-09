using System;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[RequireComponent (typeof (Animator))]
public class PlayerControl : MonoBehaviour {

	public Transform rightGunBone;
	public Transform rightRifleBone;
	public Arsenal[] arsenal;
	public Camera Cam;
	public bool alive = true;
	public AudioSource audio;
	[SerializeField] private Rigidbody _rb;
	[SerializeField] private float _run = 6;
	private Vector3 _input;
	private Vector3 _mousePos;
	private Animator animator;

	
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
		audio = GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		if (arsenal.Length > 0)
			SetArsenal (arsenal[0].name);
		//action.Stay();
		
	}
	
	private void Update()
	{
		GatherInput();
		Look();
		
	}
	
	void FixedUpdate()
	{
		Move();
		FireGun();
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
		Ray cameraRay = Cam.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		if (groundPlane.Raycast(cameraRay, out rayLength) && alive)
		{
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

			transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
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


			if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
			{
				transform.position = transform.position +
				                     _input * (_input.magnitude * _run  * Time.deltaTime);
				//_rb.velocity = _input * (_input.magnitude * _run * Time.deltaTime); //(transform.position + transform.forward * (_input.magnitude * _run/1.5f * Time.deltaTime));
			}
			else
			{
				transform.position =
					transform.position + _input * (_input.magnitude * _run / 2f * Time.deltaTime);
				//_rb.velocity = new Vector3(0, 0, 0); // _input * (_input.magnitude * _run * Time.deltaTime); //
				//_rb.MovePosition(transform.position + transform.forward * (_input.magnitude * _run * Time.deltaTime));
			}
		}
		else
		{
			animator.SetBool("Running", false);
		}
	}
	public void FireGun()
	{
		/*if (Input.GetButton("Fire1") && alive )
		{
			animator.SetLayerWeight(1, 1);
		}
		else
		{
			animator.SetLayerWeight(1, 0);
		}*/
	}
	

	public void SetArsenal(string name) {
		foreach (Arsenal hand in arsenal) {
			if (hand.name == name) {
				if (rightGunBone.childCount > 0)
					Destroy(rightGunBone.GetChild(0).gameObject);
				if (rightRifleBone.childCount > 0)
					Destroy(rightRifleBone.GetChild(0).gameObject);
				if (hand.name == "Gun" && hand.rightGun != null) {
					GameObject newRightGun = (GameObject) Instantiate(hand.rightGun);
					newRightGun.GetComponent<GunSystem>()._rb = _rb;
					newRightGun.GetComponent<GunSystem>().anim = animator;
					newRightGun.transform.parent = rightGunBone;
					newRightGun.transform.localPosition = Vector3.zero;
					newRightGun.transform.localRotation = Quaternion.Euler(0, 0, 0);
					animator.SetLayerWeight(1,1);
					animator.SetLayerWeight(2,0);
				}
				if (hand.name == "Rifle" && hand.rightGun != null) {
					GameObject newRightRifle = (GameObject) Instantiate(hand.rightGun);
					newRightRifle.transform.parent = rightRifleBone;
					newRightRifle.GetComponent<GunSystem>()._rb = _rb;
					newRightRifle.GetComponent<GunSystem>().anim = animator;
					newRightRifle.transform.localPosition = Vector3.zero;
					newRightRifle.transform.localRotation = Quaternion.Euler(0, 0, 0);
					animator.SetLayerWeight(1,0);
					animator.SetLayerWeight(2,1);
				}
				return;
				}
		}
	}
	
	public void OnDrawGizmos()
	{
		float MaxDistance = 10f;
		Gizmos.color= Color.green;
		//Gizmos.DrawRay(transform.position, transform.forward * MaxDistance);
	}

	
	[System.Serializable]
	public struct Arsenal {
		public string name;
		public GameObject rightGun;
	}
}
