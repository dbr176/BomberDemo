using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class TestInput : MonoBehaviour
{
    Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
            _player.MoveDown();
        if (Input.GetKeyUp(KeyCode.UpArrow))
            _player.MoveUp();
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            _player.MoveLeft();
        if (Input.GetKeyUp(KeyCode.RightArrow))
            _player.MoveRight();
    }
}
