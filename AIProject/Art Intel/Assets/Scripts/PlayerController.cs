using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Animator myAnimator;
    private Rigidbody2D rb;


    #region Movement Variables
    [SerializeField]
    private float movementSpeed = 2f;
    [SerializeField]
    private float verticalSpeed = 2f;

    public GameObject jetPack;
    public GameObject coin;

    [SerializeField]
    private float aiControl = 0f;
    [SerializeField]
    private float aiControlScale = 3f;
    [SerializeField]
    private float aiTimeTillTakeover = 30f;
    [SerializeField]
    private float aiSpeedScale = 1f;
    [SerializeField]
    private float aiSpeedFactor = 0f;
    [SerializeField]
    private float aiSpeedBoost = 10f;


    [SerializeField]
    private bool rightSide = true;

    private Vector2 movementDirection = new Vector2();
    private Vector2 movementDirectionPlayer = new Vector2();
    private Vector2 movementDirectionAI = new Vector2();

    private bool endGame = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called at fixed timestep
    void FixedUpdate()
    {
        if (!endGame)
        {
            rb.AddForce(Vector2.right * (movementSpeed + aiSpeedFactor * aiSpeedBoost) * movementDirection.x);
            rb.AddForce(Vector2.up * (verticalSpeed + aiSpeedFactor * aiSpeedBoost) * movementDirection.y);
        }

    }

    //Update called once per frame
    private void Update()
    {
        if (!endGame)
        {
            float aiSpeed = 0;
            //Increase towards 1 as time goes on up until time till take over happens
            if (Time.time > aiTimeTillTakeover * .6)
            {
                aiControl = 1;

                //Speed up the AI
                //Normalized time since AI has taken over up till 30 seconds past
                float timeSinceTakeover = (Time.time - aiTimeTillTakeover * .6f) / 30;
                aiSpeedFactor = 1 / (1 + Mathf.Pow(timeSinceTakeover / (1 - timeSinceTakeover), -1 * aiSpeedScale));
            }
            else
            {
                float normalizedTime = Time.time / aiTimeTillTakeover;
                aiControl = 1 / (1 + Mathf.Pow(normalizedTime / (1 - normalizedTime), -1 * aiControlScale));
            }

            //aiControl = 1 - Mathf.Pow((float)System.Math.E, Time.time * -1 * aiControlScale);

            //Get player input
            movementDirectionPlayer.x = Input.GetAxis("Horizontal");
            movementDirectionPlayer.y = Input.GetAxis("Vertical");

            //Get AI input
            Vector3 targetDirection = coin.transform.position - transform.position;
            Vector3 targetDirectionNormalized = Vector3.Normalize(targetDirection);

            movementDirectionAI.x = targetDirectionNormalized.x;
            movementDirectionAI.y = targetDirectionNormalized.y;


            //Mix the two based on aiControl (time progression)
            movementDirection.x = (movementDirectionPlayer.x * (1 - aiControl) + movementDirectionAI.x * (aiControl));
            movementDirection.y = (movementDirectionPlayer.y * (1 - aiControl) + movementDirectionAI.y * (aiControl));

            //Get rid of negative vertical movement
            if (movementDirection.y < 0f)
            {
                movementDirection.y = 0f;
            }

            //There is some movement left or right occuring
            if (Mathf.Abs(movementDirection.x) > 0.1f)
            {
                myAnimator.SetBool("Walking", true);

                // Only update movement animation on walking, otherwise dont update so direction is preserved
                myAnimator.SetFloat("HorizontalMovement", movementDirection.x);
                //myAnimator.SetFloat("VerticalMovement", Input.GetAxis("Vertical"));
            }
            else
            {
                myAnimator.SetBool("Walking", false);
            }

            //Check if the player is on the ground
            if (transform.position.y < -1.4f)
            {
                myAnimator.SetBool("Flying", false);
            }
            else
            {
                myAnimator.SetBool("Flying", true);
            }


            //See if jet pack needs to be moved to other side
            if (myAnimator.GetFloat("HorizontalMovement") < 0)
            {
                //Check to see if needing to change
                if (rightSide == false)
                    jetPack.transform.localPosition = new Vector2(0.28f, jetPack.transform.localPosition.y);
                {
                }
                rightSide = true;

            }
            else if (myAnimator.GetFloat("HorizontalMovement") > 0)
            {
                //Check to see if needing to change
                if (rightSide == true)
                {
                    jetPack.transform.localPosition = new Vector2(-0.256f, jetPack.transform.localPosition.y);
                }
                rightSide = false;
            }
        }
    }

    public void stopPlayer() {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        endGame = true;
        myAnimator.SetBool("Walking", false);

    }

}
