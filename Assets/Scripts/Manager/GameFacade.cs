using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Common;
public class GameFacade : MonoBehaviour
{
    //[HideInInspector]
    public List<MovableObject> movableObjects;

    static GameFacade _instance;
    public static GameFacade Instance;
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = GameObject.Find("GameFacade").GetComponent<GameFacade>();
    //         }
    //         return _instance;
    //     }
    // }
    public List<SavePoint> savePoints;
    public SavePoint lastSave;

    PlayerManager playerMng;
    RequestManager requestMng;
    ClientManager clientMng;

    public bool StartSyncSymble = false;

    public bool IsStartGame;
    public bool IsConnected;

    public int tempname;


    void Awake()
    {
        Instance = this;
        clientMng = new ClientManager(this);
        requestMng = new RequestManager(this);
        playerMng = new PlayerManager(this);//要在最后
        DontDestroyOnLoad(this.gameObject);

        //Screen.SetResolution(640, 480, false);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        foreach (MovableObject m in movableObjects)
        {
            m.StartCoroutine(m.Move());
        }
    }

    void Update()
    {
        if (StartSyncSymble)
        {
            StartPlaying();
            StartSyncSymble = false;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            NextLevel();
        }
    }
    public void NextLevel()
    {
        StartCoroutine(Next());
    }


    // void OnDestroy()
    // {
    //     DestroyManager();
    // }

    // void InitManager()
    // {
    //     clientMng.OnInit();
    //     playerMng.OnInit();
    // }
    // void DestroyManager()
    // {
    //     playerMng.OnDestroy();
    //     clientMng.OnDestroy();
    // }


    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMng.AddRequest(actionCode, request);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestMng.RemoveRequest(actionCode);
    }
    public void HandleReponse(ActionCode actionCode, string data)
    {
        requestMng.HandleReponse(actionCode, data);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        Debug.Log(data);
        clientMng.SendRequest(requestCode, actionCode, data);
    }

    public GameObject GetCurrentRoleGameObject()
    {
        return playerMng.GetCurrentRoleGameObject();
    }
    public void EnterPlayingSync()
    {
        StartSyncSymble = true;
        IsStartGame = true;
    }

    public void StartPlaying()
    {
        playerMng.GetLocalPlayer();
        playerMng.AddRequestScript();
        foreach (MovableObject m in movableObjects)
        {
            m.StartCoroutine(m.Move());
        }
    }

    public void GameOver()
    {
        // GameObject currentRoleGameObject;
        // GameObject playerSyncRequest;
        // GameObject remoteRoleGameObject;

        // ShootRequest shootRequest;
        // AttackRequest attackRequest;

        // GameObject.Destroy(currentRoleGameObject);
        // GameObject.Destroy(playerSyncRequest);
        // GameObject.Destroy(remoteRoleGameObject);
        StartCoroutine(Over());
    }


    IEnumerator Next()
    {
        Image blackground = GetComponentInChildren<Image>();
        float timer = 0f;
        Color color = blackground.color;
        while (timer < 1.5f)
        {
            color.a = Mathf.Lerp(0f, 1f, timer / 1.5f);
            blackground.color = color;
            timer += Time.fixedDeltaTime;
            yield return 0;
        }
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1);
        timer = 0;
        while (timer < 1.5f)
        {
            color.a = Mathf.Lerp(1f, 0f, timer / 1.5f);
            blackground.color = color;
            timer += Time.fixedDeltaTime;
            yield return 0;
        }
    }

    IEnumerator Over()
    {
        Image blackground = GetComponentInChildren<Image>();
        float timer = 0f;
        Color color = blackground.color;
        while (timer < 1.5f)
        {
            color.a = Mathf.Lerp(0f, 1f, timer / 1.5f);
            blackground.color = color;
            timer += Time.fixedDeltaTime;
            yield return 0;
        }
        ResetPlayers();
        yield return new WaitForSeconds(1);
        timer = 0;
        while (timer < 1.5f)
        {
            color.a = Mathf.Lerp(1f, 0f, timer / 1.5f);
            blackground.color = color;
            timer += Time.fixedDeltaTime;
            yield return 0;
        }
    }



    public void ResetPlayers()
    {
        //     Vector3 v = Vector3.zero;
        //     p1.gameObject.SetActive(false);
        //     p2.gameObject.SetActive(false);

        //     p1.transform.position = Vector3.SmoothDamp(p1.transform.position, lastSave.transform.position + lastSave.t1, ref v, 0.6f);
        //     p2.transform.position = Vector3.SmoothDamp(p2.transform.position, lastSave.transform.position + lastSave.t2, ref v, 0.6f);

        playerMng.p1.transform.position = lastSave.transform.position + lastSave.t1;
        playerMng.p2.transform.position = lastSave.transform.position + lastSave.t2;
        // playerMng.p1.GetComponent<SpriteRenderer>().enabled = true;
        // playerMng.p2.GetComponent<SpriteRenderer>().enabled = true;
        playerMng.p1.gameObject.SetActive(true);
        playerMng.p2.gameObject.SetActive(true);

    }

}
