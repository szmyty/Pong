///-----------------------------------------------------------------
///   Class:       Timer
///   Description: This class is responsible for keeping the GUI updated to function as a timer.
///   Author:      Keywi
///-----------------------------------------------------------------


using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    int minutesLeft;
    int secondsLeft;
    float secondsGoneBy;
    float minutesGoneBy;

    public TextMeshProUGUI minutesText;
    public TextMeshProUGUI secondsText;
    public GameObject time;

    public GameManager gameManager;

    bool isFirstSecond = true;
    bool isGameStarted = false;
    bool isFirstCountDown = true;

    [HideInInspector]
    public bool isTimedGame = false;

    /// <summary>Start is a Method in the Timer Class.
    /// <para>Checks if the game is a timed game. If it is then it resets the timer and text fields. If it is not, then it removes the textfield with the timer on the player's screen.</para>
    /// </summary>
    void Start()
    {
        /* Check to see if the game is a timed game. */
        if(Settings.time != int.MaxValue && Settings.time != int.MinValue)
        {
            ResetTimer();
        }
        else
        {
            time.SetActive(false);
        }
    }

    /// <summary>Update is a Method in the Timer Class.
    /// <para>Checks if the game is a timed game. If it is then begin the timer processes.</para>
    /// </summary>
    void Update()
    {
        if (isTimedGame)
        {
            /* In the first second of the game, we need to start the game after a two second delay. */
            if (isFirstSecond)
            {
                isFirstSecond = false;
                StartCoroutine(GameStarted());
            }

            /* Once the game starts, the regular countdown functionality can begin. */
            if (!isFirstSecond && isGameStarted)
            {
                Countdown();
            }
        }
    }

    /// <summary>ResetTimer is a Method in the Timer Class.
    /// <para>Resets the timer when a new game is started.</para>
    /// </summary>
    public void ResetTimer()
    {
        isFirstSecond = true;
        isGameStarted = false;
        isFirstCountDown = true;
        isTimedGame = true;
        minutesLeft = Settings.time;
        minutesText.text = minutesLeft.ToString();
        secondsText.text = "00";
        secondsLeft = 60;
    }

    /// <summary>GameStarted is a Coroutine in the Timer Class.
    /// <para>This Coroutine starts when the game has started and waits two seconds, so that the timer starts after the ball begins moving.</para>
    /// </summary>
    IEnumerator GameStarted()
    {
        /* Wait two seconds for the ball to start moving. */
        yield return new WaitForSeconds(2.0f);
        minutesLeft -= 1;
        isGameStarted = true;
    }

    /// <summary>Countdown is a Method in the Timer Class.
    /// <para>Counts down from the start of the game until the end of the game. It updates the text of the timer on screen as it goes and calls the GameOver method once the timer reaches 0 minutes and 0 seconds.</para>
    /// </summary>
    public void Countdown()
    {
        /* Keep track of how much time has passed. */
        float timePassed = (float)System.Math.Round(Time.deltaTime, 3);
        secondsGoneBy += timePassed;
        minutesGoneBy += timePassed;

        secondsGoneBy = (float)System.Math.Round(secondsGoneBy, 3);
        minutesGoneBy = (float)System.Math.Round(minutesGoneBy, 3);

        /* Once the minutes left and the seconds left are 0, then the game is over. */
        if (minutesLeft <= 0 && secondsLeft <= 0)
        {
            secondsText.text = "00";
            minutesText.text = "0";
            if(gameManager.player1Score > gameManager.player2Score)
            {
                gameManager.GameOver("Player 1 Wins!");
            }
            else if(gameManager.player2Score > gameManager.player1Score)
            {
                gameManager.GameOver("Player 2 Wins!");
            }
            else
            {
                gameManager.GameOver("It's a Draw!");
            }
            isTimedGame = false;
            return;
        }

        /* Once a second goes by, decrement the secondsLeft variable until it reaches 0. Once it's 0 reset it to 60 (unless the game is over). */
        if (secondsGoneBy >= 1.0f)
        {
            secondsGoneBy = (float)System.Math.Round(Mathf.Repeat(minutesGoneBy, 1.0f), 3);
            secondsLeft -= 1;

            if (secondsLeft < 10)
            {
                secondsText.text = "0" + secondsLeft.ToString();
            }
            else
            {
                secondsText.text = secondsLeft.ToString();
            }

            /* If the game just started set the text of the minutes to minutes - 1, so that the timer looks correct to the player(s). */
            if (isFirstCountDown)
            {
                minutesText.text = minutesLeft.ToString();
                isFirstCountDown = false;
            }

            if (secondsLeft == 0 && minutesLeft != 0)
            {
                secondsLeft = 60;
            }
        }

        /* Once a minute goes by, decrement from the minutes left and set the text to the minutes left (need to wait 61 seconds, so that the timer looks correct for player(s). */
        if (minutesGoneBy >= 61.0f && minutesLeft != 0)
        {
            minutesGoneBy = 0.0f;
            minutesLeft -= 1;
            minutesText.text = minutesLeft.ToString();
        }
    }
}
