using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalloonGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreCounter;

    public void UpdateScore(int newScore)
    {
        scoreCounter.text = newScore.ToString();
    }
}
