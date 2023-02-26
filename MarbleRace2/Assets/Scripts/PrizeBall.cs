using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeBall : MonoBehaviour
{
    public bool isOpen = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOpen = false;

        gameObject.SetActive(false);

        Destroy(collision.gameObject);
    }
}
