using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeBallSpawner : MonoBehaviour
{
    public GameObject[] prizeBallPoints;

    private int randomNumber;

    void Start()
    {
        StartCoroutine(SpawnPrizeBall());
    }

    IEnumerator SpawnPrizeBall()
    {
        while (true)
        {
            randomNumber = Random.Range(0, prizeBallPoints.Length);

            if (prizeBallPoints[randomNumber].GetComponent<PrizeBall>().isOpen)
            {
                randomNumber = Random.Range(0, prizeBallPoints.Length);
            }
            else
            {
                prizeBallPoints[randomNumber].SetActive(true);
                prizeBallPoints[randomNumber].GetComponent<PrizeBall>().isOpen = true;
            }

            yield return new WaitForSeconds(10);
        }
    }
}
