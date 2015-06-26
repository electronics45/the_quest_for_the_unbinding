using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyBinding
{
	public BindingAction m_bindingAction;
	public bool m_isContinuous = false;

	string m_bindingName;

	List <KeyCode> requiredKeysDown = new List<KeyCode>();
	bool m_isTriggeredDown = false;
	//bool m_isTriggerReleased = false;

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

	public void addKeyDown (KeyCode kCode)
	{
		requiredKeysDown.Add (kCode);
	}

//	public void addKeyDownHold (KeyCode kCode)
//	{
//		requiredKeysDown.Add (kCode);
//	}

	public void setIsContinuous (bool continuous)
	{
		m_isContinuous = continuous;
	}

	void evaluateKeysDown ()
	{
		if (requiredKeysDown.Count == 0) {
			// List empty.
			return;
		}

		foreach (KeyCode keyCode in requiredKeysDown)
		{
			if (!Input.GetKey (keyCode))
			{
				if (m_isTriggeredDown == true && m_bindingAction != null)
				{
					// Now releasing the keybinding.
					m_bindingAction.onBindingRelease (m_bindingName);
				}

				m_isTriggeredDown = false;
				return;
			}
		}

		// If we only want to trigger the action once, and we've
		// already triggered is once, return.
		if (!m_isContinuous && m_isTriggeredDown == true)
		{
			return;
		}

		// All required keys are down.
		m_isTriggeredDown = true;

		// All keys are pressed.  Execute action if set
		if (m_bindingAction != null)
		{
			m_bindingAction.executeBinding (m_bindingName);
		}
	}
}
