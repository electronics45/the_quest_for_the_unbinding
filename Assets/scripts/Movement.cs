using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour, BindingAction
{
	public float moveSpeed = 100000;
	public float maxMoveSpeed = 1000000;

	// Use this for initialization
	void Start () {
		KeyBinding binding = new KeyBinding ("left", this);
		binding.addKeyDown (KeyCode.A);

		GetComponent <KeyBindings> ().aquireKeyBinding (binding);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void move (Vector3 direction)
	{
		Rigidbody body = GetComponent <Rigidbody> ();

		if (body.velocity.sqrMagnitude < maxMoveSpeed * maxMoveSpeed) {
			body.AddForce (direction * (moveSpeed * Time.deltaTime));
		}
	}

	void BindingAction.executeBinding (string actionName)
	{
		if (actionName == "left")
		{
			move (new Vector3 (1,1,1));
		}
	}
}
