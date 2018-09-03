using UnityEngine;
using System.Collections;
using System;

public class NetworkMessages : MonoBehaviour
{
	//Server Side
	[RPC] 
	public  void OnConnect (string name)
	{	
	}
	
	[RPC] 
	public  void OnSendMail (string sender, string receiver, string subject, string message)
	{	
	}
	
	[RPC] 
	public  void OnMailRequest (string name)
	{
	}

	[RPC] 
	public  void OnSentRequest (string name)
	{
	}

	[RPC] 
	public  void OnDraftRequest (string name)
	{
	}
	
	[RPC] 
	public  void OnDeleteMail (string box, int id)
	{	
	}

	[RPC] 
	public  void OnReadMail (int id)
	{	
	}

	[RPC] 
	public  void OnSave (string subject, string message)
	{	
	}
	
	//Client side
	[RPC]
	public void UpdateBoxes (int box, string cont)
	{	//For testing. Defaut disable.
		//Debug.Log ("Box " + box + " Message " + cont);
		
		//Split content string get each parameter.
		string[] data = cont.Split ('~'); 
		/*Each parameter meaning.
		[0] - ID
		[1] - Inbox - Sender/ Sent - Receiver/ Draft - Subject
		[2] - Inbox, Sent - Subject/ Draft - Message 
		[3] - Inbox, Sent - Message/ Draft - Save Date
		[4] - Inbox, Sent - Date/ Draft - None
		[5] - Inbox - isChecked/ Sent, Draft - None
		*/
		if (box == 0) {	
			//Detect if it's new or old message.
			if ((data [5]) == "0") {
				Sys.Mail.MailBox.Add (new Message (int.Parse (data [0]), data [1], data [2], data [3], data [4], false));
			} else if ((data [5]) == "1") {
				Sys.Mail.MailBox.Add (new Message (int.Parse (data [0]), data [1], data [2], data [3], data [4], true));
			}
		} else if (box == 1) {
			Sys.Mail.Draft.Add (new Message (int.Parse (data [0]), "", data [1], data [2], data [3], true));
		} else if (box == 2) {
			Message msg = new Message (int.Parse (data [0]), data [1], data [2], data [3], data [4], true);
			msg.Receiver = data [2];
			Sys.Mail.Sent.Add (msg);
		}
	}

	[RPC]
	public  void NewMail (int id, string from, string subject, string content, string date)
	{
		//Add new message to mail box.
		Sys.Mail.MailBox.Add (new Message (id, from, subject, content, date, false));
		//Notify user about new message. 
		Sys.Screen.ShowRespond (" --- You have new message from user [ " + from + " ]");
	}

	[RPC]
	public  void OnResponde (int status)
	{

		//Implement your notification system here. Don't use this, because it's just for demostration purpose.
		if (status == 0) {
			Sys.Screen.ShowRespond ("Your message was sent.");
		} else if (status == 1) {
			Sys.Screen.ShowRespond ("Something went wrong, your message wasn't sent.");
		} else if (status == 2) {
			Sys.Screen.ShowRespond ("User doesn't exist. Check the user name you are sending mail.");
		} else if (status == 3) {
			Sys.Screen.ShowRespond ("Draft have been saved.");
		} else if (status == 4) {
			Sys.Screen.ShowRespond ("Login have been successful.");
		} else if (status == 5) {
			Sys.Screen.ShowRespond ("Multiple connections from same ip is not allowed.");
		} else if (status == 6) {
			Sys.Screen.ShowRespond ("Your message have been deleted.");
		} else if (status == 7) {
			Sys.Screen.ShowRespond ("Error. Contact Administrator!.");
		}
	}

	//For testing purpose.. Online users list.
	[RPC]
	public  void Users (string user, int oper)
	{
		if (oper == 0) {
			Sys.Screen.Users.Add (user);	
		} else {
			Sys.Screen.Users.Remove (user);
		}
	}
}

