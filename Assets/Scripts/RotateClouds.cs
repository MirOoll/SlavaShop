using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateClouds : MonoBehaviour
{
    public float speed = 1.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
