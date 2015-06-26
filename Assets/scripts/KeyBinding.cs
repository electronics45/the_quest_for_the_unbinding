using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyBinding
{
	public BindingAction m_bindingAction;
	string m_bindingName;

	List <KeyCode> requiredKeysDown = new List<KeyCode>();

	public KeyBinding (string bindingName, BindingAction action)
	{
		m_bindingAction = action;
		m_bindingName = bindingName;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
		evaluateKeysDown ();
	}

	public void addKeyDownPress (KeyCode kCode)
	{
		requiredKeysDown.Add (kCode);
	}

	public void addKeyDownHold (KeyCode kCode)
	{
		requiredKeysDown.Add (kCode);
	}

	void evaluateKeysDown ()
	{
		if (requiredKeysDown.Count == 0) {
			// List empty.
			return;
		}

		foreach (KeyCode keyCode in requiredKeysDown)
		{
			if (!Input.GetKeyDown (keyCode))
			{
				return;
			}
		}

		// All keys are pressed.  Execute action if set.
		if (m_bindingAction != null) {
			m_bindingAction.executeBinding (m_bindingName);
		}
	}
}
