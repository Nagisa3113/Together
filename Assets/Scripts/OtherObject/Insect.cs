using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum InsectStatus
{
    Idle,
    MoveToApple,
    MoveToLight,
}

public class Insect : MonoBehaviour
{
    public int index;

    InsectStatus status;

    public InsectHome insectHome;

    public Vector3 tarPos;

    float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        if (this.index == -1)
        {
            status = InsectStatus.Idle;
        }
        else
        {
            tarPos = insectHome.points[index].position;
            StartCoroutine(MoveToLight());

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case InsectStatus.Idle:
                if (insectHome.hasApple)
                {
                    status = InsectStatus.MoveToApple;
                    StopAllCoroutines();
                    StartCoroutine(MoveToApple());
                }
                else
                {
                    status = InsectStatus.MoveToLight;
                    StopAllCoroutines();
                    StartCoroutine(MoveToLight());
                }
                break;
            case InsectStatus.MoveToApple:
                if (!insectHome.hasApple)
                {
                    status = InsectStatus.MoveToLight;
                    StopAllCoroutines();
                    StartCoroutine(MoveToLight());
                }
                break;
            case InsectStatus.MoveToLight:
                if (insectHome.hasApple)
                {
                    status = InsectStatus.MoveToApple;
                    StopAllCoroutines();
                    StartCoroutine(MoveToApple());
                }
                break;
        }


        if (index == -1) return;

        if (Vector3.Distance(this.transform.position, tarPos) < 0.5f
            && insectHome.light2Ds[index].enabled == true)
        {
            insectHome.light2Ds[index].intensity -= 1f * Time.deltaTime;
            insectHome.light2Ds[index].pointLightOuterRadius -= 0.5f * Time.deltaTime;
            insectHome.light2Ds[index].pointLightInnerRadius -= 0.5f * Time.deltaTime;

            if (insectHome.light2Ds[index].intensity < 1)
            {
                insectHome.light2Ds[index].enabled = false;
            }
        }


    }

    IEnumerator MoveToApple()
    {
        Light2D appleLight = insectHome.apple.GetComponent<Light2D>();

        while (insectHome.hasApple)
        {
            this.GetComponent<Collider2D>().enabled = false;
            this.transform.position = Vector3.MoveTowards(transform.position, insectHome.apple.position, speed * Time.deltaTime);
            //appleLight.intensity -= 0.3f * Time.deltaTime;
            yield return 0;
        }
        this.GetComponent<Collider2D>().enabled = true;


        status = InsectStatus.Idle;
        if (index != -1)
        {
            insectHome.hasApple = false;
            insectHome.ResetApple();
        }


    }


    public IEnumerator MoveToLight()
    {
        //Vector3 tmpPos;
        //tmpPos = new Vector3(tarPos.x, transform.position.y, 0);

        //while (Vector3.Distance(this.transform.position, tmpPos) > 0.5f)
        //{
        //    this.transform.position = Vector3.MoveTowards(transform.position, tmpPos, speed * Time.deltaTime);
        //    yield return 0;
        //}
        while (Vector3.Distance(this.transform.position, tarPos) > 0.5f)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, tarPos, speed * Time.deltaTime);
            yield return 0;
        }
        status = InsectStatus.Idle;

    }

}
