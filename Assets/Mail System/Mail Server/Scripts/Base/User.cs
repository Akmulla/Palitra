using UnityEngine;
using System.Collections;

public class User {
	private int id;
	private string login, ip;
	private NetworkPlayer netPlayer;
	public User(int ID, string Login, string Ip, NetworkPlayer NetPlayer){
		id = ID;
		login = Login;
		ip = Ip;
		netPlayer = NetPlayer;
	}
	
	public int ID{
		get{ return id; }
		set{ id = value; }
	}
	public string Login{
		get{ return login; }
		set{ login = value; }
	}
	public string IP{
		get{ return ip; }
		set{ ip = value; }
	}
	public NetworkPlayer Player{
		get{ return netPlayer; }
		set{ netPlayer = value; }
	}
}
