using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBite : MonoBehaviour
{
    public GameObject childRabbit;

    private Animator evilCrocAnim;

    private void Start()
    {
        evilCrocAnim = GetComponentInParent<Animator>();
        evilCrocAnim.SetBool("Hungry", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // "Secure" child rabbit
            RabbitExtra childRabbitExtraScript = childRabbit.GetComponent<RabbitExtra>();
            childRabbitExtraScript.secured = true;

            // Kill mama rabbit
            if (collision.GetComponent<RabbitBehaviour>() != null)
            {
                RabbitBehaviour rabbit = collision.GetComponent<RabbitBehaviour>();
                rabbit.bittenByCroc = true;
                evilCrocAnim.SetBool("Hungry", false);
            }
        }
    }
}
