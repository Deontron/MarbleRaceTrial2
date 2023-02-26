using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Base : MonoBehaviour
{
    public GameObject cannon;
    public GameObject enemyBase;

    public Image healthBar;
    public TMP_Text healthText;

    public Image ultiBar;
    public TMP_Text ultiBarText;
    public GameObject ultiAnim;

    private float health = 100;
    private float ulti = 0;

    private bool usingUlti = false;
    private int ultiAmount;

    private string bulletTag;
    private string soldierTag;
    private GameObject ballSpawner;

    private string animTrigger;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        KnowYourEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(bulletTag) || collision.CompareTag(soldierTag))
        {
            animator.SetTrigger(animTrigger);

            Destroy(collision.gameObject);

            GetDamage(collision.transform.localScale.x);
        }
    }

    private void KnowYourEnemy()
    {
        switch (gameObject.tag)
        {
            case "RedBase":
                bulletTag = "BlueBullet";
                soldierTag = "BlueSoldier";
                ballSpawner = GameObject.FindGameObjectWithTag("RedBallSpawner");
                animTrigger = "redDamage";

                break;

            case "BlueBase":
                bulletTag = "RedBullet";
                soldierTag = "RedSoldier";
                ballSpawner = GameObject.FindGameObjectWithTag("BlueBallSpawner");
                animTrigger = "blueDamage";

                break;
        }
    }

    private void GetDamage(float damage)
    {
        if (!usingUlti)
        {
            health -= damage;
            healthBar.fillAmount = health / 100;
            if(health <= 0)
            {
                health = 0;
                Time.timeScale = 0;
            }
            healthText.text = "100 / " + health.ToString("f1");

            ulti += damage;
            if (ulti >= 10)
            {
                ulti = 10;
                ultiBar.fillAmount = ulti / 10;
                ultiBarText.text = "10 / " + ulti.ToString("f1");

                StartCoroutine(UseUlti());
            }

            ultiBar.fillAmount = ulti / 10;
            ultiBarText.text = "10 / " + ulti.ToString("f1");
        }
    }

    IEnumerator UseUlti()
    {
        ultiAnim.SetActive(true);
        usingUlti = true;

        if (enemyBase.GetComponent<Base>().health > health)
        {
            ultiAmount = 120;
            ballSpawner.GetComponent<BallSpawner>().SpawnBall(5);
        }
        else
        {
            ultiAmount = 30;
        }

        for (int i = 0; i < ultiAmount; i++)
        {
            cannon.GetComponent<Cannon>().Fire();

            if (i % 10 == 0)
            {
                cannon.GetComponent<Cannon>().SpawnSoldier();
            }

            yield return new WaitForSeconds(0.1f);
        }
        ulti = 0;
        ultiBar.fillAmount = ulti / 10;
        ultiBarText.text = "10 / " + ulti.ToString("f1");

        usingUlti = false;
        ultiAnim.SetActive(false);
    }
}
