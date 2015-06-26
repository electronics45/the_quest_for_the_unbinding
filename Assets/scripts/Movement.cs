using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour, BindingAction
{
	public float m_moveSpeed = 100;
	public float m_maxMoveSpeed = 10;

	float m_moveFactor = 60; // Expected 60 frames per second.

	bool m_isStopping = false;

	// Use this for initialization
	void Start () {
		KeyBinding binding = new KeyBinding ("left", this);
		binding.setIsContinuous (true);
		binding.addKeyDown (KeyCode.A);
		binding.addKeyDown (KeyCode.G);

		GetComponent <KeyBindings> ().aquireKeyBinding (binding);

		binding = new KeyBinding ("right", this);
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

	void moveRight ()
	{
		Rigidbody body = GetComponent <Rigidbody> ();

		float additionalForce = Time.deltaTime * m_moveSpeed * m_moveFactor;

//		if (body.velocity.x - additionalForce < -m_maxMoveSpeed)
//		{
//			additionalForce = m_maxMoveSpeed - 
//		}

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
	}

	void moveLeft ()
	{
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

		m_isStopping = false;
	}

	void BindingAction.onBindingRelease (string actionName)
	{
		// This is set to "false" for every frame that a key is being held down.
		m_isStopping = true;

		Debug.Log ("stopping");
	}
}
