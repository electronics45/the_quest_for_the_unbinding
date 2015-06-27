using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BindingPickup : MonoBehaviour {
	public string bindingActionName;

	public List <KeyCode> requiredDownKeys;
	public string bindgingDescription;
	public string bindingId;
	public bool isContinuous = false;

	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.name == "Player")
		{
			// Aquire the keybinding.
			aquireBinding();
		}
	}

	void aquireBinding ()
	{
		KeyBindings allBindings = GameObject.Find ("Player").GetComponent <KeyBindings> ();

		BindingAction action = player.GetComponent <KeyBindings> ().getBindingAction (bindingActionName);

		KeyBinding keyBinding = new KeyBinding (bindingId, action);
		keyBinding.setIsContinuous (isContinuous);

		foreach (KeyCode keyCode in requiredDownKeys)
		{
			keyBinding.addKeyDown (keyCode);
		}

		keyBinding.m_funnyText = bindgingDescription;

		allBindings.aquireKeyBinding (keyBinding);

		gameObject.SetActive (false);
	}
}
