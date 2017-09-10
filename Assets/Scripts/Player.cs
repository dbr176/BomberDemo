using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 0.01f;

    private bool _isMoving;
    private Vector3 _moveFinish;
    private Vector3 _moveStart;
    private Vector3 _direction;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
        }
    }

    private Vector3 Round(Vector3 v)
    {
        return new Vector3(
            Mathf.Round(v.x),
            Mathf.Round(v.y),
            Mathf.Round(v.z));
    }

    protected virtual bool Move(Vector3 direction, float dist)
    {
        //if (_isMoving) return false;

        var moveOnLine = Vector3.Distance(transform.position, _moveFinish) < 0.2;
        var onTileCenter = Vector3.Distance(Round(transform.position), transform.position) < 0.05;
        var parallelDirection = Vector3.Angle(_direction, -direction) < 0.01;

        if (onTileCenter && !_isMoving)
            transform.position = Round(transform.position);

        if ((parallelDirection && moveOnLine) || onTileCenter)
        {
            var hit = Physics2D.Raycast(transform.position, direction, dist);

            if (hit.collider != null)
            {
                _isMoving = false;
                return false;
            }

            _direction = direction;
            _moveFinish = Round(direction + transform.position);
            _moveStart = transform.position;

            return true;
        }

        return false;
    } 

    protected virtual void Update()
    {
        if (IsMoving)
            transform.position += _direction * velocity;
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
