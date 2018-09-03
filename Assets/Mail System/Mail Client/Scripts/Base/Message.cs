using UnityEngine;
using System.Collections;

public class Message {
	private int _id;
	private string _sender;
	private string _subject;
	private string _message;
	private string deliveryTime;
	private bool _checked;
	private string receiver;

	
	public Message() {
	
	}
	
	public Message(int ID, string sender, string subject, string message, string DeliveryTime, bool Checked ) {
		_id = ID;
		_sender = sender;
		_subject = subject;
		_message = message;
		deliveryTime = DeliveryTime;
		_checked = Checked;
		receiver = "";
	}
	public int ID {
		get{ return _id; }
		set{ _id = value; }
	}
	public string Sender {
		get{ return _sender; }
		set{ _sender = value; }
	}
	public string Subject {
		get{ return _subject; }
		set{ _subject = value; }
	}
	public string Text {
		get{ return _message; }
		set{ _message = value; }
	}
	public string DeliveryTime {
		get{ return deliveryTime; }
		set{ deliveryTime = value; }
	}
	public bool Checked {
		get{ return _checked; }
		set{ _checked = value; }
	}
	public string Receiver {
		get{ return receiver; }
		set{ receiver = value; }
	}
	
}
