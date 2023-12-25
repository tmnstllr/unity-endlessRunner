using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    public GameObject disDisplay;
    public GameObject disEndDisplay;
    public int disRun;
    public bool addingDis = false;
    public float disBaseDelay = 0.35f;
    public float disDelay;

    private PlayerMove playerMoveScript; // Reference to the PlayerMove script

    void Start()
    {
        // Find the PlayerMove script on the playerObject
        playerMoveScript = FindAnyObjectByType<PlayerMove>();
    }

    void Update()
    {
        if (!addingDis)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }
    }

    IEnumerator AddingDis()
    {
        disRun += 1;
        disDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "" + disRun;
        disEndDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "" + disRun;

        float currentMoveSpeed = playerMoveScript.GetMoveSpeed();
        disDelay = disBaseDelay * 5 / currentMoveSpeed;

        yield return new WaitForSeconds(disDelay);
        addingDis = false;
    }
}
