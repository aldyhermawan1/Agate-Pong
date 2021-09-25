using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Player1
    public PlayerControl p1;
    private Rigidbody2D p1rb;
    //Player2
    public PlayerControl p2;
    private Rigidbody2D p2rb;
    //Ball
    public BallControl ball;
    private Rigidbody2D ballrb;
    private CircleCollider2D ballCollider;
    //Score
    public int maxScore;
    //Debug
    private bool isDebugWindowShown = false;
    //Trajectory
    public Trajectory trajectory;

    // Start is called before the first frame update
    void Start()
    {
        p1rb = p1.GetComponent<Rigidbody2D>();
        p2rb = p2.GetComponent<Rigidbody2D>();
        ballrb = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI(){
        //SCORE
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + p1.score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + p2.score);

        //RESTART
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART")){
            p1.ResetScore();
            p2.ResetScore();
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        //Max Score
        if (p1.score == maxScore) {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER 1 WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        } else if (p2.score == maxScore) {
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER 2 WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        //DEBUG
        if (GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO")){
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
        Color oldColor = GUI.backgroundColor;
        if (isDebugWindowShown){
            GUI.backgroundColor = Color.red;
            float ballMass = ballrb.mass;
            Vector2 ballVelocity = ballrb.velocity;
            float ballSpeed = ballrb.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulseP1X = p1.LastContactPoint.normalImpulse;
            float impulseP1Y = p1.LastContactPoint.tangentImpulse;
            float impulseP2X = p2.LastContactPoint.normalImpulse;
            float impulseP2Y = p2.LastContactPoint.tangentImpulse;

            string debugText = 
                "Ball mass = " + ballMass+ "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulseP1X + ", " + impulseP1Y + ")\n" +
                "Last impulse from player 2 = (" + impulseP2X + ", " + impulseP2Y + ")\n";

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);
        } else if (!isDebugWindowShown) {
            GUI.backgroundColor = oldColor;
        }
    }
}
