using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteStart : MonoBehaviour
{
    public GameObject boxPrefab;

    private float instantiateCD;

    // Start is called before the first frame update
    void Start()
    {
        instantiateCD = Random.Range(2f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (instantiateCD <= 0)
        {
            Instantiate(boxPrefab, transform.position, transform.rotation);
            instantiateCD = Random.Range(2f, 12f);
        }

        instantiateCD -= Time.deltaTime;
    }
}
