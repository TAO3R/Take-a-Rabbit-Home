using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilCroc : MonoBehaviour
{
    public GameObject childRabbit;
    public GameObject mamaRabbit;
    public LevelManager levelManagerScript;

    public bool stepped;
    public bool hungry;

    private Animator evilCrocAnim;
    private CapsuleCollider2D biteCld, idleCld, fAttackCld;
    private Vector2 biteOffset, idleOffset;
    private Vector3 attackPosOffset, attackPos;
    [SerializeField]
    private float speed;
    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        // Get and Set animator
        evilCrocAnim = GetComponent<Animator>();
        // evilCrocAnim.SetBool("SteppedByRabbit", false);

        // Get children
        biteCld = transform.GetChild(0).GetComponent<CapsuleCollider2D>();
        idleCld = transform.GetChild(1).GetComponent<CapsuleCollider2D>();
        fAttackCld = transform.GetChild(2).GetComponent<CapsuleCollider2D>();

        // Sets colliders for children
        biteCld.enabled = false;
        idleCld.enabled = true;
        fAttackCld.enabled = false;

        biteOffset = biteCld.transform.localPosition;
        idleOffset = idleCld.transform.localPosition;

        stepped = false;
        hungry = true;

        // Direction
        GetComponent<SpriteRenderer>().flipX = true;
        
        // Bite collider
        transform.GetChild(0).localPosition = new Vector3(biteOffset.x * -1, biteOffset.y, 0);
        // Idle collider
        transform.GetChild(1).localPosition = new Vector3(idleOffset.x * -1, idleOffset.y, 0);
        

        // Set Destinition
        attackPosOffset = new Vector3(2.672f, -0.72f, 0);
        attackPos = childRabbit.transform.position + attackPosOffset;

        speed = 0.3f;

        isAttacking = false;
    }

    private void Update()
    {
        // Move
        if (!isAttacking)
        {
            float dis = Vector3.Distance(transform.position, attackPos);
            if (dis >= 0.05f)
            {
                Vector2 dir = (attackPos - transform.position).normalized;
                transform.Translate(dir * speed * Time.deltaTime, Space.World);
            }
            else
            {
                evilCrocAnim.SetBool("AttackForward", true);
            }
        }

        // Change direction accordingly when stepped
        if (stepped)
        {
            // Direction
            if (GetComponent<SpriteRenderer>().flipX)
            {
                // Bite collider
                transform.GetChild(0).localPosition = new Vector3(biteOffset.x * -1, biteOffset.y, 0);
                // Idle collider
                transform.GetChild(1).localPosition = new Vector3(idleOffset.x * -1, idleOffset.y, 0);
            }
            else
            {
                // Bite collider
                transform.GetChild(0).localPosition = biteOffset;
                // Idle collider
                transform.GetChild(1).localPosition = idleOffset;
            }

            stepped = false;
        }
    }

    public void Emerge_start()
    {
        biteCld.enabled = false;
        idleCld.enabled = false;
    }

    public void Emerge_end()
    {
        biteCld.enabled = false;
        idleCld.enabled = true;
    }
    
    public void Up_attack_start()      // Used by animation
    {
        isAttacking = true;

        evilCrocAnim.SetBool("SteppedByRabbit", false);

        idleCld.enabled = false;
        biteCld.enabled = true;
    }

    public void Up_bite_end()      // Used by animation
    {
        biteCld.enabled = false;
    }

    public void Up_attack_end()        // Used by animation
    {
        isAttacking = false;
            
        idleCld.enabled = true;
        if (GetComponent<SpriteRenderer>().flipX != true)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            // Bite collider
            transform.GetChild(0).localPosition = new Vector3(biteOffset.x * -1, biteOffset.y, 0);
            // Idle collider
            transform.GetChild(1).localPosition = new Vector3(idleOffset.x * -1, idleOffset.y, 0);
        }
    }

    public void Forward_attack_start()      // Used by animation
    {
        biteCld.enabled = false;
        idleCld.enabled = false;
    }

    public void Froward_bite_start()      // Used by animation
    {
        fAttackCld.enabled = true;
        childRabbit.transform.GetChild(1).GetComponent<Animator>().SetBool("BittenByCroc", true);
        childRabbit.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        hungry = false;
        
        if (Vector3.Distance(childRabbit.transform.position, mamaRabbit.transform.position) < 1f)
        {
            mamaRabbit.transform.GetChild(1).GetComponent<Animator>().SetBool("BittenByCroc", true);
            levelManagerScript.ReloadLevel();
        }
    }

    public void Forward_bite_end()      // Used by animation
    {
        fAttackCld.enabled = false;
    }

    public void Forward_attack_end()        // Used by animation
    {
        Destroy(gameObject);
    }
}
