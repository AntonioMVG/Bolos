using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBoxes : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        // Access to the persistent GameManager gameObject
        gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            gameManager.Box++;
            gameManager.PlayScore = gameManager.Box;

            gameManager.GameScore += gameManager.PlayScore;
            Debug.Log(gameManager.PlayScore);
        }
    }
}
