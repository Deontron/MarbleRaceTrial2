using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject firePoint;

    public GameObject bullet;
    public GameObject soldier;

    private GameObject spawnedBullet;
    private GameObject spawnedSoldier;

    private float positionLimit = 4;
    private float movementSpeed = 2;
    private bool isRising = true;

    void Update()
    {
        CannonMovement();
    }

    private void CannonMovement()
    {
        if (isRising)
        {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

            if (transform.localPosition.y >= positionLimit)
            {
                isRising = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);

            if (transform.localPosition.y <= -4)
            {
                isRising = true;
            }
        }
    }

    public void Fire(int amount = 2)
    {
        for (int i = 0; i < amount; i++)
        {
            spawnedBullet = Instantiate(bullet, firePoint.transform.position, Quaternion.identity);

            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.forward * 100);
        }
    }

    public void SpawnSoldier()
    {
        spawnedSoldier = Instantiate(soldier, firePoint.transform.position, Quaternion.identity);

        spawnedSoldier.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.forward * 20);
    }

    public void EnlargeSoldiers()
    {
        if (spawnedSoldier != null)
        {
            GameObject[] soldiers = GameObject.FindGameObjectsWithTag(spawnedSoldier.tag);

            foreach (var soldier in soldiers)
            {
                soldier.transform.localScale += (Vector3.one / 5);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Barrier"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.forward * 100);
        }
    }
}
