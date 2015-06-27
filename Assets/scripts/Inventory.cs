using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public Vector3 nextKeyPos;

	public float xSeparation = 0.4;

	List <GameObject> keyBindings;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void drawKeyBinding (KeyBinding binding)
	{
		// Spawn prefab. With correct number of keys.

		// Move down to nextKeyPos.

		// Swap texture on keys.

		// Set description text.
	}

	public void drawAllKeybindings ()
	{

	}

	public void clearAllKeyBindings ();
}
