///-----------------------------------------------------------------
///   Class:       AIPlayer
///   Description: This class is responsible for the AI control of the second                      paddle when the game is in 1 Player mode. The paddle follow                     the ball that is closest to it and tries to be there before it                  goes by for a goal.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    /* How fast the paddle will move towards the ball. */
    public float movementSpeed;

    /* The min y distance that triggers when the paddle will start following. */
    public float distanceToFollow;

    public bool isMultiPlayer = false;

    GameManager gameManager;

    /* The current closest ball. */
    GameObject closestBall;

    bool isFirstBall = true;

    float paddleXPosition;

    /// <summary>Start is a Method in the AIPlayer Class.
    /// <para>This method gets the right paddle's x position.</para>
    /// </summary>
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        paddleXPosition = gameManager.rightPaddle.transform.position.x;
    }

    /// <summary>FixedUpdate is a Method in the AIPlayer Class.
    /// <para>This method finds the ball closest to the right paddle and follows it when it gets close enough. This only happens when the game is in 1 Player mode.</para>
    /// </summary>
    private void FixedUpdate()
    {
        if (!isMultiPlayer)
        {
            /* Initialize the closest ball to be the first ball. */
            if (isFirstBall)
            {
                closestBall = gameManager.balls[0];
                isFirstBall = false;
            }

            /* Intialize the closest ball position to be the distance from the paddle to the center. */
            float closestBallPosition = paddleXPosition;

            /* Try to follow the closest ball to the paddle. */
            foreach (GameObject ball in gameManager.balls)
            {
                float distanceFromPaddle = 0.0f;

                /* If the ball is to the right of the paddle, then no need to follow it. */
                if (ball.transform.position.x > paddleXPosition)
                {
                    continue;
                }

                /* Calculate the distance of the ball from the paddle. */
                if (ball.transform.position.x < 0)
                {
                    distanceFromPaddle = paddleXPosition + Mathf.Abs(ball.transform.position.x);
                }
                else
                {
                    distanceFromPaddle = paddleXPosition - ball.transform.position.x;
                }

                /* If the distance of this ball is closer to the paddle than the current closest ball, set this ball as the new closest ball. */
                if (distanceFromPaddle < closestBallPosition)
                {
                    closestBallPosition = distanceFromPaddle;
                    closestBall = ball;
                }
            }

            /* When the current closest ball is above or below the paddle, follow it. */
            if (Mathf.Abs(transform.position.y - closestBall.transform.position.y) > distanceToFollow)
            {
                if (transform.position.y < closestBall.transform.position.y)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * movementSpeed;
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * movementSpeed;
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }
}
