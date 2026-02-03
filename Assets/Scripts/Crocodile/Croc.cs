using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Croc : MonoBehaviour
{
    public CrocBite crocBiteScript;
    public CapsuleCollider2D crocIdleCld;

    public float biteCD;
    public BiteCDIndicator biteIndicatorScript;
    public GameObject biteCDUI;

    private Animator crocAnimator;
    private SpriteRenderer crocRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get necessary components first
        crocAnimator = GetComponent<Animator>();
        crocRenderer = GetComponent<SpriteRenderer>();

        crocAnimator.SetBool("SteppedByRabbit", false);

        // Croc behavior
        if (SceneManager.GetActiveScene().buildIndex <= 7)
        {
            Debug.Log("Croc are at daytime");
            crocAnimator.SetBool("AtDaytime", true);
            biteCDUI.SetActive(false);
        }
        else
        {
            Debug.Log("Croc are at night");
            crocAnimator.SetBool("AtDaytime", false);
            biteCDUI.SetActive(true);
            biteCD = Random.Range(1f, 5f);
            biteIndicatorScript.SetMaxCD(biteCD);
        }

        // Direction of idle collider & bite indicator
        if (crocRenderer.flipX == true)
        {
            // Idle collider
            Transform idleCldTrans = transform.GetChild(1).GetComponent<Transform>();
            Vector2 rightIdleOffset = idleCldTrans.localPosition;
            idleCldTrans.localPosition = new Vector3(rightIdleOffset.x * -1, rightIdleOffset.y, 0);
            // Bite indicator
            Transform biteIndicatorTrans = transform.GetChild(2).GetComponent<Transform>();
            Vector2 rightIndicatorOffset = biteIndicatorTrans.localPosition;
            biteIndicatorTrans.localPosition = new Vector3(rightIndicatorOffset.x * -1, rightIndicatorOffset.y, 0);
        }
    }

    private void Update()
    {
        if (crocAnimator.GetBool("AtDaytime")) { return; }

        biteCD -= Time.deltaTime;
        biteIndicatorScript.SetBiteCD(biteCD);

        if (biteCD <= 0)
        {
            crocAnimator.SetBool("SteppedByRabbit", true);
            biteCD = 999f;
        }
    }

    public void CrocAttackStart()    // Disable collider for rabbit to be grounded
    {
        crocAnimator.SetBool("SteppedByRabbit", false);
        biteCD = 999f;
        crocIdleCld.enabled = false;

        if (crocRenderer.flipX == false) { crocBiteScript.BiteRight(); }
        else { crocBiteScript.BiteLeft(); }
    }

    public void CrocBiteEnd()
    {
        crocBiteScript.BiteEnd();
    }

    public void CrocAttackEnd()
    {
        crocIdleCld.enabled = true;

        if (crocAnimator.GetBool("AtDaytime") == false)
        {
            biteCD = Random.Range(5f, 10f);
            biteIndicatorScript.SetMaxCD(biteCD);
        }
    }
}
