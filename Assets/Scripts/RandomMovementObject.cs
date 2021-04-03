using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementObject : MonoBehaviour
{
    public GameObject[] childrenObjects;
    private GameObject[] lines;

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
    }

    void Start()
    {
        rb.useGravity = false;
        startPosition = transform.position;

        for (int i = 0; i < childrenObjects.Length; i++)
        {
            
        }

        StartCoroutine(DelayChangeMovement());
    }

    
    void Update()
    {
        if (isMove)
        {
            bool isMaxDistance = Vector3.Distance(startPosition, transform.position) > maxDistance;
            if (isMaxDistance)
            {
                var velocity = rb.velocity;
                rb.velocity = Vector3.zero;
                rb.AddForce(velocity * -1, ForceMode.Impulse);
            }
        }
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
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
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
            else delay = Random.Range(minDelay, maxDelay);

            rb.velocity = Vector3.zero;
            direction = GetRandomDirection();
                rb.velocity = Vector3.zero;
            rb.AddForce(direction * force, ForceMode.Impulse);
            isMove = true;
        }     
    }

}
