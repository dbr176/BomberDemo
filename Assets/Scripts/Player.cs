using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 4.0f;

    private float _time;
    private bool _isMoving;
    private Vector3 _moveFinish;
    private Vector3 _moveStart;

    protected virtual bool Move(Vector3 direction, float dist)
    {
        if (_isMoving) return false;

        var hit = Physics2D.Raycast(transform.position, direction, dist);

        if (hit.collider != null)
            return false;

        _isMoving = true;
        _moveFinish = direction + transform.position;
        _moveStart = transform.position;
        _time = 0;

        return true;
    } 

    protected virtual void Update()
    {
        if (_time > 1.0)
        {
            _isMoving = false;
            transform.position = _moveFinish;
        }
        if (_isMoving)
        {
            var pos = Vector3.Lerp(_moveStart, _moveFinish, _time);
            transform.position = pos;
            _time += Time.deltaTime * velocity;
        }
    }

    public bool MoveLeft()
    {
        return Move(Vector2.left, 1.0f);
    }

    public bool MoveRight()
    {
        return Move(Vector2.right, 1.0f);
    }

    public bool MoveUp()
    {
        return Move(Vector2.up, 1.0f);
    }

    public bool MoveDown()
    {
        return Move(Vector2.down, 1.0f);
    }

}
