using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour, BindingAction {

	public float lowJumpForce = 30;
	public float highJumpForce = 100;

	float distToGround;

	GameObject player;

	string BindingAction.getActionName()
	{
		return "Jump";
	}

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

		distToGround = GetComponent <Collider> ().bounds.extents.y;

		player.GetComponent <KeyBindings> ().registerBindingAction ("jump", this);

		// Setup default keybindings
		KeyBinding binding = new KeyBinding ("low_jump", this);
		binding.m_funnyText = "Perform a feeble excuse for 'jump'.";
		binding.addKeyDown (KeyCode.Space);
		GetComponent <KeyBindings> ().aquireKeyBinding (binding);

		binding = new KeyBinding ("high_jump", this);
		binding.m_funnyText = "Perferm a right proper jump.";
		binding.addKeyDown (KeyCode.Space);
		binding.addKeyDown (KeyCode.LeftControl);
		GetComponent <KeyBindings> ().aquireKeyBinding (binding);
	}

	// Update is called once per frame
	void Update () {
	
	}

	bool isGrounded ()
	{
		return Physics.Raycast (transform.position - distToGround * Vector3.down, Vector3.down, distToGround + 0.01f);
	}

	void BindingAction.executeBinding (string actionName)
	{
		// Verify that we're on the ground.
		if (!isGrounded ())
		{
			// No air jump. |:-[
			return;
		}

		if (actionName == "low_jump")
		{
			jump (lowJumpForce);
		}
		else if (actionName == "high_jump")
		{
			jump (highJumpForce);
		}
	}
	
	void BindingAction.onBindingRelease (string actionName)
	{

	}

	void jump (float force)
	{
		Rigidbody body = GetComponent <Rigidbody> ();

		body.AddForce (0, force, 0);
	}
}
