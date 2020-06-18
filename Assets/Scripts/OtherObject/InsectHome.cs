using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InsectHome : MonoBehaviour
{
    public GameObject Insect;
    public Transform insectPos;

    [HideInInspector]
    public Vector3 applePos;
    public Transform apple;
    public bool hasApple;

    public List<Transform> points;
    public List<Light2D> light2Ds;
    int nums;
    float deltaTime = 4f;

    bool isStart;


    // Start is called before the first frame update
    void Start()
    {
        applePos = apple.position;
        // StartCoroutine(Generate());
    }
    void Update()
    {
        if (light2Ds[0].enabled && light2Ds[1].enabled && light2Ds[2].enabled && light2Ds[3].enabled)
        {
            GameFacade.Instance.NextLevel();
        }

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && isStart == false)
        {
            isStart = true;
            StartCoroutine(Generate());

        }

    }


    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Apple"))
        {
            hasApple = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Apple"))
        {
            hasApple = false;
        }
    }

    IEnumerator Generate()
    {

        while (nums < points.Count)
        {
            Insect ins = Instantiate(Insect).GetComponent<Insect>();
            ins.transform.SetParent(this.transform);
            ins.transform.localPosition = insectPos.localPosition;
            ins.insectHome = this;
            ins.index = nums;
            nums++;

            yield return new WaitForSeconds(deltaTime);
        }

    }

    public void ResetApple()
    {
        apple.gameObject.SetActive(false);
        apple.transform.position = applePos;
        apple.gameObject.SetActive(true);
    }


}
