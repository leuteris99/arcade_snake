using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb; //this is the rigidbody of the player.

    private Vector2 _gridMoveDirection; // the direction that the head of the snake faces.
    private Vector2 _gridPosition; // the position of the head of the snake.
    private float _gridMoveTimer; // the time that has passed since the last repositioning of the snake.
    private float _gridMoveTimerMax; // the time that needs to pass between every reposition of the snake.
    public float _stepDistance = 0.6f; // the distance of the snakes repositioning.
    public GameObject restartMenu; // reference to the restart menu
    public GameObject pauseMenu; // reference to the pause menu
    public Transform bodyPrefab; // the transform of the game object that will spawn as the tail of the snake.
    private bool _isMoving; // checking if the snake is allowed to move.
    private List<Transform> _segmentsList; // list with the transforms of the head and all the body segments of the body.

    private void Awake()
    {
        _isMoving = true; // the snake starts able to move.
        _gridPosition = new Vector2Int(-3, 0); // starting position of the head.
        _gridMoveTimerMax = 0.6f; // set the repositioning tick to happen every 0.6 secs.
        _gridMoveTimer = _gridMoveTimerMax; // immidiatly start the game with a reposition of the head on his starting coordinates.
        _gridMoveDirection = new Vector2(_stepDistance, 0); // the starting direction that the head faces.
    }

    private void Start()
    {
        _segmentsList = new List<Transform>(); // init the list
        _segmentsList.Add(this.transform); // adding the head transform to it.
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInput();
        HandleGridMovement();
    }

    /*
        Handles the input from the user's keyboard.
    */
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) // setting the direction of the head to face up.
        {
            if(_gridMoveDirection.y != -_stepDistance)
            {
                _gridMoveDirection.x = 0;
                _gridMoveDirection.y = _stepDistance;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) // setting the direction of the head to face down.
        {
            if (_gridMoveDirection.y != _stepDistance)
            {
                _gridMoveDirection.x = 0;
                _gridMoveDirection.y = -_stepDistance;
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) // setting the direction of the head to face left.
        {
            if (_gridMoveDirection.x != _stepDistance)
            {
                _gridMoveDirection.x = -_stepDistance;
                _gridMoveDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) // setting the direction of the head to face right.
        {
            if (_gridMoveDirection.x != -_stepDistance)
            {
                _gridMoveDirection.x = _stepDistance;
                _gridMoveDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // trigger the pause menu
        {
            // if the pause menu is hidden then hide the menu and make the player move.
            if (pauseMenu.activeInHierarchy && !restartMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
                _isMoving = true;
            }
            else if (!restartMenu.activeInHierarchy)
            {
                // else if the menu hidden (the game is not paused) then stop the player's movement (freeze the game) and show the pause menu.
                _isMoving = false;
                pauseMenu.SetActive(true);
            }
        }
    }

    /*
        It moves the snake.
        the movement is not linear. Instead it happens in ticks.
     */
    private void HandleGridMovement()
    {
        if (_isMoving) // move only if you are allowed to.
        {
            _gridMoveTimer += Time.deltaTime; // adding the time passed since the last frame, updates the time since the last tick.
            if (_gridMoveTimer >= _gridMoveTimerMax) // if the time since the last tick passed the time that every tick should happen then move the player.
            {
                for (int i = _segmentsList.Count - 1; i > 0; i--) // for each segment of the snake give it the position and orientation of  its "front" segment.
                {
                    _segmentsList[i].position = _segmentsList[i - 1].position;
                    _segmentsList[i].rotation = _segmentsList[i - 1].rotation;
                    
                }
                _gridPosition += _gridMoveDirection;
                _gridMoveTimer -= _gridMoveTimerMax; // since the tick happend we substract the time of a tick so that the next tick could be triggered.
                // update the the position and rotation of the head of the snake.
                transform.position = new Vector3(_gridPosition.x, _gridPosition.y);
                transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(_gridMoveDirection));
            }
        }
    }
    // return the angle of a vector2 as a float.
    private float GetAngleFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }

    // this method is necessary for the apples to interact with the player. Executes when the player hit a trigger.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Apple")) // if the player pass through an Apple...
        {
            // get the child text object of the apple and store it's value. 
            GameObject child = other.transform.GetChild(0).gameObject;
            string text = child.GetComponent<TextMesh>().text;
            // than destroy the Apple and a point to the scoreboard.
            Destroy(other.gameObject); // destroy the apple.
            if (ScoreManager.instance.AddPoint(int.Parse(text))) // add or substract one point from the scoreboard than return true if it added a point.
            {
                // Grow the snake if a point has been added.
                Grow();
            }
        } 
        if (other.CompareTag("Body")) // if the player pass through an Body segment...
        {
            // stop the game and show the restart menu.
            _isMoving = false;
            restartMenu.SetActive(true);
        }
    }

    // Executes when the player hit an Object.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Barrier")) // if you hit a barrier or a wall stop the game and show the restart menu.
        {
            _isMoving = false;
            restartMenu.SetActive(true);
        }
    }

    // this method changes the value isMoving, effectivly give the ability to an outside event to freeze or unfreeze the game.
    public void SetIsMoving(bool val)
    {
        _isMoving = val;
    }

    // Grows the tail of the snake.
    private void Grow()
    {
        Transform segment = Instantiate(this.bodyPrefab); // create a new body segment object on the game.
        if(_segmentsList.Count == 1) // if this segment is the first after the head then change it's tag. this is needed cause the segments touch each other. Else the player will lose when he eat the first apple.
        {
            segment.gameObject.tag = "FirstBody";
        }
        // give to the segment the position and rotation of the segment in front of it.
        segment.position = _segmentsList[_segmentsList.Count - 1].position;
        segment.rotation = _segmentsList[_segmentsList.Count - 1].rotation;

        _segmentsList.Add(segment); // add the segment to the list.
    }
}
