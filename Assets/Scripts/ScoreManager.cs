using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText; // the ui score element.

    int score = 0;
    string printingScoreString = " Points";

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString() + printingScoreString;
    }

    public void addPoint(){
        score += 1;
        scoreText.text = score.ToString() + printingScoreString;
    }
}
