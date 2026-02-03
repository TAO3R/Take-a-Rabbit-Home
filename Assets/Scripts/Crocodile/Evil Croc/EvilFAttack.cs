using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ChildRabbit")
        {
            Debug.Log("Eat child rabbit!");
            Animator childRabbitAnim = collision.gameObject.transform.GetChild(1).GetComponent<Animator>();
            childRabbitAnim.SetBool("BittenByCroc", true);
        }
    }
}
