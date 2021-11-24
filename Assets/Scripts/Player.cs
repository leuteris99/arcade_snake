using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // the move speed of the player. You dont need to keep it.
    public Rigidbody2D rb; // making the object interact with physics.(this is the rigidbody of the player) You can change it.

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        // this lines control the player.(For testing purpose only, change it when you implement the correct movement of the snake).
        // this are the distance to move the player.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // this moves the player to the new position.(Change it when you implement the correct movement of the snake)
    }

    // this method is necessary for the apples to interact with the player(DON'T change it)!!!
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Apple")) // if the player pass through an Apple...
        {
            // than destroy the Apple and a point to the scoreboard.
            Destroy(other.gameObject);
            ScoreManager.instance.addPoint();
        }
    }
}
