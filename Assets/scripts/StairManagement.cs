using UnityEngine;
using System.Collections;

public class StairManagement : MonoBehaviour, BindingAction
{
	GameObject player;

	public bool upDirectionIsRight = true;
	bool m_isInStairs;
	bool m_isAgainstStairs = false;

	// Use this for initialization
	void Start () {

		//yield return new WaitForSeconds (0.1f);
		player = GameObject.Find ("Player");

		player.GetComponent <KeyBindings> ().registerBindingAction ("stairs", this);

		// Setup default keyBindings.
		KeyBinding binding = new KeyBinding ("stairsDown", this);
		binding.setIsContinuous (true);
		binding.addKeyDown (KeyCode.Y);
	//	binding.addKeyDown (KeyCode.G);
		
		player.GetComponent <KeyBindings> ().aquireKeyBinding (binding);

//		binding = new KeyBinding ("stairsUp", this);
//		binding.setIsContinuous (true);
//		binding.addKeyDown (KeyCode.T);
//		//binding.addKeyDown (KeyCode.H);
//		
//		player.GetComponent <KeyBindings> ().aquireKeyBinding (binding);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isInStairs)
		{
			//player.GetComponent <Movement>().snapToGround();
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.name == "Player")
		{
			// Ther player is against the stairs.
			m_isAgainstStairs = true;
		}
	}

	void OnCollisionExit (Collision collision)
	{
		if (collision.gameObject.name == "Player")
		{
			// Ther player is against the stairs.
			//m_isAgainstStairs = false;
		}
	}

	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.gameObject.name == "Player")
		{
			// Turn off movement.
			player.GetComponent <Movement>().isMovementEnabled = false;
			player.GetComponent <Movement>().stop();

		//	Debug.Log ("in Stairs.");
			m_isInStairs = true;
		}
	}

	void OnTriggerExit (Collider otherCollider)
	{
		if (otherCollider.gameObject.name == "Player")
		{
		//	Debug.Log ("exit Stairs.");
			m_isInStairs = false;
			GetComponent <Collider> ().isTrigger = false;

			// Enable movement.
			player.GetComponent <Movement>().isMovementEnabled = true;
		}
	}

	public void setApproachingStairs (bool isApproachingStairs)
	{
		if (isApproachingStairs == false)
		{
			player.GetComponent <Movement> ().stop ();
		}

		m_isAgainstStairs = isApproachingStairs;
	}

	void moveIntoStairs()
	{
		//Debug.Log ("Setting trigger");
		// Turn Stairs into trigger.
		GetComponent <Collider> ().isTrigger = true;

		// move into the stairs a little.
		player.GetComponent <Movement> ().moveInFacingDirection ();
		player.GetComponent <Movement> ().moveInFacingDirection ();
		player.GetComponent <Movement> ().moveInFacingDirection ();
	}

//	void exitStairs()
//	{
//		
//	}

	void moveUpStairs()
	{
//		if (!m_isInStairs && !m_isAgainstStairs)
//		{
//			// Player is not near or in stairs.
//			return;
//		}

		if (m_isAgainstStairs)
		{
			moveIntoStairs();
		}

		if (upDirectionIsRight)
		{
			player.GetComponent <Movement> ().moveRight ();
		}
		else
		{
			player.GetComponent <Movement> ().moveLeft ();
		}
	}

	void moveDownStairs()
	{
		if (m_isAgainstStairs)
		{
			moveIntoStairs();
		}

		if (upDirectionIsRight)
		{
			player.GetComponent <Movement> ().moveLeft ();
		}
		else
		{
			player.GetComponent <Movement> ().moveRight ();
		}
	}

	void BindingAction.executeBinding (string actionName)
	{
		if (!m_isInStairs && !m_isAgainstStairs)
		{
			// Player is not near or in stairs.
			return;
		}

		if (actionName == "stairsDown")
		{
			moveDownStairs();
		}
		else if (actionName == "stairsUp")
		{
			moveUpStairs();
		}
	}
	
	void BindingAction.onBindingRelease (string actionName)
	{
		player.GetComponent <Movement> ().stop ();
	}
}
