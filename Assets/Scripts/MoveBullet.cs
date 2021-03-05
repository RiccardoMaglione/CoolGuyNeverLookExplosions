using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public float speed = 1;

    void Update()
    {
        transform.Translate(Vector3.forward * speed);       //Muove il proiettile
    }
}