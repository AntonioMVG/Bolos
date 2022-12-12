using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountKegels : MonoBehaviour
{
    private int countKegels;
    private GameManager gameManager;

    private void Start()
    {
        // Access to the persistent GameManager gameObject
        gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kegel"))
        {
            countKegels++;
            gameManager.PlayScore = countKegels;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kegel"))
        {
            countKegels--;
        }
    }
}
