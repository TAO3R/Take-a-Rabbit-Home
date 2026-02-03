using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
    public float fluctuateRange;
    public float fluctuateSpeed;
    public float fluctuateSpeedModifier;
    public float sinkCD;

    private float time;
    
    private Vector3 initialPos;
    private Animator lilypadAnim;
    private SpriteRenderer lilypadRend;
    private CapsuleCollider2D lilypadCld;
    
    // Start is called before the first frame update
    void Start()
    {
        fluctuateRange = 0.025f;
        fluctuateSpeedModifier = 3.15f;

        time = 0f;
        sinkCD = 5f;
        initialPos = transform.localPosition;

        lilypadAnim = GetComponent<Animator>();
        lilypadRend = GetComponent<SpriteRenderer>();
        lilypadCld = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // float
        time += Time.fixedDeltaTime;
        fluctuateSpeed = Mathf.Sin(time * fluctuateSpeedModifier);

        Vector3 lilyPadPos = new Vector3(initialPos.x, initialPos.y + fluctuateSpeed * fluctuateRange, initialPos.z);
        transform.position = lilyPadPos;

        // sink
        if (transform.childCount == 1)
        {
            if (transform.GetChild(0).tag == "Player")
            {
                sinkCD -= Time.fixedDeltaTime;
                if (sinkCD <= 0f) { Sink(); }
            }
        }
        else { sinkCD = 5f; }
    }

    private void Sink()
    {
        lilypadAnim.SetTrigger("Sink");
        sinkCD = 5f;
    }

    public void Full_sink()
    {
        lilypadRend.enabled = false;
        lilypadCld.enabled = false;
        if (transform.childCount > 0)
        {
            if (transform.GetChild(0) != null)
            {
                transform.GetChild(0).GetComponent<RabbitBehaviour>().droppedInWater = true;
            }
        }
    }

    public void Back_to_water()
    {
        lilypadRend.enabled = true;
        lilypadCld.enabled = true;
    }
}
