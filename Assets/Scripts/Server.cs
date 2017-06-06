using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
//using Random = UnityEngine.Random;

//public enum RoomsID
//{
//    Debref = 9999,
//    Bref = 9998,
//    Between = 9997,
//    Game = 9996
//}
//public class Team
//{
//    public string TeamName;
//    public int RoomNumber;
//    public int TeamId;
//    public int ScoreRoom;
//    public float RoomTime;
//    public int ScoreAll;
//}

//public class Room
//{
//    public int RoomTime;

//}
public enum GameStages
{
    DontExist = -1,
    Brief = 0,
    Debref = 1,
    Walking = 2,
    Playing = 3
}
public class Server : MonoBehaviour
{


    public static Server Instance;
    public Text RoomTimer;
    public InputField TeamNameField;
    public InputField TimeToStart;
    public Button CreateTeamBtn;
    public Text Scanned;

    public string TeamName = "NoName";

    private float _localTimer;
    private const string LINKBASE = "http://192.168.0.151:8000";
    private bool _teamCreated = false;
    private GameStages _currentStage = GameStages.DontExist;
    private string _message = "Scan the tablet, game will start in";
    private bool _scanned=false;
    private bool _block=false;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void IncScore()
    {
        //.text = 
        //(Int32.Parse(Score.text) + 1).ToString();
    }
    void Start()
    {
        AndroidNFCReader.enableBackgroundScan();
        AndroidNFCReader.ScanNFC(gameObject.name, "OnFinishScan");

        // Score.text = "0";
    }

    private int ID = 0;
    public void CreateTeam()
    {

        //t.teamId = PlayerPrefs.GetInt("teamId", 0); ;
        //if (t.teamId == 0)
        //{
        //    t.teamId = Random.Range(0, 333);
        //}
        //PlayerPrefs.SetInt("teamId", t.teamId);
        string url = LINKBASE + "/TeamCreation/0";
        Debug.Log("SADASDAPSKKDPS TEAM  CREATIN");
        WWWForm form = new WWWForm();
        form.AddField("name", TeamNameField.text);
        form.AddField("startTime", TimeToStart.text);

        byte[] rawData = form.data;
        var headers = new Dictionary<string, string>();
        WWW www = new WWW(url, rawData, headers);

        StartCoroutine(RequestTeamCreation(www));
    }

    public void EnteringRoom()

    {
       
        print(_currentStage);
        SceneManager.LoadScene("AR");
    }
    public void EnteredRoom()
    {
        GetGameStage();
        ID = PlayerPrefs.GetInt("teamID", -1);
        string url = LINKBASE + "/Enter/" + ID;

        WWWForm form = new WWWForm();
        form.AddField("", "bla");
        byte[] rawData = form.data;
        var headers = new Dictionary<string, string>();
        WWW www = new WWW(url, rawData, headers);

        StartCoroutine(RoomEnterRequest(www));



    }
    public void GetGameStage()
    {
         string url = LINKBASE + "/GameStage/" + ID;
        WWW www = new WWW(url);
        StartCoroutine(GetGameStageRequest(www));
    }
    public void LeaveRoom()
    {
        string url = LINKBASE + "/Finish/" + ID;

        WWWForm form = new WWWForm();
        form.AddField("score", 0);
        byte[] rawData = form.data;

        // headers.Add("X-Auth-Token", "7da4596d42e24f9798d73ec40bbbbd81");

        var headers = new Dictionary<string, string>();
        WWW www = new WWW(url, rawData, headers);


        StartCoroutine(RoomEndRequest(www));


    }
    IEnumerator GetTimeRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            print("timer request   "+www.data);
            float t = 0;
            float.TryParse(www.data, NumberStyles.Float, null, out t);
            int time = ((int)t);
            RoomTimer.text = (Math.Floor(time / 60.0f).ToString() + ":" + time % 60);
            //Debug.Log(time);
            if (time < 0.5f)
            {
                if (_scanned)
                {

                    _currentStage = GameStages.Playing;
                    EnteringRoom();
                }

            
               else
               {
                   _block=true;
               }
            }

            // Debug.Log(r.RoomTime);
            // t.RoomTime = r.RoomTime;
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }

    }
  IEnumerator GetGameStageRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {

            int t = 0;
            int.TryParse(www.data, NumberStyles.Float, null, out t);
            
            _currentStage=(GameStages)t;

            // Debug.Log(r.RoomTime);
            // t.RoomTime = r.RoomTime;
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }

    }
    // NFC callback
    void OnFinishScan(string result)
    {

        // Cancelled
        if (result == AndroidNFCReader.CANCELLED)
        {

            // Error
        }
        else if (result == AndroidNFCReader.ERROR)
        {


            // No hardware
        }
        else if (result == AndroidNFCReader.NO_HARDWARE)
        {
        }
        //EnteredRoom();
        if (Scanned)
        {
         // Scanned.text = ("game will start in"+result);
        if (result=="poop")
        {
            _scanned=true;   
            Scanned.text = "game will start in";
            _block=false;
        }
        }   
        //qrString =  (result);
    }
    void Update()
    {

        if (!_block &&(_teamCreated || _currentStage==GameStages.Playing))
        {
            _localTimer += Time.deltaTime;
            if (_localTimer >= 0.5f)
            {
                _localTimer = 0;
                string url = LINKBASE + "/Time/" + ID;

                WWW www = new WWW(url);

                StartCoroutine(GetTimeRequest(www));

            }

        }
    }
    IEnumerator RoomEnterRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW ENTERED!: " + www.data);



        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }


    IEnumerator RoomEndRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW END!: " + www.data);

            //Debug.Log(t.TeamId+"----"+t.RoomNumber.ToString());

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    IEnumerator RequestTeamCreation(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW TEAMCREATIOn: " + www.data);
            TeamNameField.gameObject.SetActive(false);
            TimeToStart.gameObject.SetActive(false);
            CreateTeamBtn.gameObject.SetActive(false);
            RoomTimer.gameObject.SetActive(true);
            Scanned.gameObject.SetActive(true);
            
            ID = int.Parse(www.data);
            PlayerPrefs.SetInt("teamID", ID);

            Debug.Log(ID);
            //Debug.Log(t.TeamId+"----"+t.RoomNumber.ToString());
            // EnterRoom();
            _teamCreated = true;
            _currentStage = GameStages.Brief;
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

}
