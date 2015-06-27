using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	GameObject player;
	float camTargetHeight;
	bool fixedUpdatePerformed = false;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player");

		camTargetHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (fixedUpdatePerformed)
		{
			cameraLagLerp();
			fixedUpdatePerformed = false;
		}
	}

	void FixedUpdate()
	{
		fixedUpdatePerformed = true;

	}

	void cameraLagLerp ()
	{
		float lerpWeight = 2;

		Vector3 targetPos = player.gameObject.transform.position;

		Vector3 newPos = transform.transform.position;

		// We only want to move in the x, and y dimensions.
		//targetPos = new Vector3 (targetPos.x, targetPos.y, transform.position.x);

		newPos.x = Mathf.Lerp (transform.position.x, targetPos.x, lerpWeight * Time.deltaTime);
		newPos.y = Mathf.Lerp (transform.position.y, targetPos.y + camTargetHeight, lerpWeight * Time.deltaTime);

		transform.position = newPos;
	}

	public void cameraSnapToPlayer ()
	{
		Vector3 targetPos = gameObject.transform.position;

		targetPos.x = player.gameObject.transform.position.x;
		targetPos.y = player.gameObject.transform.position.y + camTargetHeight;

		transform.position = targetPos;
	}
}
