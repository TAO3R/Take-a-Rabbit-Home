using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilIdle : MonoBehaviour
{
    [SerializeField]
    private  Animator evilCrocAnim;
    [SerializeField]
    private SpriteRenderer evilCrocRend;
    [SerializeField]
    private EvilCroc evilCrocScript;

    private void Start()
    {
        evilCrocAnim = GetComponentInParent<Animator>();
        evilCrocRend = GetComponentInParent<SpriteRenderer>();
        evilCrocScript = GetComponentInParent<EvilCroc>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Set direction
            if (collision.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX)
            {
                evilCrocRend.flipX = false;
                evilCrocScript.stepped = true;
            }
            else
            {
                evilCrocRend.flipX = true;
                evilCrocScript.stepped = true;
            }

            // Set attack bool
            evilCrocAnim.SetBool("SteppedByRabbit", true);
        }
    }
}
