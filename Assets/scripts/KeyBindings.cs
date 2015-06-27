﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyBindings : MonoBehaviour {

	public bool isUnbound = false;

	public List <KeyBinding> m_keyBindings = new List <KeyBinding>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Update/check the keybindings for there keys being down.
		foreach (KeyBinding binding in m_keyBindings) {
			binding.Update();
		}
	}

	public void aquireKeyBinding (KeyBinding binding)
	{
		m_keyBindings.Add (binding);
	}

	public void discardKeyBinding (KeyBinding binding)
	{
		m_keyBindings.Remove (binding);
	}
}

