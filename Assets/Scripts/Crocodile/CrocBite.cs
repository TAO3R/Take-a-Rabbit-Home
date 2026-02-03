using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocBite: MonoBehaviour
{
    private CapsuleCollider2D biteCld;
    private Vector2 rightBiteOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        biteCld = GetComponent<CapsuleCollider2D>();
        rightBiteOffset = transform.localPosition;
        biteCld.enabled = false;
    }

    public void BiteRight()
    {
        transform.localPosition = rightBiteOffset;
        biteCld.enabled = true;
    }

    public void BiteLeft()
    {
        transform.localPosition = new Vector3(rightBiteOffset.x * -1, rightBiteOffset.y, 0);
        biteCld.enabled = true;
    }

    public void BiteEnd()
    {
        biteCld.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<RabbitBehaviour>() != null)
            {
                RabbitBehaviour rabbit = collision.GetComponent<RabbitBehaviour>();
                rabbit.bittenByCroc = true;
            }
        }
    }
}
