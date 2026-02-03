using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitExtra : MonoBehaviour
{
    public GameObject mamaRabbit;

    public bool secured;

    private RabbitBehaviour mamaRabbitScript;
    private Animator rabbitExtraAnim;

    // Start is called before the first frame update
    void Awake()
    {
        mamaRabbitScript = mamaRabbit.GetComponent<RabbitBehaviour>();
        gameObject.GetComponent<RabbitBehaviour>().enabled = false;
        secured = false;
        rabbitExtraAnim = transform.GetChild(1).GetComponent<Animator>();
        // rabbitExtraAnim.SetBool("Secured", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (secured)
        {
            gameObject.GetComponent<RabbitBehaviour>().enabled = true;
            // Need to implement the Animator first!!!
            //rabbitExtraAnim.SetBool("Secured", true);
        }
    }
}
