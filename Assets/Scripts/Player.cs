using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // public float moveSpeed = 5f; // the move speed of the player. You dont need to keep it.
    public Rigidbody2D rb; // making the object interact with physics.(this is the rigidbody of the player) You can change it.

    private Vector2 _gridMoveDirection;
    private Vector2 _gridPosition;
    private float _gridMoveTimer;
    private float _gridMoveTimerMax;
    public float _stepDistance = 0.6f;
    public GameObject restartMenu;
    private bool _isMoving;

    private void Awake()
    {
        //_stepDistance = 0.6f;
        _isMoving = true;
        _gridPosition = new Vector2Int(-3, 0);
        _gridMoveTimerMax = 0.4f;
        _gridMoveTimer = _gridMoveTimerMax;
        _gridMoveDirection = new Vector2(_stepDistance, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInput();
        HandleGridMovement();
        // https://www.youtube.com/watch?v=0vFucqblH-g
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(_gridMoveDirection.y != -_stepDistance)
            {
                _gridMoveDirection.x = 0;
                _gridMoveDirection.y = _stepDistance;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_gridMoveDirection.y != _stepDistance)
            {
                _gridMoveDirection.x = 0;
                _gridMoveDirection.y = -_stepDistance;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_gridMoveDirection.x != _stepDistance)
            {
                _gridMoveDirection.x = -_stepDistance;
                _gridMoveDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_gridMoveDirection.x != -_stepDistance)
            {
                _gridMoveDirection.x = _stepDistance;
                _gridMoveDirection.y = 0;
            }
        }
    }

    private void HandleGridMovement()
    {
        if (_isMoving)
        {
            _gridMoveTimer += Time.deltaTime;
            if (_gridMoveTimer >= _gridMoveTimerMax)
            {
                _gridPosition += _gridMoveDirection;
                _gridMoveTimer -= _gridMoveTimerMax;
                transform.position = new Vector3(_gridPosition.x, _gridPosition.y);
                transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(_gridMoveDirection));
            }
        }
    }

    private float GetAngleFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }

    // this method is necessary for the apples to interact with the player(DON'T change it)!!!
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Apple")) // if the player pass through an Apple...
        {
            GameObject child = other.transform.GetChild(0).gameObject;
            string text = child.GetComponent<TextMesh>().text;
            // than destroy the Apple and a point to the scoreboard.
            Destroy(other.gameObject);
            ScoreManager.instance.AddPoint(int.Parse(text));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Barrier"))
        {
            _isMoving = false;
            restartMenu.SetActive(true);
        }
    }
}
