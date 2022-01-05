using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText; // the ui score element.
    public Text targetText;
    public int endScore = 15;

    private int score = 0;
    private string target;
    readonly string printingScoreString = " Points";
    readonly string printingTargetString = "Target: ";

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString() + printingScoreString;
        target = GetRandomTarget();
        targetText.text =  printingTargetString + target;
    }

    public void AddPoint(int pointNum){
        if(AddPointChecker(pointNum).Equals(target)){
            score += 1;
            scoreText.text = score.ToString() + printingScoreString;
            if(score >= endScore)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private string AddPointChecker(int pointNum)
    {
        if(pointNum % 2 == 0)
        {
            return "evens";
        }
        return "odds";
    }

    string GetRandomTarget()
    {
        if (RandomBoolean())
        {
            return "odds";
        }
        return "evens";
    }

    bool RandomBoolean() {
        {
            if (Random.value >= 0.5)
            {
                return true;
            }
            return false;
        } 
    }
}
