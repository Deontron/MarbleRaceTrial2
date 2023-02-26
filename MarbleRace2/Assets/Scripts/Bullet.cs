using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject ballSpawner;

    private string soldierTag;
    private string enemySoldierTag;

    private void Start()
    {
        KnowYourArmy();
    }
    private void KnowYourArmy()
    {
        switch (gameObject.tag)
        {
            case "RedBullet":
                soldierTag = "RedSoldier";
                enemySoldierTag = "BlueSoldier";

                ballSpawner = GameObject.FindGameObjectWithTag("RedBallSpawner");

                break;

            case "BlueBullet":
                soldierTag = "BlueSoldier";
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
        if (collision.gameObject.CompareTag(soldierTag))
        {
            collision.transform.localScale += Vector3.one / 10;

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(enemySoldierTag))
        {
            if(collision.transform.localScale.x > 0.1f)
            {
                collision.transform.localScale -= Vector3.one / 10;
            }
            else
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("BallPoint"))
        {
            GetBallPoint();
        }
    }
}
