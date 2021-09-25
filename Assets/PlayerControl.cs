using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    // Declaring variables
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public float speed = 10.0f; //Speed
    public float yBoundary = 9.0f; //Camera Limit
    private Rigidbody2D rb2d;
    public int score;
    private ContactPoint2D lastContactPoint; //contact point with ball
    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Updating speed from player control
        Vector2 velocity = rb2d.velocity;
        if (Input.GetKey(upButton)) {
            velocity.y = speed;
        } 
        else if (Input.GetKey(downButton)) {
            velocity.y = -speed;
        } 
        else {
            velocity.y = 0.0f;
        }
        rb2d.velocity = velocity;

        // Keeping player in camera
        Vector3 position = transform.position;
        if (position.y > yBoundary){
            position.y = yBoundary;
        } else if (position.y < -yBoundary){
            position.y = -yBoundary;
        }
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name.Equals("Ball")){
            lastContactPoint = other.GetContact(0);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        trajectoryOrigin = transform.position;
    }

    public void IncrementScore(){
        score++;
    }
    
    public void ResetScore(){
        score = 0;
    }

    //Get Function
    public int Score{
        get { return score; }
    }

    public ContactPoint2D LastContactPoint{
        get { return lastContactPoint; }
    }

    public Vector2 TrajectoryOrigin {
        get { return trajectoryOrigin; }
    }
}
