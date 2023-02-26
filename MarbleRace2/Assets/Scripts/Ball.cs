using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameObject spawner;
    private GameObject cannon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "OrangeBlock":
                cannon.GetComponent<Cannon>().Fire();
                collision.GetComponent<Animator>().SetTrigger("collision");

                transform.position = spawner.transform.position;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;

            case "PurpleBlock":
                cannon.GetComponent<Cannon>().EnlargeSoldiers();
                collision.GetComponent<Animator>().SetTrigger("collision");

                transform.position = spawner.transform.position;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;

            case "GreyBlock":
                cannon.GetComponent<Cannon>().SpawnSoldier();
                collision.GetComponent<Animator>().SetTrigger("collision");

                transform.position = spawner.transform.position;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
        }
    }

    public void GetYourSpawner(GameObject _spawner, GameObject _cannon)
    {
        spawner = _spawner;
        cannon = _cannon;
    }
}
