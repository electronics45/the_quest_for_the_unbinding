using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Xml;

public class DialogueManager : MonoBehaviour {
	private List<KeyValuePair<string, List<KeyValuePair<string, string>>>> dialogs; 

	List<KeyValuePair<string, string>> current_dialog_phrases;
	private int CurrentPhrase;

	public string defaultDialogId = "default";

	void Awake() 
	{
		CurrentPhrase = 0;
		current_dialog_phrases = new List<KeyValuePair<string, string>> ();
		//parse xml for dialogs
		//FileStream ReadFileStream = new FileStream(@"", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
		XmlDocument doc = new XmlDocument();
		doc.Load("C:\\Users\\alexander.rzhevskiy\\Documents\\New Unity Project\\dialogs.xml");
		dialogs = new List<KeyValuePair<string, List<KeyValuePair<string, string>>>> ();
		foreach(XmlNode dialog in doc.DocumentElement.ChildNodes){
			List<KeyValuePair<string, string>> dialog_phrases = new List<KeyValuePair<string, string>>();
			//for every dialog create pair of its id and array of phrases of that dialog
			foreach(XmlNode phrase in dialog.ChildNodes){
				//assumed that this characters are invalid once in text
				string text = phrase.InnerText;
				text = text.Replace("\t", "");
				text = text.Replace("\n", "");
				text = text.Replace("\\n", "\n");
				//save phrase into list
				dialog_phrases.Add(new KeyValuePair<string, string>(phrase.Attributes["actor"].InnerText, text));
			}
			//save array of phrases to dialog
			dialogs.Add( new KeyValuePair<string, List<KeyValuePair<string, string>>>(dialog.Attributes["id"].InnerText, dialog_phrases));
		}

	}


	public void RunNewDialog(string id){
		SetDialog (id);
		if (current_dialog_phrases.Count > 0){
			NextPhrase ();
		}
	}

	public void RunCurrentDialog(){
		NextPhrase();
	}

	public void SetDialog(string id){
		CurrentPhrase = 0;
		//find dialog with requested id
		foreach (KeyValuePair<string, List<KeyValuePair<string, string>>> dialog in dialogs) {
			if (dialog.Key == id){
				current_dialog_phrases = new List<KeyValuePair<string, string>> (dialog.Value);
				break;
			}
		}
	}

	public void NextPhrase(){
		GameObject actor;
		GameObject prevActor;
		SpeechBubble bubble;
		if (CurrentPhrase < current_dialog_phrases.Count) {
			//run phrase
			actor = GameObject.FindGameObjectWithTag(current_dialog_phrases[CurrentPhrase].Key);

			if(!actor){
				return;
			}

			//if not the first phrase hide previous
			if(CurrentPhrase > 0){
				prevActor = GameObject.FindGameObjectWithTag(current_dialog_phrases[CurrentPhrase - 1].Key);
				bubble = prevActor.GetComponent<SpeechBubble>() as SpeechBubble;
				bubble.StopTalk();
			}

			bubble = actor.GetComponent<SpeechBubble>() as SpeechBubble;
			if(!bubble){
				return;
			}
			bubble.Say (current_dialog_phrases[CurrentPhrase].Value);
			CurrentPhrase++;
		} else {
			//finish dialog
			FinishDialog();
		}
	}

	public void FinishDialog(){
		GameObject actor;
		SpeechBubble bubble;
		foreach (KeyValuePair<string, string> phrase in current_dialog_phrases) {
			actor = GameObject.FindGameObjectWithTag(phrase.Key);
			if(!actor){
				return;
			}
			bubble = actor.GetComponent<SpeechBubble>() as SpeechBubble;
			if(!bubble){
				return;
			}
			bubble.StopTalk();
		}
		CurrentPhrase = 0;
	}

	public void StopDialog(){
		CurrentPhrase = 0;
	}
}
