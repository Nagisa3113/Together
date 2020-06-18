using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect_example : MonoBehaviour
{
    public Transform target;
    float timerCounter = 3f;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            this.GetComponent<Animator>().enabled = true;
            StartCoroutine(Generate());
            this.GetComponent<Collider2D>().enabled = false;
        }

    }

    IEnumerator Generate()
    {
        Vector3 vector = transform.position;
        float timer = 0;
        while (timer < timerCounter)
        {
            this.transform.position = Vector3.Lerp(vector, target.position, timer / timerCounter);
            timer += Time.fixedDeltaTime;
            yield return 0;
        }
        this.GetComponent<Animator>().enabled = false;


    }


}
