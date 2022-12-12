using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playNumber = 0;

    private int playScore;
    private int gameScore;

    public int PlayScore { get => playScore; set => playScore = value; }
    public int GameScore { get => gameScore; set => gameScore = value; }

    private void Awake()
    {
        int instancesNumber = FindObjectsOfType<GameManager>().Length;
        if(instancesNumber != 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
