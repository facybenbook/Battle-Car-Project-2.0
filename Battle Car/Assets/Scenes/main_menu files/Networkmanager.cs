using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Networkmanager : NetworkManager {

	public void StartupHost()
	{
		SetPort ();
		NetworkManager.singleton.StartHost ();
	}

	public void JoinGame()
	{
		SetIPAdress ();
		SetPort ();
		NetworkManager.singleton.StartClient ();
	}

	void SetIPAdress()
	{
        string ipAdress;
        try
        {
            ipAdress = GameObject.Find("InputFieldIPAdress").transform.Find("Text").GetComponent<Text>().text;
            Console.WriteLine(ipAdress);

            if (ipAdress == null || ipAdress == "")
                ipAdress = "localhost";
            NetworkManager.singleton.networkAddress = ipAdress;
        }

        catch (Exception e)
        {
            NetworkManager.singleton.networkAddress = "localhost";
            Console.WriteLine("failed to input ip");
        }
		
		
	}

	void SetPort()
	{
		string port;
		int acc;
		try
		{
			port = GameObject.Find("InputFieldPort").transform.Find("Text").GetComponent<Text>().text;
			Console.WriteLine(port);
			acc= int.Parse(port);
			NetworkManager.singleton.networkPort = acc;
		}

		catch (Exception e)
		{
			NetworkManager.singleton.networkPort = 7777;
			Console.WriteLine("failed to input port");
		}
	}

	void OnLevelWasLoaded(int level)
	{
		if (level == 0) {
			SetupMenuSceneButtons ();
		} else {
			SetupOtherSceneButtons ();
		}
	}

	void SetupMenuSceneButtons ()
	{
		GameObject.Find ("ButtonStartHost").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonStartHost").GetComponent<Button> ().onClick.AddListener (StartupHost);

		GameObject.Find ("ButtonJoinGame").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonJoinGame").GetComponent<Button> ().onClick.AddListener (JoinGame);
	}

	void SetupOtherSceneButtons()
	{
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find ("ButtonDisconnect").GetComponent<Button> ().onClick.AddListener (NetworkManager.singleton.StopHost);
	}



}
