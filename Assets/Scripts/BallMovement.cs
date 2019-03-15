///-----------------------------------------------------------------
///   Class:       BallMovement
///   Description: This class is responsible for moving the ball.
///   Author:      Keywi
///-----------------------------------------------------------------

using System;
using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public float movementSpeed;
    public float extraSpeedPerHit;
    public float maxSpeed;

    int hitCounter = 0;

    public bool isMoved = false;

    Rigidbody2D thisRigidbody2D;

    /// <summary>Start is a Method in the BallMovement Class.
    /// <para>This method moves the ball in a random direction to start with.</para>
    /// </summary>
	void Start () {
        thisRigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        int random = UnityEngine.Random.Range(0, 2);
        if(random == 0)
        {
            StartCoroutine(StartBall(true));
        }
        else
        {
            StartCoroutine(StartBall(false));
        }
    }

    /// <summary>Update is a Method in the BallMovement Class.
    /// <para>This method checks every frame to see if the ball is moving. If for some reason the ball isn't moving (is stuck), then move the ball by force.</para>
    /// </summary>
    private void Update()
    {
        if (isMoved)
        {
            if(thisRigidbody2D.IsSleeping())
            {
                int random = UnityEngine.Random.Range(0, 2);
                if(random == 0)
                {
                    MoveBall(new Vector2(-1, 0));
                }
                else
                {
                    MoveBall(new Vector2(1, 0));
                }
            }
        }
    }

    /// <summary>StartBall is a Coroutine Method in the BallMovement Class.
    /// <para>This method waits for 2 seconds and then moves the ball.</para>
    /// </summary>
    /// <param name="isStartingPlayer1"></param>
    /// <returns>IEnumerator</returns>
    public IEnumerator StartBall(bool isStartingPlayer1)
    {
        hitCounter = 0;
        yield return new WaitForSeconds(2);

        if (isStartingPlayer1)
        {
            MoveBall(new Vector2(-1, 0));
        }
        else
        {
            MoveBall(new Vector2(1, 0));
        }

        isMoved = true;
    }

    /// <summary>MoveBall is a Method in BallMovement Class.
    /// <para>This method actually moves the ball using its rigidbody2D component.</para>
    /// </summary>
    /// <param name="direction"></param>
	public void MoveBall(Vector2 direction)
    {
        /* Normalize the direction vector. */
        direction = direction.normalized;

        float speed = movementSpeed + hitCounter * extraSpeedPerHit;

        /* Move the ball. */
        try
        {
            thisRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            thisRigidbody2D.velocity = direction * speed;
        }
        catch (Exception e)
        {
            print(e);
        }
    }

    /// <summary>IncreaseHitCounter is a Method in the BallMovement Class.
    /// <para>This method adds speed to the ball by counting the hits. Once it gets to the maxSpeed, it doesn't need to add to hit counter.</para>
    /// </summary>
    public void IncreaseHitCounter()
    {
        if(hitCounter * extraSpeedPerHit <= maxSpeed)
        {
            hitCounter++;
        }
    }
}
