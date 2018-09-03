using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MailManager : MonoBehaviour
{
	public enum MAILSTATE
	{
		INBOX,
		WRITE,
		READ,
		SENT,
		DRAFT
	}
	
	private enum MAILTYPE
	{
		DRAFT,
		SENT,
		INBOX
	}

	public GUISkin skin;
	private List<Message> mailBox = new List<Message> ();
	private List<Message> sent = new List<Message> ();
	private List<Message> draft = new List<Message> ();
	
	//  VARIABLES
	//******************************************************************

	private float scrW;
	private float scrH;
	//Identifier for selected mail.
	private int _selectedMail = 0;
	private int MAIL_WINDOW_ID = 1;
	public Vector2 messageLabelSize = new Vector2(583,40); 
	//Rectangle on the screen to use for the ScrollView. 
	public Rect scrollView = new Rect (0, 80, 600, 280);
	//The rectangle used inside the ScrollView. 
	public Rect scrollViewInside = new Rect(0, 0, 580, 280);
	//  BOOLEANS
	//******************************************************************
	private bool showMailWin = false;
	//  RECTANGLES
	//******************************************************************
	private Rect _mailRect = new Rect (30, 80, 600, 400);
	private bool dragable = true;
	//  STRINGS
	//******************************************************************
	private string uName = "";
	private string mReceiver = "";
	private string mSubject = "";
	private string mMessage = "";
	
	//  VECTORS
	//******************************************************************
	private Vector2 MailViewVector = Vector2.zero;

	//  READ LAYOUT 
	//******************************************************************
	public Rect rBackButton = new Rect (490, 350, 100, 40);
	public Rect rReplayButton = new Rect (490, 310, 100, 40);
	public Rect rResendButton = new Rect (490, 125, 100, 40);
	public Rect rDeleteButton = new Rect (490, 85, 100, 40);
	public Rect rFromLabel = new Rect (10, 40, 80, 40);
	public Rect rSubjectLabel = new Rect (10, 80, 80, 40);
	public Rect rSenderLabel = new Rect (90, 45, 300, 30);
	public Rect rContent = new Rect (0, 130, 480, 250);
	public Rect rSubject = new Rect (90, 85, 300, 30);

	//  WRITE LAYOUT
	//******************************************************************
	public Rect wBackButton = new Rect (460, 90, 110, 40);
	public Rect wReplayButton = new Rect (490, 310, 100, 40);
	public Rect wDraftButton = new Rect (460, 10, 110, 40);
	public Rect wSendButton = new Rect (450, 50, 130, 40);
	public Rect wToLabel = new Rect (10, 40, 80, 40);
	public Rect wSubjectLabel = new Rect (10, 80, 80, 40);
	public Rect wReceiverLabel = new Rect (90, 45, 300, 30);
	public Rect wContent = new Rect (0, 130, 600, 250);
	public Rect wSubject = new Rect (90, 87.5f, 300, 25);

	//  DRAFT LAYOUT
	//******************************************************************
	public Rect dBackButton = new Rect (480, 20, 110, 40);
	public Rect dNewDraftButton = new Rect (350, 30, 130, 50);

	//  SENT LAYOUT
	//******************************************************************
	public Rect sBackButton = new Rect (480, 20, 110, 40);
	public Rect sClearButton = new Rect (350, 30, 130, 50);
	
	//  SETTERS AND GETTERS
	//******************************************************************
	public string UName {
		get{ return uName; }
		set{ uName = value;}
	}

	public string Receiver {
		get{ return mReceiver; }
		set{ mReceiver = value;}
	}
	
	public string Subject {
		get{ return mSubject; }
		set{ mSubject = value;}
	}
	
	public string Message {
		get{ return mMessage; }
		set{ mMessage = value;}
	}
	public Rect MRect {
		get{ return _mailRect; }
		set{ _mailRect = value;}
	}

	public bool Enable {
		get{ return showMailWin; }
		set{ showMailWin = value;}
	}

	public List<Message> MailBox {
		get{ return mailBox;}
		set{ mailBox = value;}		
	}

	public List<Message> Draft {
		get{ return draft;}
		set{ draft = value;}			
	}

	public List<Message> Sent {
		get{ return sent;}
		set{ sent = value;}			
	}
	//******************************************************************
	
	private MAILSTATE mailState = MAILSTATE.INBOX;
	private MAILTYPE mailType = MAILTYPE.INBOX;

	void Update ()
	{
	}

	void OnGUI ()
	{
		//Assign the gui skin.
		GUI.skin = skin;
		//Show mail window if true.
		if (showMailWin) {
			_mailRect = GUI.Window (MAIL_WINDOW_ID, _mailRect, Mail, "");
		}
	}

	private void DisplayInbox ()
	{
		scrollViewInside.height = messageLabelSize.y * mailBox.Count;
		//Create scrollable are to view messages.
		MailViewVector = GUI.BeginScrollView (scrollView, MailViewVector, scrollViewInside);
		for (int cnt = 0; cnt < mailBox.Count; cnt++) {
			if (mailBox [cnt].Checked == true) {
				if (GUI.Button (new Rect (0, 0 + (cnt * messageLabelSize.y), messageLabelSize.x, messageLabelSize.y), mailBox [cnt].Subject + " | " + mailBox [cnt].Sender + " : " + mailBox [cnt].DeliveryTime, "Mail")) {
					mailState = MAILSTATE.READ;
					mailType = MAILTYPE.INBOX;
					_selectedMail = cnt;
					mReceiver = mailBox [cnt].Sender;
				}
			} else {
				if (GUI.Button (new Rect (0, 0 + (cnt * messageLabelSize.y), messageLabelSize.x, messageLabelSize.y), "<New> " + mailBox [cnt].Subject + " | " + mailBox [cnt].Sender + " : " + mailBox [cnt].DeliveryTime, "Mail")) {
					mailState = MAILSTATE.READ;
					_selectedMail = cnt;
					mailType = MAILTYPE.INBOX;
					mReceiver = mailBox [cnt].Sender;
					GetComponent<NetworkView>().RPC ("OnReadMail", RPCMode.Server, mailBox [cnt].ID);
					mailBox [cnt].Checked = true;
				}
			}
		}
		GUI.EndScrollView ();
	}

	private void DisplayDraft ()
	{
		scrollViewInside.height = messageLabelSize.y * draft.Count;
		//Create scrollable are to view messages.
		MailViewVector = GUI.BeginScrollView (scrollView, MailViewVector, scrollViewInside);
		for (int cnt = 0; cnt < draft.Count; cnt++) {
			if (GUI.Button (new Rect (0, 0 + (cnt * messageLabelSize.y), messageLabelSize.x, messageLabelSize.y), draft [cnt].Subject + " | " + draft [cnt].Sender + " : " + draft [cnt].DeliveryTime, "Mail")) {
				mailState = MAILSTATE.READ;
				_selectedMail = cnt;
				mailType = MAILTYPE.DRAFT;					
			}
		}
		GUI.EndScrollView ();
	}

	private void DisplaySent ()
	{
		scrollViewInside.height = messageLabelSize.y * sent.Count;
		//Create scrollable are to view messages.
		MailViewVector = GUI.BeginScrollView (scrollView, MailViewVector, scrollViewInside);
		for (int cnt = 0; cnt < sent.Count; cnt++) {
			if (GUI.Button (new Rect (0, 0 + (cnt * messageLabelSize.y), messageLabelSize.x, messageLabelSize.y), sent [cnt].Subject + " | " + sent [cnt].Sender + " : " + sent [cnt].DeliveryTime, "Mail")) {
				mailState = MAILSTATE.READ;
				mailType = MAILTYPE.SENT;
				_selectedMail = cnt;
				mReceiver = sent [cnt].Sender;				
			}
		}
		GUI.EndScrollView ();
	}

	void Mail (int window)
	{
		//Window name and state. Demonstration purpose.
		GUI.Label (new Rect (10, 0, 70, 40), "Mail");
		GUI.Label (new Rect (80, 10, 70, 30), mailState.ToString ());
		
		if (mailState == MAILSTATE.INBOX) {
			#region Inbox Layout
			if (GUI.Button (new Rect (350, 30, 130, 50), "New Message")) {			
				mailState = MAILSTATE.WRITE;
				mReceiver = "";
			}
			if (GUI.Button (new Rect (240, 20, 110, 40), "Sent")) {			
				mailState = MAILSTATE.SENT;
			}
			if (GUI.Button (new Rect (480, 20, 110, 40), "Draft")) {
				mailState = MAILSTATE.DRAFT;				
			}
			//Background for scrollable area.
			GUI.Label (new Rect (0, 80, 600, 280), "", "Field");
			DisplayInbox ();
			#endregion
		} else if (mailState == MAILSTATE.READ) {
			#region Read Layout
			if (GUI.Button (rBackButton, "Back")) {
				//Return to previous window.
				Back ();
			}
			//Chech to show Replay button or not.
			if (mailType == MAILTYPE.INBOX) {
				if (GUI.Button (rReplayButton, "Replay")) {
					mailState = MAILSTATE.WRITE;
					mReceiver = mailBox [_selectedMail].Sender;
				}
			}
			//Chech to show Send button or not.
			if (mailType == MAILTYPE.DRAFT) {
				if (GUI.Button (rResendButton, "Send")) {	
					mailState = MAILSTATE.WRITE;
					mSubject = draft [_selectedMail].Subject;
					mMessage = draft [_selectedMail].Text;
				}	
			}
			if (GUI.Button (rDeleteButton, "Delete")) {
				//Return to previous window.
				Back ();
				//Detect from there message must be deleted and send request to server to delete it.
				if (mailType == MAILTYPE.INBOX) {	
					GetComponent<NetworkView>().RPC ("OnDeleteMail", RPCMode.Server, "inbox", mailBox [_selectedMail].ID);
					mailBox.RemoveAt (_selectedMail);
				} else if (mailType == MAILTYPE.DRAFT) {
					GetComponent<NetworkView>().RPC ("OnDeleteMail", RPCMode.Server, "draft", draft [_selectedMail].ID);
					draft.RemoveAt (_selectedMail);
				} else if (mailType == MAILTYPE.SENT) {
					GetComponent<NetworkView>().RPC ("OnDeleteMail", RPCMode.Server, "sent", sent [_selectedMail].ID);
					sent.RemoveAt (_selectedMail);			
				}						
			}
			//Display subject label. 
			GUI.Label (rSubjectLabel, "Subject: ");
			//Check in which state user is viewing messasge.
			if (mailType == MAILTYPE.INBOX) {
				GUI.Label (rFromLabel, "From: ");
				GUI.Label (rSenderLabel, mailBox [_selectedMail].Sender, "Field");
				GUI.Label (rSubject, mailBox [_selectedMail].Subject, "Field");
				GUI.TextArea (rContent, mailBox [_selectedMail].Text, "Content");
			} else if (mailType == MAILTYPE.DRAFT) {
				GUI.Label (rSubject, draft [_selectedMail].Subject, "Field");
				GUI.TextArea (rContent, draft [_selectedMail].Text, "Content");
			} else if (mailType == MAILTYPE.SENT) {
				GUI.Label (rFromLabel, "To: ");
				GUI.Label (rSenderLabel, sent [_selectedMail].Sender, "Field");
				GUI.Label (rSubject, sent [_selectedMail].Subject, "Field");
				GUI.TextArea (rContent, sent [_selectedMail].Text, "Content");
			}
		#endregion
		} else if (mailState == MAILSTATE.WRITE) {
			#region Write Layout
			//Check if it's needed to show 'To: ' field.
			if (mailType == MAILTYPE.INBOX || mailType == MAILTYPE.SENT) {
				GUI.Label (wToLabel, "To :");
				mReceiver = GUI.TextField (wReceiverLabel, mReceiver, 500);
			}
			//Display subject field.
			GUI.Label (wSubjectLabel, "Subject :");
			mSubject = GUI.TextField (wSubject, mSubject, 255);
			//Display message field.
			mMessage = GUI.TextField (wContent, mMessage, "Content");
			//Display draft button.
			if (GUI.Button (wDraftButton, "Draft")) {
				//Check if all fields are filled up. If not ask user to fill it up.
				if (mSubject == "" || mMessage == "") {
					Sys.Screen.ShowRespond ("All fields must be filled.");
					return;
				}
				//Send request to the server to save draft.
				GetComponent<NetworkView>().RPC ("OnSave", RPCMode.Server, mSubject, mMessage);
				//Reset subject and message fields.
				mSubject = "";
				mMessage = "";
				//Return to previous state.
				Back ();
			}
			if (GUI.Button (wSendButton, "Send")) {
				//Check if all fields are filled up. If not ask user to fill it up.
				if (mSubject == "" || mReceiver == "" || mMessage == "") {
					Sys.Screen.ShowRespond ("All fields must be filled.");
					return;
				}
				//Send request to the server to send message to specified user.
				Sys.Network.SendMail (mReceiver, mSubject, mMessage);
				//Reset subject, message and receiver fields.
				mSubject = "";
				mMessage = "";
				mReceiver = "";
				//Return to previous window.
				Back ();
			}
			if (GUI.Button (wBackButton, "Back")) {
				//Return to previous window.
				Back ();
			}
			#endregion
		} else if (mailState == MAILSTATE.DRAFT) {
			#region Draft Layout
			if (GUI.Button (dNewDraftButton, "New Draft")) {			
				mailState = MAILSTATE.WRITE;
				mailType = MAILTYPE.DRAFT;
			}
			if (GUI.Button (dBackButton, "Back")) {
				mailState = MAILSTATE.INBOX;	
				mailType = MAILTYPE.INBOX;
			}
			//Background for scrollable area.
			GUI.Label (new Rect (0, 80, 600, 280), "", "Field");
			DisplayDraft ();
			#endregion
		} else if (mailState == MAILSTATE.SENT) {
			#region Sent Layout
			if (GUI.Button (sClearButton, "Clear")) {			
				sent.Clear ();
			}
			if (GUI.Button (sBackButton, "Back")) {
				mailState = MAILSTATE.INBOX;	
				mailType = MAILTYPE.INBOX;
			}
			//Background for scrollable area.
			GUI.Label (new Rect (0, 80, 600, 280), "", "Field");
			DisplaySent ();
			#endregion
		}
		//To make dragable window
		Rect drag = new Rect (0, 0, _mailRect.width, 50);
		GUI.Label (drag, "", "Drag");
		if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && drag.Contains (Event.current.mousePosition)) {
			dragable = true;
		} else if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && !drag.Contains (Event.current.mousePosition))
			dragable = false;
		
		if (dragable)
			GUI.DragWindow ();	
		
	}
	
	/// <summary>
	/// Call to return to previous window.
	/// </summary>
	private void Back ()
	{
		//Check to which state to return.
		if (mailType == MAILTYPE.INBOX) {
			mailState = MAILSTATE.INBOX;
		} else if (mailType == MAILTYPE.DRAFT) {
			mailState = MAILSTATE.DRAFT;
		} else if (mailType == MAILTYPE.SENT) {
			mailState = MAILSTATE.SENT;
		}	
	}
	
	/// <summary>
	/// Call to connect to mail system.
	/// </summary>
	public void Con ()
	{
		GetComponent<NetworkView>().RPC ("OnConnect", RPCMode.Server, Sys.Mail.UName);
	}
	/// <summary>
	/// Request inbox.
	/// </summary>
	public void RInbox ()
	{
		//Clear box before getting new one from server
		mailBox.Clear();
		GetComponent<NetworkView>().RPC ("OnMailRequest", RPCMode.Server, uName);
	}
	
	/// <summary>
	/// Request sent.
	/// </summary>
	public void RSent ()
	{
		//Clear box before getting new one from server
		sent.Clear();
		GetComponent<NetworkView>().RPC ("OnSentRequest", RPCMode.Server, uName);
	}
	
	/// <summary>
	/// Request draft.
	/// </summary>
	public void RDraft ()
	{
		//Clear box before getting new one from server
		draft.Clear();
		GetComponent<NetworkView>().RPC ("OnDraftRequest", RPCMode.Server, uName);
	}
}
