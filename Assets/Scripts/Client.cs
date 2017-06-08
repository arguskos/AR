using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using MyMessage1;
using UnityEngine.SceneManagement;

[System.Serializable]

public class ServerMessage
{
	public ServerMessage(int time, MyMessage1.HiveStates state)
	{
		Time = time;
		State = state;
	}

	public int Time;
	public HiveStates State;
}
public class Client : MonoBehaviour
{



	public InputField TeamId;




	private int ID = -1;
	// Use this for initialization
	public Text RoomTimer;

	internal bool SocketReady = false;
	private TcpClient _mySocket;
	private NetworkStream _myStream;
	private StreamWriter _myWriter;
	private StreamReader _myReader;
	private const string Host = "127.0.0.1";
	private const int Port = 8000;
	private bool _created = false;
	public void SetupSocket()
	{


		_mySocket = new TcpClient(Host, Port);
        print("connected    ");
		_myStream = _mySocket.GetStream();
		_myWriter = new StreamWriter(_myStream);
		_myReader = new StreamReader(_myStream);
		SocketReady = true;

	}
	public void WriteSocket(string theLine)
	{
		if (!SocketReady)
			return;
		string foo = theLine + "\r\n";
		_myWriter.Write(foo);
		_myWriter.Flush();
	}
	public string ReadSocket()
	{
		if (!SocketReady)
			return "";
		if (_myStream.DataAvailable)
			return _myReader.ReadLine();
		return "";
	}
	public void SendScore()
	{
		WriteSocket("recieve");
		SendScore lol = new SendScore(0, 100);
		new BinaryFormatter().Serialize(_myStream, lol);
	}
	public void CreateTeam()
	{
		WriteSocket("recieve");
		try
		{
			int.TryParse(TeamId.text, out ID);

		}
		catch (Exception e)
		{
			ID = 1;
			print("ERROR PARSING ID ");
		}
		TeamCreation lol = new TeamCreation(0, ID);
		new BinaryFormatter().Serialize(_myStream, lol);
		_created = true;
	}

	public void GetServerMessage()
	{

		WriteSocket("Info");
		var d = new BinaryFormatter().Deserialize(_myStream) as BaseMessage;
		if ((d as ServerTime) != null)
		{

			RoomTimer.text=(d as ServerTime).Time.ToString();
		    if ((d as ServerTime).Time < 0.7f)
		    {
                CloseSocket();
		   //SceneManager.LoadScene("AR");
            }

        }
		else if ((d as HiveStateChanged) != null)
		{
           
			//SceneManager.LoadScene("AR");
		}
		//print(d.Time);
	}
	public void CloseSocket()
	{
		if (!SocketReady)
			return;
		_myWriter.Close();
		_myReader.Close();
		_mySocket.Close();
		SocketReady = false;
	}


	void Start()
	{
		//    _client = new TcpClient("192.168.0.151", 8000);
		SetupSocket();
		StartCoroutine(CheckTime());

	}
	IEnumerator CheckTime()
	{
		while (true)
		{
			if (_created && SocketReady)
			{

				GetServerMessage();

			}
			yield return new WaitForSeconds(0.5f);


		}
	}
	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))


		{
			SendScore();
		}
        	}
    void OnApplicationQuit()
    {
        CloseSocket();
    }
} 

