using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    public Text scoreTime, scoreKills, finalScore;

    // Start is called before the first frame update
    void Start()
    {
        if(GameData.currentStats != null)
        {
            scoreTime.text = (int)(GameData.currentStats.time) + " seconds";
            scoreKills.text = GameData.currentStats.nKilled.ToString();
            finalScore.text = "Final Score: " + GameData.currentStats.GetScore();
        }   
    }
}
