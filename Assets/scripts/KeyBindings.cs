using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyBindings : MonoBehaviour {

	public bool isUnbound = false;

	public List <KeyBinding> m_keyBindings = new List <KeyBinding>();

	Hashtable bindingActions = new Hashtable();

	List <BindingAction> allTheActions = new List<BindingAction>();
	//Hashtable allTheActions = new Hashtable();

//	public void registerAction (BindingAction action)
//	{
//		allTheActions.Add (action);
//	}

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
		// Only add this binding if it has not already been added.
//		foreach (KeyBinding bind in m_keyBindings)
//		{
//			if (bind.m_bindingName == binding.m_bindingName)
//			{
//				Debug.Log ("discarding binding: " + binding.m_bindingName);
//
//				return;
////				if (bind != binding)
////				{
////					discardKeyBinding (bind);
////				}
////				//m_keyBindings.Remove (bind);
////				break;
//			}
//		}
		Debug.Log ("aquiring binding: " + binding.m_bindingName + "With num bindings: " + binding.requiredKeysDown.Count);
		m_keyBindings.Add (binding);
	}

	public void discardKeyBinding (KeyBinding binding)
	{
		m_keyBindings.Remove (binding);
	}

	public BindingAction getBindingAction (string bindingName)
	{
		return (BindingAction) bindingActions [bindingName];
	}

	public void registerBindingAction (string bindingName, BindingAction action)
	{
		bindingActions [bindingName] = action;
		allTheActions.Add (action);
	}

	public void triggerBindingExecute (string actionName, string bindingTag)
	{
		foreach (BindingAction action in allTheActions)
		{
			if (action.getActionName() == actionName)
			{
				action.executeBinding (bindingTag);
			}
		}
	}
}

