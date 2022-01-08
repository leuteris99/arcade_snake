using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText; // the ui score element.
    public Text targetText; // the ui target element. (it shows to the player if he have to eat odd or even numbers).
    public int endScore = 10; // the score the player needs to earn to go to the next level.

    private int score = 0; // current score value of the player.
    private string target; // current target value of the game.
    readonly string printingScoreString = " Points";
    readonly string printingTargetString = "Target: ";

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString() + printingScoreString; // initialize the value in the score ui element.
        target = CreateRandomTarget(); // get a random target.
        targetText.text =  printingTargetString + target; // initialize the value in the target ui element.
    }

    // Adds or substract a point from the scoreboard. Return a bool value. True if it add a point or false if it substract a point. 
    public bool AddPoint(int pointNum){
        if(AddPointChecker(pointNum).Equals(target)){ // if the given number is the same type as the target...
            score++; // add a point the score and update the scoreboard.
            scoreText.text = score.ToString() + printingScoreString;
            if(score >= endScore) // if the score has exceed the points the player needs, then load the next level.
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            return true;
        }
        else
        {
            // if the apple is not the same type as the target substract a point.
            score--;
            scoreText.text = score.ToString() + printingScoreString;
            return false;
        }
    }
    // return if the given number is even or odd.
    private string AddPointChecker(int pointNum)
    {
        if(pointNum % 2 == 0)
        {
            return "evens";
        }
        return "odds";
    }
    // return randomly an "odd" or "even" keyword
    string CreateRandomTarget()
    {
        if (RandomBoolean())
        {
            return "odds";
        }
        return "evens";
    }
    // generate a random bool.
    bool RandomBoolean() {
        {
            if (Random.value >= 0.5)
            {
                return true;
            }
            return false;
        } 
    }
    // give read access to the target value from outside sources.
    public string GetTarget()
    {
        return target;
    }
}
