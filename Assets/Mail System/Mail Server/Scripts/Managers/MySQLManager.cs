using UnityEngine;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;

public class MySQLManager : MonoBehaviour
{
	private string constr = "Server=127.0.0.1;Database=database;User ID=root;Password=root;Pooling=true";
	// connection object
	MySqlConnection con = null;
	// command object
	MySqlCommand cmd = null;
	// reader object
	MySqlDataReader rdr = null;
	
	// Use this for initialization
	void Awake ()
	{
		try {
			// setup the connection element
			con = new MySqlConnection (constr);
			// lets see if we can open the connection
			con.Open ();
			Debug.Log ("Connection State: " + con.State);
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
		}
		
	}
	
	/// <summary>
	/// Register the specified Login and info.
	/// </summary>
	/// <param name='Login'>
	/// If set to <c>true</c> login.
	/// <param name='info'>
	/// If set to <c>true</c> info.
	/// </param>
	public bool Register (string Login, NetworkMessageInfo info)
	{
		Debug.Log ("*** MySQL - Register Called ***");
		
		//Check if vip code is correct
		//VipCodeCheck(string code);
		
		string query = string.Empty;
		// Error trapping in the simplest form
		try {
			query = "INSERT INTO users (login, ip) VALUES ( ?Login, ?IP)";
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				
				using (cmd = new MySqlCommand (query, con)) {
					
					MySqlParameter login = cmd.Parameters.Add ("?Login", MySqlDbType.VarChar);
					login.Value = Login;
					MySqlParameter IP = cmd.Parameters.Add ("?IP", MySqlDbType.VarChar);
					IP.Value = info.sender.ipAddress;
				
					cmd.ExecuteNonQuery ();
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
			return false;
		} finally {
		}
		return true;
	}
	
	/// <summary>
	/// Login using specified Name.
	/// </summary>
	/// <param name='Name'>
	/// If set to <c>true</c> name.
	/// </param>
	public bool Login (string Name)
	{
		Debug.Log ("*** MySQL - Login Called ***");
		string query = string.Empty;
		
		try {
			query = "SELECT Login FROM users";
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				using (cmd = new MySqlCommand (query, con)) {
					rdr = cmd.ExecuteReader ();
					if (rdr.HasRows)
						while (rdr.Read ()) {
							string _name = "";
							
							_name = rdr ["Login"].ToString ();
							
							if (_name == Name) {
								return true;
							}
						}
					rdr.Dispose ();
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
		} finally {
			
		}
		return false;
	}

	/// <summary>
	/// Deletes the mail from specified box.
	/// </summary>
	/// <returns>
	/// The mail.
	/// </returns>
	/// <param name='box'>
	/// If set to <c>true</c> box.
	/// </param>
	/// <param name='xID'>
	/// If set to <c>true</c> x I.
	/// </param>
	public bool DeleteMail (string box, int xID)
	{
		Debug.Log ("*** MySQL - Delete Mail Called ***");
		string query = string.Empty;
		//int ID = getID (Login);
		try {
			query = "DELETE FROM " + box + " WHERE id=?mailID ";
			
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				using (cmd = new MySqlCommand (query, con)) {
					
					MySqlParameter oParam = cmd.Parameters.Add ("?mailID", MySqlDbType.UInt16);
					oParam.Value = xID;
				
					cmd.ExecuteNonQuery ();
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
			return false;
		} finally {
			
		}
		return false;
	}
	
	public int GetLastID (string table)
	{
		Debug.Log ("*** MySQL - Get Record Count Called ***");
		string query = string.Empty;
		
		try {
			query = "SELECT max(id) FROM " + table ;
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				using (cmd = new MySqlCommand (query, con)) {
					rdr = cmd.ExecuteReader ();
					if (rdr.HasRows)
						while (rdr.Read ()) {
							string cnt = "";
							
							cnt = rdr ["max(id)"].ToString();
							
							return int.Parse(cnt) ;
						}
					rdr.Dispose ();
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
		} finally {
			
		}
		return -1;
	}
	
	public String GetUsers ()
	{
		Debug.Log ("*** MySQL - Get Record Count Called ***");
		string query = string.Empty;
		string cnt = "";
		try {
			query = "SELECT login FROM users"  ;
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				using (cmd = new MySqlCommand (query, con)) {
					rdr = cmd.ExecuteReader ();
					if (rdr.HasRows)
						while (rdr.Read ()) {
							
							
							cnt += "" + rdr ["login"].ToString() +"~" ;
							
							
						}
					
					rdr.Dispose ();
					return cnt ;
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
		} finally {
			
		}
		return "";
	}
	
	public bool UpdateMail (int mID)
	{
		Debug.Log ("*** MySQL - Update Mail Called ***");
		string query = string.Empty;
		try {
			query = "UPDATE inbox SET checked='1' WHERE id=?mailID ";
			
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				using (cmd = new MySqlCommand (query, con)) {
					
					MySqlParameter oParam = cmd.Parameters.Add ("?mailID", MySqlDbType.UInt16);
					oParam.Value = mID;
				
					cmd.ExecuteNonQuery ();
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
			return false;
		} finally {
			
		}
		return false;
	}

	public bool AddMail (int _id, int senderID, int receiverID, string subject, string message)
	{
		Debug.Log ("*** MySQL - AddMail Called ***");
		Debug.Log(senderID + " " + receiverID);
		string query = string.Empty;
		// Error trapping in the simplest form
		try {
			query = "INSERT INTO inbox (id, sender, receiver, subject, message) VALUES ( ?id, ?sender, ?receiver, ?subject, ?message)";
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				
				using (cmd = new MySqlCommand (query, con)) {
					MySqlParameter id = cmd.Parameters.Add ("?id", MySqlDbType.Int32);
					id.Value = _id; 
					MySqlParameter sender = cmd.Parameters.Add ("?sender", MySqlDbType.Int32);
					sender.Value = senderID;
					MySqlParameter receiver = cmd.Parameters.Add ("?receiver", MySqlDbType.Int32);
					receiver.Value = receiverID;
					MySqlParameter subj = cmd.Parameters.Add ("?subject", MySqlDbType.VarChar);
					subj.Value = subject;
					MySqlParameter msg = cmd.Parameters.Add ("?message", MySqlDbType.VarChar);
					msg.Value = message;
					
					cmd.ExecuteNonQuery ();
					return true;
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
			
		} finally {
		}
		return false;
	}
	
	public bool AddSent (int idx, int senderID, int receiverID, string subject, string message)
	{
		Debug.Log ("*** MySQL - AddSent Called ***");
		Debug.Log(senderID + " " + receiverID);
		string query = string.Empty;
		// Error trapping in the simplest form
		try {
			query = "INSERT INTO sent (id, sender, receiver, subject, message) VALUES (?id, ?sender, ?receiver, ?subject, ?message)";
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				
				using (cmd = new MySqlCommand (query, con)) {
					MySqlParameter id = cmd.Parameters.Add ("?id", MySqlDbType.Int32);
					id.Value = idx;
					MySqlParameter sender = cmd.Parameters.Add ("?sender", MySqlDbType.Int32);
					sender.Value = senderID;
					MySqlParameter receiver = cmd.Parameters.Add ("?receiver", MySqlDbType.Int32);
					receiver.Value = receiverID;
					MySqlParameter subj = cmd.Parameters.Add ("?subject", MySqlDbType.VarChar);
					subj.Value = subject;
					MySqlParameter msg = cmd.Parameters.Add ("?message", MySqlDbType.VarChar);
					msg.Value = message; 
					cmd.ExecuteNonQuery ();
					return true;
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
			
		} finally {
		}
		return false;
	}
	
	public bool AddDraft (int _owner,  string subject, string message)
	{
		Debug.Log ("*** MySQL - AddDraft Called ***");
		string query = string.Empty;
		// Error trapping in the simplest form
		try {
			query = "INSERT INTO draft (owner, subject, message) VALUES (?owner, ?subject, ?message)";
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				
				using (cmd = new MySqlCommand (query, con)) {
					
					MySqlParameter owner = cmd.Parameters.Add ("?owner", MySqlDbType.VarChar);
					owner.Value = _owner;
					MySqlParameter subj = cmd.Parameters.Add ("?subject", MySqlDbType.VarChar);
					subj.Value = subject;
					MySqlParameter msg = cmd.Parameters.Add ("?message", MySqlDbType.VarChar);
					msg.Value = message; 
					cmd.ExecuteNonQuery ();
					return true;
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
			
		} finally {
		}
		return false;
	}
	
	public int getID (string Login)
	{
		Debug.Log ("*** MySQL - Get UserID Called ***");
		string query = string.Empty;
		
		try {
			query = "SELECT login, id FROM Users WHERE login=?login";
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				using (cmd = new MySqlCommand (query, con)) {
					MySqlParameter oParam1 = cmd.Parameters.Add ("?login", MySqlDbType.VarChar);
					oParam1.Value = Login;
					rdr = cmd.ExecuteReader ();
					if (rdr.HasRows) {
						
						while (rdr.Read ()) {

							return int.Parse (rdr ["id"].ToString ());
							
						}
						rdr.Dispose ();
					}
				}			
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
		} finally {
			
		}
		return -1;
	}

	public List<string> GetSent (int id)
	{
		Debug.Log ("*** MySQL - Get Sent Called ***");
		List<string> _data = new List<string> ();
		string query = string.Empty;
		

		try {
			
			query = "SELECT sent.*, users.login FROM sent, users WHERE sent.sender = ?sender && users.id=  sent.receiver ";
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				using (cmd = new MySqlCommand (query, con)) {
					MySqlParameter oParam1 = cmd.Parameters.Add ("?sender", MySqlDbType.Int32);
					oParam1.Value = id;
					rdr = cmd.ExecuteReader ();
					if (rdr.HasRows) {
						
						while (rdr.Read ()) {
								_data.Add( rdr ["id"].ToString () + "~" + rdr ["login"].ToString () + "~" + rdr ["subject"].ToString () + "~" + rdr ["message"].ToString () + "~" + rdr ["delivery_date"].ToString () );	
						}

						rdr.Dispose ();
					}
				}			
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
		} finally {
			
		}
		return _data;
	}
	
	public List<string> GetDraft (int id)
	{
		Debug.Log ("*** MySQL - Get Draft Called ***");
		List<string> _data = new List<string> ();
		string query = string.Empty;
		
		
		try {
			query = "SELECT * FROM draft WHERE owner=?owner";
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				using (cmd = new MySqlCommand (query, con)) {
					MySqlParameter oParam1 = cmd.Parameters.Add ("?owner", MySqlDbType.Int32);
					oParam1.Value = id;
					rdr = cmd.ExecuteReader ();
					if (rdr.HasRows) {
						
						while (rdr.Read ()) {
								_data.Add( rdr ["id"].ToString () +  "~" + rdr ["subject"].ToString () + "~" + rdr ["message"].ToString () + "~" + rdr ["saved"].ToString () + "~");	
						}
						rdr.Dispose ();
					}
				}			
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
		} finally {
			
		}
		return _data;
	}
	
	public List<string> GetMail (int id)
	{
		Debug.Log ("*** MySQL - Get Mail Called ***");
		List<string> _data = new List<string> ();
		string query = string.Empty;
		
		//Debug.Log ("*** His ID in get mail *** " + id);
		try {
			query = "SELECT inbox.*, users.login FROM inbox, users WHERE inbox.receiver = ?receiver  && users.id=  inbox.sender";
			if (con.State.ToString () != "Open")
				con.Open ();
			using (con) {
				using (cmd = new MySqlCommand (query, con)) {
					MySqlParameter oParam1 = cmd.Parameters.Add ("?receiver", MySqlDbType.Int32);
					oParam1.Value = id;
					rdr = cmd.ExecuteReader ();
					if (rdr.HasRows) {
						
						while (rdr.Read ()) {
								_data.Add( rdr ["id"].ToString () +  "~" + rdr ["login"].ToString () + "~" + rdr ["subject"].ToString () + "~" + rdr ["message"].ToString () + "~" + rdr ["delivery_date"].ToString () + "~" + rdr ["checked"].ToString ()  + "~");	
						
							
						}
						rdr.Dispose ();
					}
				}			
			}
		} catch (Exception ex) {
			Debug.Log (ex.ToString ());
		} finally {
			
		}
		return _data;
	}
	
	void OnApplicationQuit ()
	{
		Debug.Log ("killing con");
		if (con != null) {
			if (con.State.ToString () != "Closed")
				con.Close ();
			con.Dispose ();
		}
	}
}
