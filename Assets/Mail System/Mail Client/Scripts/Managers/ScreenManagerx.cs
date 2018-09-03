using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScreenManager : MonoBehaviour
{
	
	public GUISkin skin;
	private List<string> users = new List<string> ();
	
	//  VARIABLES
	//******************************************************************
	private float scrW;
	private float scrH;
	private	float y = -40;
	//  BOOLEANS
	//******************************************************************
	private bool _showInfo = false;
	private bool _showUsers= false;
	//  STRINGS
	//******************************************************************
	private string sRespond = "";
	//  VECTORS
	//******************************************************************
	private Vector2 ViewVector = Vector2.zero;
	//  SETTERS ADN GETTERS
	//******************************************************************

	public void ShowRespond (string rsp)
	{
		sRespond = rsp;
		_showInfo = true;
	}

	public List<string> Users {
		get{ return users;}
	}
	//******************************************************************
	
	// Use this for initialization
	void Start ()
	{
		scrW = Screen.width;
		scrH = Screen.height;	
		Sys.Network.Connect ();
	}

	// Update is called once per frame
	void Update ()
	{
		scrW = Screen.width;
		scrH = Screen.height;
	}

	void OnGUI ()
	{
		//Set depth for this script GUI.
		GUI.depth = 1;
		GUI.skin = skin;
		
		GUI.Label (new Rect (scrW / 2 - 200, y, 400, 40), sRespond, "Field");
		if(_showUsers){
		GUI.Label (new Rect (scrW - 150,  scrH/2-(( scrH * 0.50f)/2), 150, scrH * 0.50f), "","Field");
		ViewVector = GUI.BeginScrollView (new Rect (scrW - 150,  scrH/2-(( scrH * 0.50f)/2), 150, scrH * 0.50f), ViewVector, new Rect (0, 0, 130, (30 * users.Count)));
		for (int cnt=0; cnt < users.Count; cnt++) {
			if (GUI.Button (new Rect (0, 0 + (cnt * 30), 130, 30), users [cnt],"Content")){
				Sys.Mail.Receiver = users[cnt];
			}
				
		}
		GUI.EndScrollView ();
		}
		if (_showInfo) {
			StartCoroutine ("playAnimShow");
			StopCoroutine ("playAnimHide");
		} else {
			StopCoroutine ("playAnimShow");
			StartCoroutine ("playAnimHide");
		}
		if (GUI.Button (new Rect (scrW - 150, scrH/2-(( scrH * 0.65f)/2), 150, scrH * 0.08f), "Connected Users", "Content")) {
		_showUsers = !_showUsers;	
		}
		if (!Sys.Mail.Enable)
			HUD ();
		
	}
		
	IEnumerator playAnimShow ()
	{	  
		y = iTween.FloatUpdate (y, 0, 10);
		yield return new WaitForSeconds(3);
		_showInfo = false;
	}

	IEnumerator playAnimHide ()
	{  
		y = iTween.FloatUpdate (y, -40, 5);
		yield return new WaitForSeconds(2);
	}
	
	//HUD layout
	private void HUD ()
	{
		
		Sys.Mail.UName = GUI.TextField (new Rect (Screen.width / 2 - 75, scrH / 2 - 30, 150, 30), Sys.Mail.UName, 25);
		if (GUI.Button (new Rect (Screen.width / 2 - 75, scrH / 2 + 10, 150, 40), "Login")) {
			//Chech if connection to server is established.
			if (Network.isClient) {
				Sys.Mail.Con();
				Sys.Mail.Enable = true;
			} else {
				ShowRespond ("No connection established.");
				
			}
		}
		
		
		
	}


}
