using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        test = GameObject.Find("test obj");
        Vector2 dir = (test.transform.position - transform.position).normalized;
        Debug.Log(dir);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
