using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxHolder : MonoBehaviour
{
    public float fluctuateRange;
    public float fluctuateSpeed;
    public float fluctuateSpeedModifier;

    private float time;
    private Transform boxTrans;

    public bool onFlowingWater;

    private Vector2 dir;
    private float speed;

    // Start is called before the first frame update
    private void Start()
    {
        fluctuateRange = 0.001f;
        fluctuateSpeedModifier = 2f;

        boxTrans = transform.GetChild(0);

        time = 0f;

        // Whether the box flow with water
        if (SceneManager.GetActiveScene().buildIndex >= 5) { onFlowingWater = true; }

        if (onFlowingWater)
        {
            speed = 1f;
            dir = new Vector2(0.871575f, -0.490261f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Floating bob
        time += Time.fixedDeltaTime;
        fluctuateSpeed = Mathf.Sin(time * fluctuateSpeedModifier);

        Vector3 boxPos = new Vector3(boxTrans.localPosition.x, boxTrans.localPosition.y + fluctuateSpeed * fluctuateRange, boxTrans.localPosition.z);
        boxTrans.localPosition = boxPos;
        
        if (onFlowingWater) { transform.Translate(dir * speed * Time.deltaTime); }

        // Sink detection
        if (transform.position.x >= 12 ||
                transform.position.y <= -8 ||
                transform.GetChild(0).GetComponent<Box>().sank)
        { Sink(); }
    }

    public void Sink()
    {
        if (transform.GetChild(0).childCount > 0)
        {
            if (transform.GetChild(0).GetChild(0).tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        Destroy(gameObject);
        return;
    }
}
