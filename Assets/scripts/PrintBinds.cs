using UnityEngine;
using System.Collections;

public class PrintBinds : MonoBehaviour {


	public string bindingString;
	public GameObject bindingPic;
	// Use this for initialization
	void Start () 
	{
		foreach (KeyBinding binding in GetComponent <KeyBindings> ().m_keyBindings)
		{
			if(binding.m_bindingName == bindingString)
			{
				for(int i = 0; i < GetComponent <KeyBinding> ().requiredKeysDown.Count; i++)
				{
					KeyCode keycode = GetComponent <KeyBinding> ().requiredKeysDown[i];
					string keybind = keycode.ToString();

					//Instantiate(bindingPic);
				}
			}
		}
	}
	
}
