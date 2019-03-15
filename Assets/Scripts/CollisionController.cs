///-----------------------------------------------------------------
///   Class:       CollisionController
///   Description: This class deals with the ball collisions.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;

public class CollisionController : MonoBehaviour
{
    AudioManager audioManager;
    BallMovement ballMovement;

    /// <summary>Start is a Method in the CollisionController Class.
    /// <para>This method gets the BallMovement and AudioManager components from the scene.</para>
    /// </summary>
    private void Start()
    {
        ballMovement = gameObject.GetComponent<BallMovement>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    /// <summary>BounceFromRacket is a Method in the CollisionController Class.
    /// <para>This method controls the movement of the ball after it hits a paddle.</para>
    /// </summary>
    /// <param name="collision2D"></param>
    void BounceFromRacket(Collision2D collision2D)
    {
        /* Get the positions of the ball and of the paddle. */
        Vector3 ballPosition = transform.position;
        Vector3 paddlePosition = collision2D.gameObject.transform.position;

        /* Get the paddle height from the collider on the paddle. */
        float paddleHeight = collision2D.collider.bounds.size.y;

        float x;
        /* If the ball hits the left paddle, change the direction of the ball to go to the right. Also, add bounces to the stats. */
        if(collision2D.gameObject.name == "LeftPaddle")
        {
            x = 1;
            Users.users[Users.playerOneCurrentUserIndex].AddBounce();
            UsersInitializer.SaveLifeTimeBounces();
        }
        else
        {
            x = -1;
            Users.users[Users.playerTwoCurrentUserIndex].AddBounce();
            UsersInitializer.SaveLifeTimeBounces();
        }

        float y = (ballPosition.y - paddlePosition.y) / paddleHeight;

        ballMovement.IncreaseHitCounter();
        audioManager.PlayBounceSound();
        ballMovement.MoveBall(new Vector2(x, y));
    }

    /// <summary>OnCollisionEnter2D is a Trigger Method in the CollisionController Class.
    /// <para>This method is called when a collision happens. It checks which paddle was colliding with and calls BounceFromRacket.</para>
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "LeftPaddle" || collision.gameObject.name == "RightPaddle")
        {
            BounceFromRacket(collision);
        }
    }
}
