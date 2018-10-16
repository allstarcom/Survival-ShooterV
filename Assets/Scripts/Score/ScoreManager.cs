using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {



    public static int score;
    public Text highscore;
    public Text scoreText;                      


    void Start()
    {
        score = 0;      
        highscore.text = PlayerPrefs.GetInt("Highscore",0).ToString();

    }


    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscore.text = score.ToString();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("Highscore");
        highscore.text = "0";
    }


}

