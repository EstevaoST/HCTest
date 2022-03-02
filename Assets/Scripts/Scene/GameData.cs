using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public enum GameScenes
    {
        MainMenu = 0, GameScene = 1, VictoryScreen = 2, GameOverScreen = 3, AdScreen = 4, StatScreen = 5
    }

    public class Stats
    {
        public float time = 0;
        public int nKilled = 0;

        private const int PERFECT_TIME = 45;

        public int GetScore()
        {
            int score = 0;
            if (time > PERFECT_TIME)
                score += 1000 / Mathf.CeilToInt(time - PERFECT_TIME);
            else
                score += 1000;

            score += 150 * nKilled;

            return score;
        }
    }
    public static Stats currentStats = null;
}
