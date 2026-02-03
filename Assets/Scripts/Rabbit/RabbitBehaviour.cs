using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitBehaviour : MonoBehaviour
{
    #region Variables

    public GameObject childRabbit;

    public LevelManager levelManagerScript;
    public GoldenCarrotExtra goldenCarrotScript;

    // Animations
    public Animator rabbitBodyAnim;
    public SpriteRenderer rabbitBodyRend;
    public Transform rabbitShadowTrans;

    // Water & Ground Detection
    private CapsuleCollider2D rabbitCld;
    public bool bittenByCroc, grounded, droppedInWater;

    // Jump & Fake height
    public Transform rabbitBodyTrans;
    public float horizontalSpeed, gravity, radiusOfJump;

    private Vector2 landingPosition;
    private bool canJump, tryJump, isJumping;
    private float jumpDistance = 0f, verticalVelocity = 0f, jumpFrame = 0f, verticalDistance = 0f;

    #endregion

    #region MonoBehavoir Lifecycle

    void Start()
    {
        levelManagerScript = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        goldenCarrotScript = GameObject.Find("Objects Holder/Carrots/Golden Carrot Holder").GetComponent<GoldenCarrotExtra>();

        rabbitCld = GetComponent<CapsuleCollider2D>();
        landingPosition = transform.position;

        { canJump = true; } { tryJump = false; } { isJumping = false; } { grounded = true; }

        // Jump parameter
        horizontalSpeed = 7f;
        radiusOfJump = 3f;

        // Death conditions
        bittenByCroc = false;
        droppedInWater = false;

        // Animation
        rabbitBodyAnim.SetBool("FacingFront", true);
        rabbitBodyAnim.SetBool("FacingBack", false);
    }

    private void Update()
    {
        Death_detection();

        if (droppedInWater || bittenByCroc) { return; }

        if(canJump && !tryJump) {
            Jump_attempt_detection();    // Initiation
        }

        if (canJump && tryJump) {
            Jump_initialization();
        }

        if (isJumping) {
            End_of_jump_detection();
        }
        
        if (canJump)
        {
            Ground_detection();
        }
            
        Death_detection();
    }

    private void FixedUpdate()
    {
        if (isJumping) { Jump_simulation(); }
    }

    #endregion

    private void Jump_attempt_detection()
    {
        if (Time.timeScale == 0) { return; }
        if (Input.GetMouseButtonUp(0)) { tryJump = true; } 
    }

    private void Jump_initialization()
    {
        // Jump parameters
        landingPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        jumpDistance = Vector2.Distance(transform.position, landingPosition);
        Debug.Log("landing position: " + landingPosition + "jump distance: " + jumpDistance);

        // Destroy the box beneath if there is one
        if (jumpDistance > 1.25f && jumpDistance <= radiusOfJump && transform.parent != null)
        {
            if (transform.parent.tag == "Box")
            {
                Animator boxAnim = transform.GetComponentInParent<Animator>();
                transform.parent = GameObject.Find("Rabbit Default Parent").transform;
                boxAnim.SetTrigger("rabbitJumpAway");
            }
        }

        // Animator parameters
        if (landingPosition.y >= transform.position.y)   // Facing front or back
        {
            rabbitBodyAnim.SetBool("FacingFront", false);
            rabbitBodyAnim.SetBool("FacingBack", true);
        }
        else
        {
            rabbitBodyAnim.SetBool("FacingFront", true);
            rabbitBodyAnim.SetBool("FacingBack", false);
        }

        if (landingPosition.x >= transform.position.x)   // Facing left or right
        {
            rabbitBodyRend.flipX = false;
        }
        else
        {
            rabbitBodyRend.flipX = true;
        }

        // Failed jump attempt
        if (jumpDistance <= horizontalSpeed * Time.fixedDeltaTime || jumpDistance > radiusOfJump)    
        {
            { canJump = true; } { tryJump = false; } { isJumping = false; } { grounded = true; }
            return;
        }

        // Fake height parameters
        if (jumpDistance < 1f) { gravity = -15000; }
        else if (jumpDistance >= 1f && jumpDistance < 3f) { gravity = -10000; }
        else if (jumpDistance >= 3f && jumpDistance < radiusOfJump) { gravity = -5000; }

        jumpFrame = Vector2.Distance(transform.position, landingPosition) / (horizontalSpeed * Time.fixedDeltaTime);
        verticalVelocity = (-1) * gravity * (jumpFrame / 2) * Time.fixedDeltaTime;

        { canJump = false; } { tryJump = false; } { isJumping = true; } { grounded = false; }

        // Animator parameters
        rabbitBodyAnim.SetBool("IsHalfway", false);
        rabbitBodyAnim.SetBool("IsJumping", true);

        // Disable shadow collision
        rabbitCld.enabled = false;

        // Initialize rabbit transform parent
        // transform.parent = GameObject.Find("Rabbit Default Parent").transform;
    }

    private void End_of_jump_detection()
    {
        float distanceToLandingPosition = Vector2.Distance(transform.position, landingPosition);

        // Animator parameters
        if (distanceToLandingPosition <= jumpDistance / 2)
        {
            rabbitBodyAnim.SetBool("IsHalfway", true);
        }

        // Enable collision a bit before finishing the jump
        if (distanceToLandingPosition <= 2 * horizontalSpeed * Time.fixedDeltaTime) { rabbitCld.enabled = true; }

        // End jumping
        if (distanceToLandingPosition <= horizontalSpeed * Time.fixedDeltaTime || distanceToLandingPosition > radiusOfJump)
        {
            rabbitBodyTrans.localPosition = new Vector3(0f, -0.25f, 0f);
            rabbitShadowTrans.localScale = new Vector3(1f, 1f, 1f);

            // Set booleans
            { canJump = true; } { tryJump = false; } { isJumping = false; }

            // Animator parameters
            rabbitBodyAnim.SetBool("IsJumping", false);
        }
    }

    private void Ground_detection()
    {
        if (grounded) { return; }
        else { droppedInWater = true; }
    }

    private void Jump_simulation()
    {
        // Horizontal Move
        Vector2 currentPosition = transform.position;
        Vector2 dir = (landingPosition - currentPosition).normalized;
        transform.Translate(dir * horizontalSpeed * Time.fixedDeltaTime, Space.World);

        // Vertical Move
        verticalDistance = verticalVelocity * Time.fixedDeltaTime + gravity / 2 * Time.fixedDeltaTime * Time.fixedDeltaTime;
        verticalVelocity += gravity * Time.fixedDeltaTime;

        // Tells the distance from the shadow (Ground
        // Debug.Log(verticalDistance);

        // rabbit body
        rabbitBodyTrans.localPosition += new Vector3(0, verticalDistance, 0) * Time.fixedDeltaTime;

        // Rabbit shadow
        rabbitShadowTrans.localScale -= new Vector3(verticalDistance / 10, verticalDistance / 10, 0) * Time.fixedDeltaTime;
    }

    private void Death_detection()
    {
        if (droppedInWater)
        {
            Debug.Log("Dropped in water!");
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            rabbitBodyAnim.SetBool("DroppedInWater", true);
            levelManagerScript.ReloadLevel();
        }
        else if (bittenByCroc && SceneManager.GetActiveScene().name != "Level10")
        {
            Debug.Log("Bitten by a crocodile!");
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            rabbitBodyAnim.SetBool("BittenByCroc", true);
            levelManagerScript.ReloadLevel();
        }
        else if (bittenByCroc && SceneManager.GetActiveScene().name == "Level10")
        {
            if (childRabbit != null)
            {
                RabbitExtra childRabbitExtraScript = childRabbit.GetComponent<RabbitExtra>();
                childRabbitExtraScript.secured = true;

                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                rabbitBodyAnim.SetBool("BittenByCroc", true);
                rabbitCld.enabled = false;
                gameObject.transform.parent = GameObject.Find("Rabbit Default Parent").transform;
                gameObject.GetComponent<RabbitBehaviour>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Carrot
        if (collision.gameObject.tag == "Carrot")
        {
            if (goldenCarrotScript.carrotCount > 1)
            {
                Destroy(collision.gameObject);
                goldenCarrotScript.carrotCount -= 1;
            }
        }

        // Grounded detection
        else if (collision.gameObject.tag == "LilyPad")
        {
            grounded = true;
            transform.parent = collision.transform;
        }
        else if (collision.gameObject.tag == "Box")
        {
            grounded = true;
            transform.parent = collision.transform;
        }
        else if (collision.gameObject.tag == "Crocodile")
        {
            grounded = true;
        }
        else if (collision.gameObject.layer == 6)
        {
            grounded = true;
            transform.parent = collision.transform;
        }
    }

    private void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

} // End of class
