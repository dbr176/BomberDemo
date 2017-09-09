using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Joystick : MonoBehaviour
{
    public Transform center;

    public float maxDist = 3.0f;
    public float sens = 0.01f;
    public float returnVel = 5.0f;
    public float distToMove = 1.5f;

    private float _time = 0.0f;

    public bool InUse
    {
        get; protected set;
    }

    public JoystickDirection Direction
    {
        get
        {
            if (InUse && DistanceToCenter() > distToMove)
            {
                var dir = (transform.position - center.position).normalized;

                var upAngle = Vector3.Distance(dir, Vector2.up);
                var downAngle = Vector3.Distance(dir, Vector2.down);
                var leftAngle = Vector3.Distance(dir, Vector2.left);
                var rightAngle = Vector3.Distance(dir, Vector2.right);

                var angles = new float[] { upAngle, downAngle, leftAngle, rightAngle };
                var minAngle = angles.Min();

                if (minAngle == upAngle)
                    return JoystickDirection.Up;
                if (minAngle == downAngle)
                    return JoystickDirection.Down;
                if (minAngle == leftAngle)
                    return JoystickDirection.Left;
                if (minAngle == rightAngle)
                    return JoystickDirection.Right;
                return JoystickDirection.None;
                
            }

            return JoystickDirection.None;
        }
    }

    #if UNITY_EDITOR
    private Vector3 _prevMousePos = Vector3.zero;
#endif

    private void Start()
    {
        #if UNITY_EDITOR
        _prevMousePos = Camera.main.WorldToScreenPoint(transform.position);
#endif
    }

private bool UpdateAllowed
    {
        get
        {
#if UNITY_ANDROID
            return Input.touchCount == 1;
#elif UNITY_EDITOR
            return Input.GetMouseButton(0);
    #endif
        }
    }

    private float DistanceToCenter()
    {
        return Vector3.Distance(center.position, transform.position);
    }

    private bool _returning = false;
    private Vector3 _retPosition;
    private void Return()
    {
        if (!_returning) return;
        if (DistanceToCenter() < 0.0001f)
            EndJoystickReutrn();
        else
        {
            _time += Time.deltaTime * returnVel;
            transform.position = Vector3.Lerp(_retPosition, center.position, _time);
#if UNITY_EDITOR
            _prevMousePos = Camera.main.WorldToScreenPoint(transform.position);
#endif
        }
    }

    private void BeginJoystickReturn()
    {
        _returning = true;
        _retPosition = transform.position;
    }

    private void EndJoystickReutrn()
    {
        _time = 0;
        _returning = false;
        transform.position = center.position;
    }

    protected virtual void Update()
    {
        Return();
        if (InUse)
        {
            if (!UpdateAllowed)
            {
                BeginJoystickReturn();
                InUse = false;
            }
            else
            {
#if UNITY_ANDROID
                var touch = Input.touches[0];
                var delta = touch.deltaPosition;
                var position = touch.position;
#elif UNITY_EDITOR
                var delta = (Vector2)(Input.mousePosition - _prevMousePos);
                _prevMousePos = Input.mousePosition;
                var position = _prevMousePos;
#endif
                var nextPos = transform.position + (Vector3)delta * sens;
                if (Vector3.Distance(center.position, nextPos) < maxDist)
                    transform.position = nextPos;  
            }
        }
        else
        {
            if (UpdateAllowed)
            {
#if UNITY_ANDROID
                var touch = Input.touches[0];
                var position = touch.position;
#elif UNITY_EDITOR
                var position = Input.mousePosition;
#endif
                var ray = Camera.main.ScreenPointToRay(position);
                var hits = Physics.RaycastAll(ray);

                if (hits.Any(x => x.collider.gameObject == gameObject))
                {
                    InUse = true;
#if UNITY_UNITY_EDITOR
                    prevMousePos = Input.mousePosition;
#endif
                    EndJoystickReutrn();
                    //
                }
            }
        }
    }
}

public enum JoystickDirection
{
    Up, Down, Left, Right, None
}
