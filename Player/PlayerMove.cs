using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float baseMoveSpeed = 5f;
    public float currentMoveSpeed;
    public float leftRightSpeed = 5f;
    public float leftRightSpeedMultiplier = 1.25f;
    public float speedIncrement = 0.075f;
    static public bool canMove = false;
    public bool isJumping = false;
    public bool comingDown = false;
    public float baseJumpDuration = 0.475f;
    public float jumpDuration;

    public float animationSpeed = 1f;
    public float animationSpeedMultiplier = 0.1f;
    public float verticalJumpSpeed = 3f; // Separate variable for vertical jump speed
    public GameObject playerObject;

    private Animator playerAnimator;

    void Start()
    {
        currentMoveSpeed = baseMoveSpeed;
        playerAnimator = playerObject.GetComponent<Animator>();
        StartCoroutine(SpeedIncrement());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * currentMoveSpeed, Space.World);

        if (canMove)
        {
            if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
            {
                if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
                }
            }

            if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
            {
                if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);
                }
            }

            if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.Space)))
            {
                if (!isJumping)
                {
                    isJumping = true;
                    playerAnimator.speed = animationSpeed;
                    playerAnimator.Play("Jump");
                    StartCoroutine(JumpSequence());
                }
            }
        }
        if (isJumping)
        {
            if (!comingDown)
            {
                transform.Translate(Vector3.up * Time.deltaTime * verticalJumpSpeed, Space.World);
            }
            else if (comingDown)
            {
                transform.Translate(Vector3.up * Time.deltaTime * -verticalJumpSpeed, Space.World);
            }
        }
    }

    IEnumerator JumpSequence()
    {
        jumpDuration = baseJumpDuration / animationSpeed;
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(jumpDuration);
        comingDown = true;
        yield return new WaitForSeconds(jumpDuration + 0.005f);

        Vector3 endPos = transform.position;
        transform.position = new Vector3(endPos.x, startPos.y, endPos.z);

        float yposDiff = endPos.y - startPos.y;
        print("Jump ypos diff: " + yposDiff);

        isJumping = false;
        comingDown = false;
        if (canMove)
        {
            playerObject.GetComponent<Animator>().Play("Standard Run");
        }
    }

    IEnumerator SpeedIncrement()
    {
        yield return new WaitForSeconds(5);
        while (canMove)
        {
            yield return new WaitForSeconds(1);
            currentMoveSpeed += speedIncrement;
            leftRightSpeed += speedIncrement * leftRightSpeedMultiplier;
            animationSpeed += speedIncrement * animationSpeedMultiplier;
            playerAnimator.speed = animationSpeed;
        }
    }

    // Getter and setter functions
    public float GetMoveSpeed() 
    {
        return currentMoveSpeed;
    }

    public void SetMoveSpeed(float newMoveSpeed)
    {
        currentMoveSpeed = newMoveSpeed;
    }

    public float GetLeftRightSpeed() 
    {
        return leftRightSpeed;
    }

    public void SetLeftRightSpeed(float newLeftRightSpeed)
    {
        leftRightSpeed = newLeftRightSpeed;
    }

    public float GetLeftRightSpeedMultiplier() 
    {
        return leftRightSpeedMultiplier;
    }
    

    public float GetAnimationSpeed()
    {
        return animationSpeed;
    }

    public void SetAnimationSpeed(float newAnimationSpeed)
    {
        animationSpeed = newAnimationSpeed;
    }

    public float GetAnimationSpeedMultiplier()
    {
        return animationSpeedMultiplier;
    }
}