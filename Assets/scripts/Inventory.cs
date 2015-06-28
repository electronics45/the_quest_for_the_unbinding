using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour, BindingAction {

	public Vector3 startKeyPos = new Vector3 (-2.785f, 0.129f, 0.0f);

	public float ySeparation = 0.4f;

	Vector3 nextKeyPos;
	bool hasDrawnInventory = false;

	List <GameObject> keyBindings;
	Hashtable keyTextureMap = new Hashtable();

	KeyBindings allBindings;
	GameObject player;

	string BindingAction.getActionName()
	{
		return "Inventory";
	}

	//GameObject InventoryRoot;

	// Use this for initialization
	void Start () {
		mapKeyTextures ();

		player = GameObject.Find ("Player");
		allBindings = player.GetComponent <KeyBindings> ();

		nextKeyPos = startKeyPos;

//		// Setup default keybinding.
//		KeyBinding binding = new KeyBinding ("open", GameObject.Find ("Player").GetComponent <Jump>());
//		binding.m_funnyText = "Aquire the orb of Osiris.";
//		binding.addKeyDown (KeyCode.Semicolon);
//		binding.addKeyDown (KeyCode.K);
//		binding.addKeyDown (KeyCode.Space);
		
		//player.GetComponent <KeyBindings> ().aquireKeyBinding (binding);

		//drawKeyBinding (binding);

		// Setup default keybinding.
		KeyBinding binding = new KeyBinding ("inv", this);
		binding.m_funnyText = "Review the scroll of key bindings.";
		binding.addKeyDown (KeyCode.F1);
		player.GetComponent <KeyBindings> ().aquireKeyBinding (binding);

		player.GetComponent <KeyBindings> ().registerBindingAction ("inventory", this);

		//drawAllKeybindings ();

		//gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		// Show/hide inventory with "F1" key.

		//clearAllKeyBindings ();
		//drawAllKeybindings ();

		if (!hasDrawnInventory)
		{
			drawAllKeybindings ();
			
			gameObject.SetActive (false);

			hasDrawnInventory = true;
		}
	}

	void BindingAction.executeBinding (string actionName)
	{
		if (gameObject.activeSelf)
		{
			gameObject.SetActive (false);
		}
		else
		{
			gameObject.SetActive (true);
		}
	}
	
	void BindingAction.onBindingRelease (string actionName)
	{

	}

	public void drawKeyBinding (KeyBinding binding)
	{
		GameObject prefab;
		GameObject newInvBindObj;

		// Determin correct prefab based on number of required keys.
		if (binding.requiredKeysDown.Count == 3)
		{
			//Debug.Log ("correct Texutre");
			prefab = (GameObject) Resources.Load ("prefab/BindingInv3", typeof(GameObject));
			//Debug.Log ("prefab: " + prefab.name);
		}
		else if (binding.requiredKeysDown.Count == 2)
		{
			prefab = (GameObject) Resources.Load ("prefab/BindingInv2", typeof(GameObject));
		}
		else
		{
			prefab = (GameObject) Resources.Load ("prefab/BindingInv1", typeof(GameObject));
		}

		// Spawn prefab. With correct number of keys.
		newInvBindObj = ((Transform) Instantiate (prefab.transform)).gameObject;

		// Set up it's parent.
		newInvBindObj.transform.SetParent (gameObject.transform);

        newInvBindObj.transform.localPosition = nextKeyPos;

		int i = 0;
		// Swap texture on keys.
		foreach (Transform child in newInvBindObj.transform)
		{

			if (child.name == "BindingDescription")
			{
				// Set description text.
				child.GetComponent <TextMesh>().text = binding.m_funnyText;
			}
			else
			{
				// This is assumed to be a key object.  Set it's texture.
				KeyCode code = binding.requiredKeysDown [i];

				//Debug.Log ("code: " + code);

				child.GetComponent <Renderer>().material.mainTexture = (Texture) keyTextureMap [code];

				++i;
			}

		}

		// Increment nextKeyPos
		nextKeyPos.y -= ySeparation;
	}

	public void drawAllKeybindings ()
	{
		List <string> drawnBindings = new List<string>();

		foreach (KeyBinding binding in player.GetComponent <KeyBindings> ().m_keyBindings)
		{
			if (drawnBindings.Contains (binding.m_bindingName))
			{
				continue;
			}

			Debug.Log ("Drawing: " + binding.m_funnyText);
			drawKeyBinding (binding);

			drawnBindings.Add (binding.m_bindingName);
		}
	}

	public void clearAllKeyBindings ()
	{
		nextKeyPos = startKeyPos;

		// Clear all children with the "invBinding" tag.
		foreach (Transform child in transform)
		{
			if (child.tag == "invBinding")
			{
				Destroy (child.gameObject);
			}
		}
	}

	// How tedious. :(
	void mapKeyTextures ()
	{
		keyTextureMap [KeyCode.Minus] = (Texture2D) Resources.Load ("textures/KeyDiff/-");
		keyTextureMap [KeyCode.LeftBracket] = (Texture2D) Resources.Load ("textures/KeyDiff/[");
		keyTextureMap [KeyCode.RightBracket] = (Texture2D) Resources.Load ("textures/KeyDiff/]");
		keyTextureMap [KeyCode.Plus] = (Texture2D) Resources.Load ("textures/KeyDiff/+");
		keyTextureMap [KeyCode.Equals] = (Texture2D) Resources.Load ("textures/KeyDiff/=");
		keyTextureMap [KeyCode.Alpha0] = (Texture2D) Resources.Load ("textures/KeyDiff/0");
		keyTextureMap [KeyCode.Alpha1] = (Texture2D) Resources.Load ("textures/KeyDiff/1");
		keyTextureMap [KeyCode.Alpha2] = (Texture2D) Resources.Load ("textures/KeyDiff/2");
		keyTextureMap [KeyCode.Alpha3] = (Texture2D) Resources.Load ("textures/KeyDiff/3");
		keyTextureMap [KeyCode.Alpha4] = (Texture2D) Resources.Load ("textures/KeyDiff/4");
		keyTextureMap [KeyCode.Alpha5] = (Texture2D) Resources.Load ("textures/KeyDiff/5");
		keyTextureMap [KeyCode.Alpha6] = (Texture2D) Resources.Load ("textures/KeyDiff/6");
		keyTextureMap [KeyCode.Alpha7] = (Texture2D) Resources.Load ("textures/KeyDiff/7");
		keyTextureMap [KeyCode.Alpha8] = (Texture2D) Resources.Load ("textures/KeyDiff/8");
		keyTextureMap [KeyCode.Alpha9] = (Texture2D) Resources.Load ("textures/KeyDiff/9");
		keyTextureMap [KeyCode.A] = (Texture2D) Resources.Load ("textures/KeyDiff/A");
		keyTextureMap [KeyCode.AltGr] = (Texture2D) Resources.Load ("textures/KeyDiff/Alt");
		keyTextureMap [KeyCode.LeftAlt] = (Texture2D) Resources.Load ("textures/KeyDiff/Alt");
		keyTextureMap [KeyCode.RightAlt] = (Texture2D) Resources.Load ("textures/KeyDiff/Alt");
		keyTextureMap [KeyCode.B] = (Texture2D) Resources.Load ("textures/KeyDiff/B");
		keyTextureMap [KeyCode.Backslash] = (Texture2D) Resources.Load ("textures/KeyDiff/BACKSLASH");
		keyTextureMap [KeyCode.C] = (Texture2D) Resources.Load ("textures/KeyDiff/C");
		keyTextureMap [KeyCode.Comma] = (Texture2D) Resources.Load ("textures/KeyDiff/COMMA");
		keyTextureMap [KeyCode.LeftControl] = (Texture2D) Resources.Load ("textures/KeyDiff/Ctrl");
		keyTextureMap [KeyCode.RightControl] = (Texture2D) Resources.Load ("textures/KeyDiff/Ctrl");
		keyTextureMap [KeyCode.D] = (Texture2D) Resources.Load ("textures/KeyDiff/D");
		keyTextureMap [KeyCode.E] = (Texture2D) Resources.Load ("textures/KeyDiff/E");
		keyTextureMap [KeyCode.F] = (Texture2D) Resources.Load ("textures/KeyDiff/F");
		keyTextureMap [KeyCode.F1] = (Texture2D) Resources.Load ("textures/KeyDiff/F1");
		keyTextureMap [KeyCode.F2] = (Texture2D) Resources.Load ("textures/KeyDiff/F2");
		keyTextureMap [KeyCode.F3] = (Texture2D) Resources.Load ("textures/KeyDiff/F3");
		keyTextureMap [KeyCode.F4] = (Texture2D) Resources.Load ("textures/KeyDiff/F4");
		keyTextureMap [KeyCode.F5] = (Texture2D) Resources.Load ("textures/KeyDiff/F5");
		keyTextureMap [KeyCode.F6] = (Texture2D) Resources.Load ("textures/KeyDiff/F6");
		keyTextureMap [KeyCode.F7] = (Texture2D) Resources.Load ("textures/KeyDiff/F7");
		keyTextureMap [KeyCode.F8] = (Texture2D) Resources.Load ("textures/KeyDiff/F8");
		keyTextureMap [KeyCode.F9] = (Texture2D) Resources.Load ("textures/KeyDiff/F9");
		keyTextureMap [KeyCode.F10] = (Texture2D) Resources.Load ("textures/KeyDiff/F10");
		keyTextureMap [KeyCode.F11] = (Texture2D) Resources.Load ("textures/KeyDiff/F11");
		keyTextureMap [KeyCode.F12] = (Texture2D) Resources.Load ("textures/KeyDiff/F12");
		keyTextureMap [KeyCode.F13] = (Texture2D) Resources.Load ("textures/KeyDiff/F13");
		keyTextureMap [KeyCode.F14] = (Texture2D) Resources.Load ("textures/KeyDiff/F14");
		keyTextureMap [KeyCode.F15] = (Texture2D) Resources.Load ("textures/KeyDiff/F15");
		keyTextureMap [KeyCode.G] = (Texture2D) Resources.Load ("textures/KeyDiff/G");
		keyTextureMap [KeyCode.H] = (Texture2D) Resources.Load ("textures/KeyDiff/H");
		keyTextureMap [KeyCode.I] = (Texture2D) Resources.Load ("textures/KeyDiff/I");
		keyTextureMap [KeyCode.J] = (Texture2D) Resources.Load ("textures/KeyDiff/J");
		keyTextureMap [KeyCode.K] = (Texture2D) Resources.Load ("textures/KeyDiff/K");
		keyTextureMap [KeyCode.L] = (Texture2D) Resources.Load ("textures/KeyDiff/L");
		keyTextureMap [KeyCode.M] = (Texture2D) Resources.Load ("textures/KeyDiff/M");
		keyTextureMap [KeyCode.N] = (Texture2D) Resources.Load ("textures/KeyDiff/N");
		keyTextureMap [KeyCode.O] = (Texture2D) Resources.Load ("textures/KeyDiff/O");
		keyTextureMap [KeyCode.P] = (Texture2D) Resources.Load ("textures/KeyDiff/P");
		keyTextureMap [KeyCode.Period] = (Texture2D) Resources.Load ("textures/KeyDiff/PERIOD");
		keyTextureMap [KeyCode.Q] = (Texture2D) Resources.Load ("textures/KeyDiff/Q");
		keyTextureMap [KeyCode.Quote] = (Texture2D) Resources.Load ("textures/KeyDiff/QUOTATIONMARK");
		keyTextureMap [KeyCode.R] = (Texture2D) Resources.Load ("textures/KeyDiff/R");
		keyTextureMap [KeyCode.S] = (Texture2D) Resources.Load ("textures/KeyDiff/S");
		keyTextureMap [KeyCode.Semicolon] = (Texture2D) Resources.Load ("textures/KeyDiff/SEMICOLON");
		keyTextureMap [KeyCode.LeftShift] = (Texture2D) Resources.Load ("textures/KeyDiff/Shift");
		keyTextureMap [KeyCode.RightShift] = (Texture2D) Resources.Load ("textures/KeyDiff/Shift");
		keyTextureMap [KeyCode.Slash] = (Texture2D) Resources.Load ("textures/KeyDiff/SLASH");
		keyTextureMap [KeyCode.Space] = (Texture2D) Resources.Load ("textures/KeyDiff/Space");
		keyTextureMap [KeyCode.T] = (Texture2D) Resources.Load ("textures/KeyDiff/T");
		keyTextureMap [KeyCode.U] = (Texture2D) Resources.Load ("textures/KeyDiff/U");
		keyTextureMap [KeyCode.V] = (Texture2D) Resources.Load ("textures/KeyDiff/V");
		keyTextureMap [KeyCode.W] = (Texture2D) Resources.Load ("textures/KeyDiff/W");
		keyTextureMap [KeyCode.X] = (Texture2D) Resources.Load ("textures/KeyDiff/X");
		keyTextureMap [KeyCode.Y] = (Texture2D) Resources.Load ("textures/KeyDiff/Y");
		keyTextureMap [KeyCode.Z] = (Texture2D) Resources.Load ("textures/KeyDiff/Z");




		//Debug.Log ("texutre loaded for '" + KeyCode.Minus + "': " + keyTextureMap [KeyCode.Minus].ToString());
		//Debug.Log (((Texture2D) Resources.Load ("KeyDiff/A")).name);
	}
}

