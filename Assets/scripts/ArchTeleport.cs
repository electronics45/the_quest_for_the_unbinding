using UnityEngine;
using System.Collections;

public class ArchTeleport : MonoBehaviour
{
	public ArchTeleport adjoiningDoor;

	GameObject player;
	GameObject cameraObj;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		cameraObj = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.name == "Player")
		{
			teleport ();
		}
	}

	void teleport ()
	{
		// Move player to new position.
		Vector3 newPos = player.transform.position;
		float doorSpawnOffset = 1;
		
		newPos.x = adjoiningDoor.gameObject.transform.position.x;
		newPos.y = adjoiningDoor.gameObject.transform.position.y + 1f;
		
//		if (!adjoiningDoor.isFrontFacingDoor)
//		{
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
//		}
		
		player.transform.position = newPos;
		
		player.GetComponent <Movement>().snapToGround ();
		
		// Snap camera to new position.
		cameraObj.GetComponent <CameraMovement> ().cameraSnapToPlayer ();
	}
}
