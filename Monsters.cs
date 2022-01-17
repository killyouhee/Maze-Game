using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    public int a = 1;
    public int speed;
    public float minX;
    public float maxX;
    

    void Update()
    {
        if (transform.localPosition.x < minX)
        {
            a = -1;
        }
        else if (transform.localPosition.x > maxX)
        {
            a = 1;
        }

        transform.Translate(Vector3.left * 5.0f * Time.deltaTime * a * speed);
    }
}

