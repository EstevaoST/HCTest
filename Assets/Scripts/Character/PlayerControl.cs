using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameCharacter target;

    public MoveController horizontalAxis;

    // Unity Events
    private void Update()
    {
        // Apply Inputs
        target.InputMove(horizontalAxis.value);
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
}
