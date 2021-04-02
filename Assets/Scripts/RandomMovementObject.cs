using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementObject : MonoBehaviour
{

    public GameObject[] childrenObjects;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
    }

    Vector3 oldPosition;
    Vector3 newPosition;
    void Update()
    {
        
        while (transform.position != newPosition)
        {
            transform.position = Vector3.Lerp(oldPosition, newPosition, 2f);
        }
        
    }

    IEnumerator delayChangeMovement()
    {
        yield return new WaitForSeconds(2f);
    }

    private void startRandomMoving()
    {
        getRandomMovement();
    }
    private Vector3 vec;
    private void getRandomMovement()
    {
        //Vector3.Lerp(Vector3(gameObject.transform, gameObject.transform, gameObject.transform),)
        //Vector3 targetPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        //rb.MovePosition(rb.position + targetPosition * Time.deltaTime);
        //StartCoroutine(delayChangeMovement());
        //getRandomMovement();
    }

    private void FixedUpdate()
    {
        //getRandomMovement();

    }
}
