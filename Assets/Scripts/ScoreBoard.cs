using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;
    Text textScore;

    void Start()
    {
        textScore = GetComponent<Text>();
        textScore.text = score.ToString();
    }

    public void ScoreKill(int scorePerKill)
    {
        score += scorePerKill;
        textScore.text = score.ToString();
    }
}
