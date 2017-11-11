using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public int score;
    private Text myText;
    void Start()
    {
        myText = GetComponent<Text>();
        myText.text = "0";
    }

    public void UpdateScore(int points)
    {
        Debug.Log("SCore Called");
        score += points;
        myText.text = score.ToString();

    }

    void ResetScore()
    {
        score = 0;
        myText.text = score.ToString();
    }
}
