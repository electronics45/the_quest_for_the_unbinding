using UnityEngine;
using System.Collections;

public class Rebind : MonoBehaviour {

	// Use this for initialization
	public void rebindLeft () {

		foreach (KeyBinding binding in GetComponent <KeyBindings> ().m_keyBindings)
		{
			if(binding.m_bindingName == "left")
			{
				GetComponent <KeyBindings> ().discardKeyBinding (binding);
			}
		}

		if (Event.current.type == EventType.KeyDown) 
		{
			KeyCode newBinding = Event.current.keyCode;
			KeyBinding binding = new KeyBinding ("left", GetComponent <Movement> ());
			binding.setIsContinuous (true);
			binding.addKeyDown (newBinding);
			GetComponent <KeyBindings> ().aquireKeyBinding (binding);
		}
	}
}
