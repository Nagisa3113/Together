using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

public class SceneInfo : MonoBehaviour
{
    public bool isDebug;
    public float intensity;
    public List<SavePoint> savePoints;

    PlayerController p1, p2;

    void Start()
    {
        while (p1 == null || p2 == null)
        {
            p1 = PlayerManager.Instance.p1;
            p2 = PlayerManager.Instance.p2;
        }


        if (!isDebug)
        {
            p1.transform.position = savePoints[0].P1Pos;
            p2.transform.position = savePoints[0].P2Pos;
        }
        p1.GetComponentInChildren<Light2D>().intensity = this.intensity;
        p2.GetComponentInChildren<Light2D>().intensity = this.intensity;
        var facade = GameObject.Find("GameFacade").GetComponent<GameFacade>();
        facade.savePoints = this.savePoints;
        facade.lastSave = savePoints[0];

        var targetGroup = GameObject.Find("TargetGroup").GetComponent<CinemachineTargetGroup>();
        targetGroup.m_Targets[0].target = p1.transform;
        targetGroup.m_Targets[1].target = p2.transform;
    }


}
