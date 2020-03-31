using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class GameFacade : MonoBehaviour
{
    [HideInInspector]
    public List<MovableObject> movableObjects;

    private static GameFacade _instance;
    public static GameFacade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameFacade").GetComponent<GameFacade>();
            }
            return _instance;
        }
    }

    public SavePoint lastSave;

    private PlayerManager playerMng;
    private RequestManager requestMng;
    private ClientManager clientMng;

    public bool StartSyncSymble = false;

    public bool IsStartGame;
    public bool IsConnected;


    void Awake()
    {
        clientMng = new ClientManager(this);
        requestMng = new RequestManager(this);
        playerMng = new PlayerManager(this);//要在最后


        Screen.SetResolution(640, 480, false);
    }

    
    private void Update()
    {
        if (StartSyncSymble)
        {
            StartPlaying();
            StartSyncSymble = false;
        }
    }

    private void OnDestroy()
    {
        DestroyManager();
    }

    private void InitManager()
    {
        clientMng.OnInit();
        playerMng.OnInit();
    }
    private void DestroyManager()
    {
        playerMng.OnDestroy();
        clientMng.OnDestroy();
    }


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
        // foreach (MovableObject m in movableObjects)
        // {
        //     m.StartCoroutine(m.Move());
        // }
    }

    public void GameOver()
    {
        //private GameObject currentRoleGameObject;
        //private GameObject playerSyncRequest;
        //private GameObject remoteRoleGameObject;

        //private ShootRequest shootRequest;
        //private AttackRequest attackRequest;

        // GameObject.Destroy(currentRoleGameObject);
        // GameObject.Destroy(playerSyncRequest);
        // GameObject.Destroy(remoteRoleGameObject);
    }


    public void ResetPlayers()
    {
        //Vector3 v = Vector3.zero;
        //p1.gameObject.SetActive(false);
        //p2.gameObject.SetActive(false);

        //p1.transform.position = Vector3.SmoothDamp(p1.transform.position, lastSave.transform.position + lastSave.t1, ref v, 0.6f);
        //p2.transform.position = Vector3.SmoothDamp(p2.transform.position, lastSave.transform.position + lastSave.t2, ref v, 0.6f);

        // p1.transform.position = lastSave.transform.position + lastSave.t1;
        // p2.transform.position = lastSave.transform.position + lastSave.t2;
        //p1.gameObject.SetActive(true);
        //p2.gameObject.SetActive(true);
    }

}
