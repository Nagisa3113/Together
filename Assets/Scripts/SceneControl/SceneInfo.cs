using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

public class SceneInfo : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    public float intensity;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        GameObject p1 = GameObject.Find("Player1");
        GameObject p2 = GameObject.Find("Player2");
        p1.transform.position = this.p1.position;
        p2.transform.position = this.p2.position;
        p1.GetComponentInChildren<Light2D>().intensity = this.intensity;
        p2.GetComponentInChildren<Light2D>().intensity = this.intensity;

        var targetGroup = GameObject.Find("TargetGroup").GetComponent<CinemachineTargetGroup>();
        targetGroup.m_Targets[0].target = p1.transform;
        targetGroup.m_Targets[1].target = p2.transform;
    }


}
