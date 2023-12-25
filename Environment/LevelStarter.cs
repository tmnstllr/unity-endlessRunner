using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    public GameObject[] countDown;
    public GameObject fadeIn;
    public AudioSource readyFX;
    public AudioSource goFX;

    void Start()
    {
        StartCoroutine(CountSequence());
    }

    IEnumerator CountSequence()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 4; i++)
        {
            if (i < 3)
            {
                countDown[i].SetActive(true);
                readyFX.Play();
                yield return new WaitForSeconds(1f);
            }
            countDown[i].SetActive(true);
            goFX.Play();
        }
        yield return new WaitForSeconds(0.125f);
        PlayerMove.canMove = true;
    }
}
