using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameCharacter target;
    public MoveController horizontalAxis;

    // Unity Events
    private void Awake()
    {
        GameData.currentStats = new GameData.Stats();
        target.OnKill += IncKillScore;
    }
    private void Update()
    {
        // Apply Inputs
        target.InputMove(horizontalAxis.value);

        GameData.currentStats.time += Time.deltaTime;
    }

    // Buttons call methods
    public void InputMelee()
    {
        target.InputMelee();        
    }
    public void InputShoot()
    {
        target.InputShoot();        
    }
    public void InputJump()
    {
        target.InputJump();        
    }

    private void IncKillScore()
    {
        GameData.currentStats.nKilled++;
    }
}
