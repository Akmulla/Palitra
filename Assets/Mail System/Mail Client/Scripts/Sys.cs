using UnityEngine;
[RequireComponent(typeof(ScreenManager))]
[RequireComponent(typeof(MailManager))]
[RequireComponent(typeof(NetworkManager))]

public class Sys: MonoBehaviour
{

	private static ScreenManager screenManager;
    public static ScreenManager Screen
    {
        get { return screenManager; }
    }
	private static MailManager mailManager;
    public static MailManager Mail
    {
        get { return mailManager; }
	}
	
	private static NetworkManager  networkManager;
    public static NetworkManager Network
    {
        get { return networkManager; }
    }
	
    // Use this for initialization
   void Awake(){
        //Find the references
        screenManager = GetComponent<ScreenManager>();
        mailManager = GetComponent<MailManager>();
		networkManager = GetComponent<NetworkManager>();		
       

		
	}
       
    
}