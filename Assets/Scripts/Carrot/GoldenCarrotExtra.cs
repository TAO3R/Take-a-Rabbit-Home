using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldenCarrotExtra : MonoBehaviour
{
    public int carrotCount;
    public LevelManager levelManagerScript;
    public EvilCroc evilCrocScript;

    private bool isWaiting;

    // Start is called before the first frame update
    void Awake()
    {
        carrotCount = 0;
        levelManagerScript = GameObject.Find("Level Manager").GetComponent<LevelManager>();

        GetComponent<CapsuleCollider2D>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;

        isWaiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (carrotCount == 1)
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        }

        if (SceneManager.GetActiveScene().name == "Level10" && isWaiting)
        {
            if (evilCrocScript.hungry == false)
            {
                levelManagerScript.LoadNextLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (carrotCount == 1)
        {
            if (collision.gameObject.tag == "Player" && SceneManager.GetActiveScene().name != "Level10") 
            { 
                levelManagerScript.LoadNextLevel(); 
            }
            else if (collision.gameObject.tag == "ChildRabbit")
            {
                levelManagerScript.LoadNextLevel();
            }
            else if (collision.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "Level10")
            {
                collision.gameObject.GetComponent<RabbitBehaviour>().enabled = false;
                Animator mamaRabbitAnim =  collision.gameObject.transform.GetChild(1).GetComponent<Animator>();
                mamaRabbitAnim.SetBool("IsJumping", false);
                mamaRabbitAnim.SetBool("FacingFront", true);
                mamaRabbitAnim.SetBool("FacingBack", false);
                collision.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = true;
                isWaiting = true;
            }
        }
    }
}
