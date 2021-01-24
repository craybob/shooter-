using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    
    public Text highScoreText;
    public int score;
    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();



    }

    // Update is called once per frame
    void Update()
    {
        scoreSystem();

    }

    void scoreSystem()
    {
        

        scoreText.text = "" + score;

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "" + score;
        }
    }

}
