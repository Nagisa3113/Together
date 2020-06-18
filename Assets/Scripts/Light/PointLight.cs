using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class PointLight : MonoBehaviour, ILight2D, IInteractive
{
    public SpriteRenderer lightedSprite;
    public SpriteRenderer unlightedSprite;

    Light2D light2D;
    float outR;
    float inR;
    float intensity;


    [SerializeField]
    float maxTime = 1f;

    void Awake()
    {
        light2D = GetComponentInChildren<Light2D>();
        light2D.enabled = false;
        lightedSprite.enabled = true;
        unlightedSprite.enabled = false;

        outR = light2D.pointLightOuterRadius;
        inR = light2D.pointLightInnerRadius;
        intensity = light2D.intensity;
    }

    public void Interactive()
    {
        Lighten();
    }


    public void Lighten()
    {
        lightedSprite.enabled = true;
        unlightedSprite.enabled = false;
        if (light2D.enabled == false)
            StartCoroutine(BecomeVisable());
    }

    public void UnLighten()
    {
        lightedSprite.enabled = false;
        unlightedSprite.enabled = true;
    }


    IEnumerator BecomeVisable()
    {
        float timer = 0;
        while (timer < maxTime)
        {
            light2D.enabled = true;
            timer += Time.deltaTime;
            light2D.pointLightOuterRadius = Mathf.Lerp(0, outR, timer / maxTime);
            light2D.pointLightInnerRadius = Mathf.Lerp(0, inR, timer / maxTime);
            light2D.intensity = Mathf.Lerp(0, intensity, timer / maxTime);
            yield return 0;
        }
    }

}
