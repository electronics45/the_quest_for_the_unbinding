using UnityEngine; // 41 Post - Created by DimasTheDriver on Dec/12/2011 . Part of the 'Unity: How to create a speech balloon' post. Available at: http://www.41post.com/?p=4545 
using System.Collections;



[ExecuteInEditMode]
public class SpeechBubble : MonoBehaviour 
{
	//this game object's transform
	private Transform goTransform;
	//the game object's position on the screen, in pixels
	private Vector3 goScreenPos;
	//the game objects position on the screen
	private Vector3 goViewportPos;
	
	//the width of the speech bubble
	public int bubbleWidth = 200;
	//the height of the speech bubble
	public int bubbleHeight = 100;
	
	//an offset, to better position the bubble 
	public float offsetX = 0;
	public float offsetY = 150;
	
	//an offset to center the bubble 
	private int centerOffsetX;
	private int centerOffsetY;
	
	//a material to render the triangular part of the speech balloon
	public Material mat;
	//a guiSkin, to render the round part of the speech balloon
	public GUISkin guiSkin;
	
	//use this for early initialization
	void Awake() 
	{
		//get this game object's transform
		goTransform = this.GetComponent<Transform>();
	}
	
	//use this for initialization
	void Start()
	{
		//if the material hasn't been found
		if (!mat) 
		{
			Debug.LogError("Please assign a material on the Inspector.");
			return;
		}
		
		//if the guiSkin hasn't been found
		if (!guiSkin) 
		{
			Debug.LogError("Please assign a GUI Skin on the Inspector.");
			return;
		}
		
		//Calculate the X and Y offsets to center the speech balloon exactly on the center of the game object
		centerOffsetX = bubbleWidth/2;
		centerOffsetY = bubbleHeight/2;
	}
	
	
	//Called once per frame, after the update
	void LateUpdate() 
	{
		//find out the position on the screen of this game object
		goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);	
		
		//Could have used the following line, instead of lines 70 and 71
		//goViewportPos = Camera.main.WorldToViewportPoint(goTransform.position);
		goViewportPos.x = goScreenPos.x/(float)Screen.width;
		goViewportPos.y = goScreenPos.y/(float)Screen.height;
	}

	public DialogueManager dialogueManager;

	private bool isBalloonVisible = false;

	private string BalloonPhrase = "";
	//Enable balloon with specific text
	public void Say(string phrase){
		isBalloonVisible = true;
		BalloonPhrase = phrase;
	}

	public void StopTalk(){
		isBalloonVisible = false;
		BalloonPhrase = "";
	}


	//gui properties
	private int button_height = 20;
	private int button_width = 50;
	private int button_offset = 10;
	//Draw GUIs
	void OnGUI()
	{
		if (isBalloonVisible) {
			Rect rct = GUILayoutUtility.GetRect(new GUIContent(BalloonPhrase),
			                         GUIStyle.none);
			Rect groupRect = new Rect();
			groupRect.height = rct.height + button_height + button_offset + 50;
			groupRect.width = rct.width + 50;
			groupRect.x = goScreenPos.x - centerOffsetX - offsetX;
			groupRect.y = Screen.height - goScreenPos.y - centerOffsetY - offsetY;
			//Begin the GUI group centering the speech bubble at the same position of this game object. After that, apply the offset
			GUI.BeginGroup (groupRect);
			
			//Render the round part of the bubble
			GUI.Label (new Rect (0, 0, rct.width+50, rct.height + 50), "", guiSkin.customStyles [0]);
			
			//Render the text
			GUI.Label (new Rect (15, 15, rct.width, rct.height + 30), BalloonPhrase, guiSkin.label);
		
			//If the button is pressed, go to next phrase
			if (GUI.Button (new Rect (groupRect.width/2 - 15, rct.height + button_offset + 10, 30, 20), "->")) {
				if(dialogueManager){
					dialogueManager.NextPhrase();
				}
			}
		
			GUI.EndGroup ();
		}
	}
	
	//Called after camera has finished rendering the scene
	void OnRenderObject()
	{
		if (isBalloonVisible) {
			//push current matrix into the matrix stack
			GL.PushMatrix ();
			//set material pass
			mat.SetPass (0);
			//load orthogonal projection matrix
			GL.LoadOrtho ();
			//a triangle primitive is going to be rendered
			GL.Begin (GL.TRIANGLES);
	
			//set the color
			GL.Color (Color.white);
			
			//Define the triangle vetices
			GL.Vertex3 (goViewportPos.x, goViewportPos.y + (offsetY / 3) / Screen.height, 0.1f);
			GL.Vertex3 (goViewportPos.x - (bubbleWidth / 3) / (float)Screen.width, goViewportPos.y + offsetY / Screen.height, 0.1f);
			GL.Vertex3 (goViewportPos.x - (bubbleWidth / 8) / (float)Screen.width, goViewportPos.y + offsetY / Screen.height, 0.1f);
		
			GL.End ();
			//pop the orthogonal matrix from the stack
			GL.PopMatrix ();
		}
	}

}