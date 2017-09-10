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
            case JoystickDirection.Down: player.MoveDown(joystick.Velocity); break;
            case JoystickDirection.Up: player.MoveUp(joystick.Velocity); break;
            case JoystickDirection.Left: player.MoveLeft(joystick.Velocity); break;
            case JoystickDirection.Right: player.MoveRight(joystick.Velocity); break;
            case JoystickDirection.None: player.IsMoving = false; break;
        }
    }
}
