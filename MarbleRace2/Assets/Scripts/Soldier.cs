using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private GameObject ballSpawner;

    private string enemySoldierTag;

    private float collisionRadius;

    private void Start()
    {
        KnowYourArmy();
    }
    private void KnowYourArmy()
    {
        switch (gameObject.tag)
        {
            case "RedSoldier":
                enemySoldierTag = "BlueSoldier";
                ballSpawner = GameObject.FindGameObjectWithTag("RedBallSpawner");

                break;

            case "BlueSoldier":
                enemySoldierTag = "RedSoldier";
                ballSpawner = GameObject.FindGameObjectWithTag("BlueBallSpawner");

                break;
        }
    }

    private void GetBallPoint()
    {
        ballSpawner.GetComponent<BallSpawner>().SpawnBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionRadius = collision.transform.localScale.x;

        if (collision.gameObject.CompareTag(enemySoldierTag) && gameObject.CompareTag("BlueSoldier"))
        {
            if (transform.localScale.x > collisionRadius)
            {
                transform.localScale = new Vector2(transform.localScale.x - collisionRadius, transform.localScale.y - collisionRadius);

                Destroy(collision.gameObject);

                if (transform.localScale.x < 0.1f)
                {
                    Destroy(gameObject);
                }
            }
            else if (transform.localScale.x < collisionRadius)
            {
                collision.transform.localScale = new Vector2(collisionRadius - transform.localScale.x, collisionRadius - transform.localScale.y);

                if (collision.transform.localScale.x < 0.1f)
                {
                    Destroy(collision.gameObject);
                }

                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("BallPoint"))
        {
            GetBallPoint();
        }
    }
}

