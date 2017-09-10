using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    public Player player;
    public Joystick joystick;

    private void Update()
    {
        player.IsMoving = true;
        switch(joystick.Direction)
        {
            case JoystickDirection.Down: player.MoveDown(); break;
            case JoystickDirection.Up: player.MoveUp(); break;
            case JoystickDirection.Left: player.MoveLeft(); break;
            case JoystickDirection.Right: player.MoveRight(); break;
            case JoystickDirection.None: player.IsMoving = false; break;
        }
    }
}
