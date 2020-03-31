using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour
{
    public Transform m_camera;

    public Transform[] bgs;
    public Vector3[] bgx;

    public float offset = 0.01f;
    float startPos;

    void Awake()
    {
        startPos = m_camera.position.x;
        bgx = new Vector3[bgs.Length];
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bgs.Length; i++)
        {
            bgx[i] = bgs[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bgs.Length; i++)
        {
            bgs[i].position = bgx[i] - new Vector3((m_camera.position.x - startPos) * i * offset, 0, 0);
        }
    }
}
