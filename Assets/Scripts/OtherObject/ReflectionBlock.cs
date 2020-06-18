using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ReflectionBlock : MonoBehaviour
{
    bool isIn;
    public Transform block;

    private Camera cCamera;

    private void Start()
    {
        cCamera = Camera.main;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (++GameFacade.Instance.tempname > 0)
        {
            cCamera.GetComponent<CinemachineBrain>().enabled = false;
            cCamera.transform.position = new Vector3(6.5f, -3.3f, -10f);
            cCamera.orthographicSize = 10f;
        }

    }
    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            block.transform.Rotate(0, 0, 3f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            block.transform.Rotate(0, 0, -3f * Time.deltaTime);
        }

        cCamera.GetComponent<CinemachineBrain>().enabled = false;
        cCamera.transform.position = new Vector3(6.5f, -3.3f, -10f);
        cCamera.orthographicSize = 10f;
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (--GameFacade.Instance.tempname == 0)
        {
            cCamera.GetComponent<CinemachineBrain>().enabled = true;
        }
    }




}
