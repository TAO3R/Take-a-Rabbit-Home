using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public Transform carrot;
    
    public float scalingSpeed;
    public float carrotBobRange;
    public float shadowScalingRange;

    private Vector3 carrotInitialPos;
    private float time;

    public GoldenCarrotExtra goldenCarrotScript;

    // Start is called before the first frame update
    void Start()
    {
        carrotInitialPos = carrot.position;
        time = 0f;
        carrotBobRange = carrot.localScale.x *  0.125f;
        shadowScalingRange = transform.localScale.x * 0.15f;

        goldenCarrotScript = GameObject.Find("Objects Holder/Carrots/Golden Carrot Holder").GetComponent<GoldenCarrotExtra>();
        goldenCarrotScript.carrotCount += 1;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        scalingSpeed = Mathf.Sin(time);
        Vector3 carrotPos = new Vector3(carrotInitialPos.x, carrotInitialPos.y + scalingSpeed * carrotBobRange, carrotInitialPos.z);
        carrot.position = carrotPos;

        transform.localScale = new Vector3(1 - scalingSpeed * shadowScalingRange, 1 - scalingSpeed * shadowScalingRange, 1);
    }
}
