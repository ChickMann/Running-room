using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject result;
    [SerializeField] private TextMeshProUGUI resultText;
 
    internal void ShowResult()
    {
        scoreText.gameObject.SetActive(false);
        result.SetActive(true);
        resultText.text = $"{score}$";
    }
    /// <summary>
    /// Increase Score 
    /// </summary>
    /// <param name="value"> value increase </param>
    internal void UpdateScore(int value)
    {
        scoreText.gameObject.SetActive(true);
        score += value;
        ShowScore();
    }
    private void ShowScore()
    {
        scoreText.text = $"{score}$";
    }

}
