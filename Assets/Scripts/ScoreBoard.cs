using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int scorePerHit = 20;
    int score = 0;
    Text textScore;

    void Start()
    {
        textScore = GetComponent<Text>();
        textScore.text = score.ToString();
    }

    public void ScoreHit()
    {
        score += scorePerHit;
        textScore.text = score.ToString();
    }
}
