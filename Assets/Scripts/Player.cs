using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 0.01f;
    public float minRaycastDist = 1.0f;

    private bool _isMoving;
    private Vector3 _moveFinish;
    private Vector3 _direction;

    private float _moveOnLineMaxDist = 0.2f;
    private float _minDistToCenter = 0.05f;
    private float _parallelDirsAngle = 0.01f;

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
        var moveOnLine = Vector3.Distance(transform.position, _moveFinish) < _moveOnLineMaxDist;
        var onTileCenter = Vector3.Distance(Round(transform.position), transform.position) < _minDistToCenter;
        var parallelDirection = Vector3.Angle(_direction, -direction) < _parallelDirsAngle;

        if (onTileCenter && !_isMoving)
            transform.position = Round(transform.position);

        if ((parallelDirection && moveOnLine) || onTileCenter)
        {
            var hit = Physics2D.Raycast(transform.position, direction, Mathf.Max(minRaycastDist, dist));

            if (hit.collider != null)
            {
                _isMoving = false;
                return false;
            }

            _direction = direction;
            _moveFinish = Round(direction + transform.position);

            return true;
        }

        return false;
    } 

    protected virtual void Update()
    {
        if (IsMoving)
            transform.position += _direction * velocity;
    }

    public bool MoveLeft(float v = 1.0f)
    {
        return Move(Vector2.left, v);
    }

    public bool MoveRight(float v = 1.0f)
    {
        return Move(Vector2.right, v);
    }

    public bool MoveUp(float v = 1.0f)
    {
        return Move(Vector2.up, v);
    }

    public bool MoveDown(float v = 1.0f)
    {
        return Move(Vector2.down, v);
    }

}
