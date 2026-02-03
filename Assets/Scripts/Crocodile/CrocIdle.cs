using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocIdle : MonoBehaviour
{
    public Animator crocAnim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            crocAnim.SetBool("SteppedByRabbit", true);
        }
    }
}
