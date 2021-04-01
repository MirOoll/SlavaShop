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

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator delayChangeMovement()
    {
        yield return new WaitForSeconds(2f);
    }

    private void startRandomMoving()
    {
        getRandomMovement();
    }
    private void getRandomMovement()
    {
        
        Vector3 targetPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f) * Time.deltaTime);
        rb.MovePosition(rb.position + targetPosition);
        //StartCoroutine(delayChangeMovement());
        //getRandomMovement();
    }

    private void FixedUpdate()
    {
        getRandomMovement();
    }
}
