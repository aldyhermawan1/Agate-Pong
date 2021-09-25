using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    //Declaring Variables
    private Rigidbody2D rb2d;
    // public float xInitialForce;
    // public float yInitialForce;
    public float initialForce;
    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnCollisionExit2D(Collision2D collision){
    //     trajectoryOrigin = transform.position;
    // }


    void ResetBall(){
        transform.position = Vector2.zero;
        rb2d.velocity = Vector2.zero;
    }

    void PushBall(){
        // float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        // float x = Mathf.Sqrt(xInitialForce*xInitialForce - yInitialForce*yInitialForce);
        
        float angle = Random.Range(60, 120);
        float rad = angle * Mathf.PI / 180;
        float x = Mathf.Sin(rad) * initialForce;
        float y = Mathf.Cos(rad) * initialForce;

        float randomDirection = Random.Range(0,2);
        if (randomDirection < 1){
            // rb2d.AddForce(new Vector2(-xInitialForce, yRandomInitialForce)); //to left
            rb2d.AddForce(new Vector2(-x, y));
        } else {
            // rb2d.AddForce(new Vector2(xInitialForce, yRandomInitialForce)); //to right
            rb2d.AddForce(new Vector2(x, y));
        }
    }

    void RestartGame(){
        ResetBall();
        Invoke("PushBall", 2);
    }

    public Vector2 TrajectoryOrigin{
        get { return trajectoryOrigin; }
    }
}
