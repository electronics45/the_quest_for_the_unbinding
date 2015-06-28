using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour, BindingAction
{
	public float m_moveSpeed = 100;
	public float m_maxMoveSpeed = 10;

	public bool isMovementEnabled = true;

	float m_moveFactor = 60; // Expected 60 frames per second.
	float distToGround;

	bool m_isStopping = false;

	// Use this for initialization
	void Start () {
		distToGround = GetComponent <Collider> ().bounds.extents.y;

		GetComponent <KeyBindings> ().registerBindingAction ("move", this);

		// Default Keybindings.
		KeyBinding binding = new KeyBinding ("left", this);
		binding.m_funnyText = "Cast Spell of anit-right-ward locomotion.";
		binding.setIsContinuous (true);
		binding.addKeyDown (KeyCode.A);
		binding.addKeyDown (KeyCode.G);

		GetComponent <KeyBindings> ().aquireKeyBinding (binding);

		binding = new KeyBinding ("right", this);
		binding.m_funnyText = "Cast Spell of right-ward locomotion.";
		binding.setIsContinuous (true);
		binding.addKeyDown (KeyCode.D);
		
		GetComponent <KeyBindings> ().aquireKeyBinding (binding);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_isStopping)
		{
			stopALittle();
		}
	}

	public void moveRight ()
	{
		Rigidbody body = GetComponent <Rigidbody> ();

		float additionalForce = Time.deltaTime * m_moveSpeed * m_moveFactor;

		Vector3 angles = transform.localEulerAngles;
		angles.y = 0;
		transform.localEulerAngles = angles;

		if (body.velocity.x < m_maxMoveSpeed)
		{
			body.AddForce (Vector3.right * additionalForce);
		}
		else
		{
			body.velocity = new Vector3 (m_maxMoveSpeed, body.velocity.y, body.velocity.z);
		}

		//body.AddForce (Vector3.left * (moveSpeed * Time.deltaTime));

//		if (body.velocity.x < -m_maxMoveSpeed)
//		{
//			body.velocity = new Vector3 (-m_maxMoveSpeed, body.velocity.y, body.velocity.z);
//		}

		m_isStopping = false;
	}

	public void moveLeft ()
	{
		// Update Rotation
		Vector3 angles = transform.localEulerAngles;
		angles.y = 180;
		transform.localEulerAngles = angles;

		Rigidbody body = GetComponent <Rigidbody> ();		
		float additionalForce = Time.deltaTime * m_moveSpeed * m_moveFactor;
		
		//		if (body.velocity.x - additionalForce < -m_maxMoveSpeed)
		//		{
		//			additionalForce = m_maxMoveSpeed - 
		//		}
		
		if (body.velocity.x > -m_maxMoveSpeed)
		{
			body.AddForce (Vector3.left * additionalForce);
		}
		else
		{
			body.velocity = new Vector3 (-m_maxMoveSpeed, body.velocity.y, body.velocity.z);
		}

		m_isStopping = false;
	}

	public void moveInFacingDirection()
	{
		float rotation = transform.eulerAngles.y;

		if (rotation < 5 || rotation > 350)
		{
			moveRight ();
		}
		else
		{
			moveLeft();
		}
	}

	public void stop ()
	{
		m_isStopping = true;
	}

	public void snapToGround()
	{
		RaycastHit hitPoint;

		if (Physics.Raycast(transform.position, -transform.up, out hitPoint))
		{
			transform.position += (hitPoint.distance) * Vector3.down;
		}
	}

	void stopALittle ()
	{
		Rigidbody body = GetComponent <Rigidbody> ();

		float stoppingTolerance = 1f;
		
		float stoppingForce = Time.deltaTime * m_moveSpeed * m_moveFactor;

		if (body.velocity.x > stoppingTolerance)
		{
			body.AddForce (Vector3.left * stoppingForce);
		}
		else if (body.velocity.x < -stoppingTolerance)
		{
			body.AddForce (Vector3.right * stoppingForce);
		}
		else		
		{
			body.velocity = new Vector3 (0, body.velocity.y, body.velocity.z);
		}
	}

	void BindingAction.executeBinding (string actionName)
	{
//		// Setup rotation.
//		if (actionName == "left")
//		{
//			Vector3 angles = transform.localEulerAngles;
//			angles.y = 180;
//			transform.localEulerAngles = angles;
//		}
//		else if (actionName == "right")
//		{
//			Vector3 angles = transform.localEulerAngles;
//			angles.y = 0;
//			transform.localEulerAngles = angles;
//		}

		// Movement keys will not work on stairs and such.
		if (!isMovementEnabled)
		{
			return;
		}

		if (actionName == "left")
		{
			//move (new Vector3 (-1,0,0));
			moveLeft ();
		}
		else if (actionName == "right")
		{
			//move (new Vector3 (-1,0,0));
			moveRight ();
		}
	}

	void BindingAction.onBindingRelease (string actionName)
	{
		// This is set to "false" for every frame that a key is being held down.
		m_isStopping = true;
	}
}
