using UnityEngine;
using System.Collections;

public class BoxMovement : MonoBehaviour, BindingAction
{
	bool playerInRange = false;
	GameObject player;

	float distToGround;
	float distToEdge;

	float pushForce = 20000;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		distToGround = GetComponent <Collider> ().bounds.extents.y;
		distToEdge = GetComponent <Collider> ().bounds.extents.x;

		player.GetComponent <KeyBindings> ().registerBindingAction ("push", this);

		// Setup default keyBindings.
		KeyBinding binding = new KeyBinding ("push", this);
		binding.m_funnyText = "Apply a 20000 newton force to regular hexahedrons.";
		binding.addKeyDown (KeyCode.Semicolon);
		binding.addKeyDown (KeyCode.Home);
		player.GetComponent <KeyBindings> ().aquireKeyBinding (binding);
	}
	
	// Update is called once per frame
	void Update () {
		checkForBoxDrop ();
	}

	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.name == "Player")
		{
			playerInRange = true;
			//Debug.Log ("box in range");
		}
	}
	
	void OnTriggerExit (Collider otherCollider)
	{
		if (otherCollider.name == "Player")
		{
			playerInRange = false;
		}
	}

	void BindingAction.executeBinding (string actionName)
	{
		if (actionName == "push" && playerInRange)
		{
			pushBox();
		}
	}
	
	void BindingAction.onBindingRelease (string actionName)
	{

	}

	void pushBox ()
	{
		// Get direction of player to box.
		Vector3 direction = transform.position - player.transform.position;

		if (direction.x > 0)
		{
			direction = Vector3.right;
		}
		else
		{
			direction = Vector3.left;
		}

		GetComponent <Rigidbody> ().AddForce (direction * pushForce);
		//GetComponent <Rigidbody> ().AddTorque (0, 0, -pushForce);
	}

	void checkForBoxDrop ()
	{
		Vector3 castPos = transform.position;
		castPos.x -= distToEdge;

		if (Physics.Raycast (castPos, Vector3.down, distToGround + 0.5f))
		{
			return;
		}

		castPos.x += distToEdge * 2;

		if (Physics.Raycast (transform.position, Vector3.down, distToGround + 0.5f))
		{
			return;
		}

		Vector3 vel = GetComponent <Rigidbody>().velocity;
		vel.x = 0;
		GetComponent <Rigidbody>().velocity = vel;
	}
}

