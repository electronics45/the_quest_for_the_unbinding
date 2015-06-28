using UnityEngine;
using System.Collections;

public class DoorActuation : MonoBehaviour, BindingAction
{
	public DoorActuation adjoiningDoor;
	public bool isFrontFacingDoor = true;

	bool playerIsAtDoor = false;
	GameObject player;
	GameObject cameraObj;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		cameraObj = GameObject.Find ("Main Camera");

		player.GetComponent <KeyBindings> ().registerBindingAction ("openDoor", this);

		// Setup default keybinding.
		KeyBinding binding = new KeyBinding ("open", this);
		binding.m_funnyText = "Actuate door-handle of the ordinary variety.";
		binding.addKeyDown (KeyCode.LeftControl);
		binding.addKeyDown (KeyCode.LeftBracket);
		
		player.GetComponent <KeyBindings> ().aquireKeyBinding (binding);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider otherCollider)
	{
		playerIsAtDoor = true;
	}

	void OnTriggerExit (Collider otherCollider)
	{
		playerIsAtDoor = false;
	}

	void BindingAction.executeBinding (string actionName)
	{
		if (actionName == "open" && playerIsAtDoor)
		{
			teleportToAdjoiningDoor();
		}
	}
	
	void BindingAction.onBindingRelease (string actionName)
	{

	}

	void teleportToAdjoiningDoor ()
	{
		// Move player to new position.
		Vector3 newPos = player.transform.position;
		float doorSpawnOffset = 1;

		newPos.x = adjoiningDoor.gameObject.transform.position.x;
		newPos.y = adjoiningDoor.gameObject.transform.position.y + 1f;

		if (!adjoiningDoor.isFrontFacingDoor)
		{
			Vector3 doorRotation = adjoiningDoor.gameObject.transform.localEulerAngles;
			if (doorRotation.y > 85 && doorRotation.y < 100)
			{
				newPos.x -= doorSpawnOffset;
				// Set Player's rotation.
				player.transform.eulerAngles = new Vector3 (0,180,0);
			}
			else
			{
				newPos.x += doorSpawnOffset;
				// Set Player's rotation.
				player.transform.eulerAngles = new Vector3 (0,0,0);
			}
		}

		player.transform.position = newPos;

		player.GetComponent <Movement>().snapToGround ();

		// Snap camera to new position.
		cameraObj.GetComponent <CameraMovement> ().cameraSnapToPlayer ();
	}
}
