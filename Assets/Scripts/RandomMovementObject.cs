using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementObject : MonoBehaviour
{
    public GameObject parent;
    public GameObject[] childrenObjects;
    private GameObject[] lines;
    private LineRenderer line;

    float startDistance;

    private Rigidbody rb;
    public float force = 3f;
    private bool isMove = false;

    Vector3 startPosition;

    private Vector3 direction;

    private float delay = 0.1f;

    [SerializeField]
    private float minDelay = 1f;
    [SerializeField]
    private float maxDelay = 3f;
    [SerializeField]
    private float maxDistance = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Start()
    {
        startDistance = Vector3.Distance(parent.transform.position, transform.position);
        startPosition = transform.position;

        StartCoroutine(DelayChangeMovement());
    }

    void Update()
    {
        if (isMove)
        {
            float currentDistant = Math.Abs(Vector3.Distance(parent.transform.position, transform.position));

            bool isMaxDistance = startDistance > (currentDistant + maxDistance) || startDistance < (currentDistant - maxDistance);
            //bool isMaxDistance = Vector3.Distance(startPosition, transform.parent.position) > maxDistance;
            if (isMaxDistance)
            {
                var velocity = rb.velocity;
                rb.velocity = Vector3.zero;
                rb.AddForce(velocity * -1, ForceMode.Impulse);
            }
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, childrenObjects[0].transform.position);
        //if (isMove)
        //{
        //    bool isMaxDistance = Vector3.Distance(startPosition, transform.position) > maxDistance;
        //    if (isMaxDistance)
        //    {
        //        var velocity = rb.velocity;
        //        rb.velocity = Vector3.zero;
        //        rb.AddForce(velocity * -1 * force, ForceMode.Impulse);
        //    }
        //}

    }

    private Vector3 GetRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
    }

    IEnumerator DelayChangeMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (minDelay >= maxDelay)
            {
                delay = minDelay;
            }
            else delay = UnityEngine.Random.Range(minDelay, maxDelay);

            rb.velocity = Vector3.zero;
            direction = GetRandomDirection();
                rb.velocity = Vector3.zero;
            rb.AddForce(direction * force, ForceMode.Impulse);
            isMove = true;
        }     
    }

}
