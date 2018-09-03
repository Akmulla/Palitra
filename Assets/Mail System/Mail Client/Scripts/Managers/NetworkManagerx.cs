using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	public string IP = "127.0.0.1";
	public int Port = 25001;
	/// <summary>
	/// Sends the mail.
	/// </summary>
	/// <param name='Receiver'>
	/// Receiver.
	/// </param>
	/// <param name='Subject'>
	/// Subject.
	/// </param>
	/// <param name='Message'>
	/// Message.
	/// </param>
	public void SendMail (string Receiver, string Subject, string Message)
	{
		//Send message to user/s
		string[] receivers = Receiver.Split (',');
		for (int cnt= 0; cnt < receivers.Length; cnt++) {
			GetComponent<NetworkView>().RPC ("OnSendMail", RPCMode.Server, Sys.Mail.UName, receivers [cnt], Subject, Message);
		}
	}
	/// <summary>
	/// Connect to specified IP address.
	/// </summary>
	public void Connect ()
	{
		Network.Connect (IP, Port);	
	}
	/// <summary>
	/// Setup the specified ip and port.
	/// </summary>
	/// <param name='ip'>
	/// Ip.
	/// </param>
	/// <param name='port'>
	/// Port.
	/// </param>
	public void Setup (string ip, int port)
	{
		IP = ip;
		Port = port;
	}
	
	//Client functions called by Unity
	void OnConnectedToServer ()
	{
		Sys.Screen.ShowRespond ("Successful connect to server.");	
	}
	void OnFailedToConnect (NetworkConnectionError error)
	{  
		Sys.Screen.ShowRespond ("Could not connect to server: " + error);   
	}
	void OnDisconnectedFromServer (NetworkDisconnection info)
	{
		Sys.Screen.ShowRespond ("This CLIENT has disconnected from a server OR this SERVER was just shut down");
		Sys.Mail.Enable = false;
		Sys.Mail.MailBox.Clear();
		Sys.Mail.Draft.Clear();
		Sys.Mail.Sent.Clear();
	}

	
}
