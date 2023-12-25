using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSlowDown : MonoBehaviour
{
    public AudioSource plantFX;
    public float remainingSpeedFactor = 0.7f; // Slowdown = 1 - remainingSpeedFactor

    private PlayerMove playerMoveScript;

    private float currentMoveSpeed;
    private float newMoveSpeed;

    private float animationSpeed;
    private float newAnimationSpeed;
    private float animationSpeedMultiplier;

    private float leftRightSpeed;
    private float newLeftRightSpeed;
    private float leftRightSpeedMultiplier;

    void Start()
    {
        playerMoveScript = FindAnyObjectByType<PlayerMove>();
    }

    void OnTriggerEnter(Collider other)
    {
        plantFX.Play();

        currentMoveSpeed = playerMoveScript.GetMoveSpeed();
        newMoveSpeed = Mathf.Max(currentMoveSpeed * remainingSpeedFactor, 5f); 
        playerMoveScript.SetMoveSpeed(newMoveSpeed);

        // 1.3-((8-6.4)*0.1) = 1.14
        animationSpeed = playerMoveScript.GetAnimationSpeed();
        animationSpeedMultiplier = playerMoveScript.GetAnimationSpeedMultiplier();
        newAnimationSpeed = Mathf.Max(animationSpeed - ((currentMoveSpeed - newMoveSpeed) * animationSpeedMultiplier), 1f);
        playerMoveScript.SetAnimationSpeed(newAnimationSpeed);

        // 8.75-((8-6.4)*1.25) = 6.75 
        leftRightSpeed = playerMoveScript.GetLeftRightSpeed();
        leftRightSpeedMultiplier = playerMoveScript.GetLeftRightSpeedMultiplier();
        newLeftRightSpeed = Mathf.Max(leftRightSpeed - ((currentMoveSpeed - newMoveSpeed) * leftRightSpeedMultiplier), 5f);
        playerMoveScript.SetLeftRightSpeed(newLeftRightSpeed);

        this.gameObject.SetActive(false);
    }
}
