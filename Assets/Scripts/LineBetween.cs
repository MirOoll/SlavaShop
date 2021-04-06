using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBetween : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer line;
    public GameObject first;
    public GameObject second;
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, first.transform.position);
        line.SetPosition(1, second.transform.position);
    }
}
