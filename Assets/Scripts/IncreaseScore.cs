using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseScore : MonoBehaviour
{
    [SerializeField] private int score;

    public void increaseScore()
    {
        GameManager.instance.playerScore += score;
    }
}
