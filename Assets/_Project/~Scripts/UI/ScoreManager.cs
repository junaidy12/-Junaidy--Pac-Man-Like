using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    int maxScore;
    int score;

    private void Start()
    {
        UpdateUI();
    }

    public void SetMaxScore(int value)
    {
        maxScore = value;
        UpdateUI();
    }

    public void AddScore()
    {
        score++;
        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = $"Score : {score}/{maxScore}";
    }

}
