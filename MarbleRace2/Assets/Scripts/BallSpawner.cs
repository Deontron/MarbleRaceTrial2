using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallSpawner : MonoBehaviour
{
    public TMP_Text ballAmountText;
    private int ballAmount;

    public GameObject cannon;

    public GameObject ball;
    private GameObject spawnedBall;

    private float positionLimit = 0.8f;
    private float movementSpeed = 2;
    private bool isRising = true;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(StartTheGame());
    }

    void Update()
    {
        SpawnerMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    IEnumerator StartTheGame()
    {
        yield return new WaitForSeconds(0.2f);
        SpawnBall();
    }

    public void SpawnBall(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            animator.SetTrigger("spawnBall");

            spawnedBall = Instantiate(ball, transform.position, transform.rotation);

            spawnedBall.GetComponent<Ball>().GetYourSpawner(gameObject, cannon);

            ballAmount++;
            ballAmountText.text = ballAmount.ToString();
        }
    }

    private void SpawnerMovement()
    {
        if (isRising)
        {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

            if (transform.localPosition.x >= positionLimit)
            {
                isRising = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);

            if (transform.localPosition.x <= -positionLimit)
            {
                isRising = true;
            }
        }
    }
}
