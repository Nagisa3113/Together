using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    [Header("光圈开始增大的距离")]
    [SerializeField] float lightDistance = 5f;
    [Header("光圈可以增大的范围")]
    [SerializeField] float deltaLightRadius = 3f;
    public Light2D light2D;

    float lightOutRadiousNormal;
    float lightOutRadiousMax;

    Transform self, target;

    LineRenderer lineRenderer;


    float distance;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        light2D = GetComponent<Light2D>();
        lightOutRadiousNormal = light2D.pointLightOuterRadius;
        lightOutRadiousMax = lightOutRadiousNormal + deltaLightRadius;
        self = this.transform.parent.transform;
    }

    void Start()
    {
        target = PlayerManager.Instance.GetAnotherPlayerTransform(self);
    }

    void Update()
    {
        distance = Vector2.Distance(self.position, target.position);

        light2D.pointLightOuterRadius = Mathf.Lerp(
            lightOutRadiousNormal, lightOutRadiousMax,
            Mathf.Clamp01((lightDistance - distance) / lightDistance));

        if (distance < 2 * light2D.pointLightOuterRadius)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, self.position);
            lineRenderer.SetPosition(1, target.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }

    }

}
