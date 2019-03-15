///-----------------------------------------------------------------
///   Class:       GameManager
///   Description: This class controls the main features of the game itself and also the game scene.
///   Author:      Keywi
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;

    public GameObject ballPrefab;

    public GameObject ball;

    public List<GameObject> balls;

    public GameObject centerField;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public GameObject bottomWall;
    public GameObject topWall;

    public GameObject leftPaddle;
    public GameObject rightPaddle;

    Vector3 startingLeftPaddle;
    Vector3 startingRightPaddle;
    Transform startingBallPosition;

    public AudioManager audioManager;
    public BallMovement ballMovement;

    const int NoAlpha = 0;
    const int FullAlpha = 1;

    [HideInInspector]
    public int player1Score = 0;
    [HideInInspector]
    public int player2Score = 0;

    bool isPaused = false;
    bool isGameOver = false;
    bool isMultipleBalls = false;

    public TextMeshProUGUI winner;

    public Timer timer;

    public AIPlayer aiPlayer;

    /// <summary>Start is a Method in the GameManager Class.
    /// <para>This method checks to see if it is a one player game or a two player game. If it is a two player game, then we need to turn on the AI player. It also initializes the player texts to the chosen users. It sets the settings that were chosen in the settings menu and initializes the balls to start the game.</para>
    /// </summary>
    private void Start()
    {
        if (Settings.isMultiplePlayers)
        {
            aiPlayer.isMultiPlayer = true;
        }
        else
        {
            SetDifficulty();
        }

        player1Text.text = Users.users[Users.playerOneCurrentUserIndex].GetUserName();
        player2Text.text = Users.users[Users.playerTwoCurrentUserIndex].GetUserName();

        /* Get starting positions of both paddles and ball. */
        startingLeftPaddle = leftPaddle.transform.position;
        startingRightPaddle = rightPaddle.transform.position;
        startingBallPosition = ball.transform;

        /* Add the first ball to the balls list. */
        balls.Add(ball);

        if(Settings.ballCount > 1)
        {
            isMultipleBalls = true;
            InitializeBalls();
        }

        SetBallSize(ball);
        SetPaddleSizes();
    }

    /// <summary>InitializeBalls is a Method in the GameManager Class.
    /// <para>This method calls a coroutine to intialize the amount of balls that has been chosen in the settings menu.</para>
    /// </summary>
    void InitializeBalls()
    {
        for (int i = 1; i < Settings.ballCount; i++)
        {
            StartCoroutine(InitializeBall());
        }
    }

    /// <summary>InitializeBall is a Coroutine Method in the GameManager Class.
    /// <para>This method instantiates a ball in starting position with the correct ball size and then adds it to the balls list.</para>
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator InitializeBall()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject newBall = Instantiate(ballPrefab, startingBallPosition.parent.transform);
        SetBallSize(newBall);
        balls.Add(newBall);
    }

    /// <summary>SetPaddleSizes is a Method in the GameManager Class.
    /// <para>This method gets the paddle size setting from the settings menu and sets the paddle size in game accordingly. It only sets the paddle size of the human players' paddles.</para>
    /// </summary>
    void SetPaddleSizes()
    {
        /* Get the current paddle size choice from settings. */
        int currentPaddleSizeOption = Settings.paddleSizeOptions.IndexOfValue(Settings.paddleSize);

        /* Set paddle size as a vector. */
        Vector3 paddleSize = new Vector3(Settings.paddleSizeOptions.Keys[currentPaddleSizeOption], leftPaddle.transform.localScale.y, leftPaddle.transform.localScale.z);

        /* Set the left paddle to size from settings. */
        leftPaddle.transform.localScale = paddleSize;

        /* If it is a 2 player game, set player 2's paddle to correct size. */
        if (Settings.isMultiplePlayers)
        {
            rightPaddle.transform.localScale = paddleSize;
        }
    }

    /// <summary>SetBallSize is a Method in the GameManager Class.
    /// <para>This method gets the ball size setting from the settings menu and sets the ball size in game accordingly.</para>
    /// </summary>
    /// <param name="ballToSize"></param>
    void SetBallSize(GameObject ballToSize)
    {
        /* Get the current ball size choice from settings. */
        int currentBallSizeOption = Settings.ballSizeOptions.IndexOfValue(Settings.ballSize);

        /* Set ball size as a vector. */
        Vector3 ballSize = new Vector3(Settings.ballSizeOptions.Keys[currentBallSizeOption], Settings.ballSizeOptions.Keys[currentBallSizeOption], ball.transform.localScale.z);

        ballToSize.transform.localScale = ballSize;
    }

    /// <summary>SetDifficulty is a Method in the GameManager Class.
    /// <para>This method gets the difficulty setting from the settings menu and sets the AI's difficulty accordingly.</para>
    /// </summary>
    void SetDifficulty()
    {
        int currentDifficultyOption = Settings.difficultyOptions.IndexOfValue(Settings.difficulty);

        aiPlayer.distanceToFollow = Settings.difficultyOptions.Keys[currentDifficultyOption];
    }

    /// <summary>Update is a Method in the GameManager Class.
    /// <para>This method checks for user input and pauses the game if the pause buttons are clicked.</para>
    /// </summary>
    void Update () {
        if (Input.GetButtonDown("Cancel") || Input.GetKeyUp(KeyCode.Joystick1Button7) || Input.GetKeyUp(KeyCode.Joystick2Button7))
        {
            audioManager.PlayPauseSound();
            TogglePause();
        }
	}

    /// <summary>PlayAgain is a Method in the GameManager Class.
    /// <para>This method calls the ResetGame method.</para>
    /// </summary>
    public void PlayAgain()
    {
        ResetGame();
    }

    /// <summary>ToggleBallVisibility is a Method in the GameManager Class.
    /// <para>This method toggles the balls visibility, so that they don't show up when the game is paused.</para>
    /// </summary>
    /// <param name="isShowing"></param>
    public void ToggleBallVisibility(bool isShowing)
    {
        Color color;
        if (isShowing)
        {
            color = MoreSettings.ballColor;
            color.a = NoAlpha;
        }
        else
        {
            color = MoreSettings.ballColor;
            color.a = FullAlpha;
        }

        foreach (GameObject ball in balls)
        {
            ball.GetComponent<SpriteRenderer>().color = color;
        }
    }

    /// <summary>TogglePause is a Method in the GameManager Class.
    /// <para>This method toggles the pause menu and removes all objects that shouldn't be seen when the game is paused.</para>
    /// </summary>
    public void TogglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            ToggleBallVisibility(true);
            centerField.SetActive(false);
            leftPaddle.SetActive(false);
            rightPaddle.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
            ToggleBallVisibility(false);
            centerField.SetActive(true);
            leftPaddle.SetActive(true);
            rightPaddle.SetActive(true);
            ResetTimeScale();
        }
        isPaused = !isPaused;
    }

    /// <summary>GameOver is a Method in the GameManager Class.
    /// <para>This method toggles the game over menu and removes all objects that shouldn't be seen when the game over menu is showing.</para>
    /// </summary>
    /// <param name="winnerText"></param>
    public void GameOver(string winnerText)
    {
        if (!isGameOver)
        {
            Time.timeScale = 0;
            ToggleBallVisibility(true);
            centerField.SetActive(false);
            bottomWall.SetActive(false);
            topWall.SetActive(false);
            leftPaddle.SetActive(false);
            rightPaddle.SetActive(false);
            gameOverMenu.SetActive(true);
            winner.text = winnerText;
        }
        else
        {
            gameOverMenu.SetActive(false);
            ToggleBallVisibility(false);
            centerField.SetActive(true);
            bottomWall.SetActive(true);
            topWall.SetActive(true);
            leftPaddle.SetActive(true);
            rightPaddle.SetActive(true);
            ResetTimeScale();
        }
        isGameOver = !isGameOver;
    }

    /// <summary>ResetGame is a Method in the GameManager Class.
    /// <para>This method sets all attributes of the game back to their default states. It also calls to destroy all of the balls except the first one.</para>
    /// </summary>
    public void ResetGame()
    {
        player1Score = 0;
        player1ScoreText.text = "0";
        player2Score = 0;
        player2ScoreText.text = "0";
        GameOver("Reset Game");

        DestroyExtraBalls();

        int random = Random.Range(0, 2);
        if(random == 0)
        {
            ResetBall(true, ball);
        }
        else
        {
            ResetBall(false, ball);
        }

        timer.ResetTimer();

        if (isMultipleBalls)
        {
            InitializeBalls();
        }
    }

    /// <summary>ResetTimeScale is a Method in the GameManagerClass.
    /// <para>This method resets the time scale back to normal, so that the game can resume normally.</para>
    /// </summary>
    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    /// <summary>ResetBall is a Method in the GameManager Class.
    /// <para>This method resets the ball to its original state. If there is only one ball in the game, then the paddles also get reset.</para>
    /// </summary>
    /// <param name="isStartingPlayer1"></param>
    /// <param name="currentBall"></param>
    public void ResetBall(bool isStartingPlayer1, GameObject currentBall)
    {
        currentBall.transform.position = new Vector3(0, 0, currentBall.transform.position.z);
        currentBall.GetComponent<BallMovement>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        currentBall.GetComponent<BallMovement>().isMoved = false;

        if (!isMultipleBalls) {
            leftPaddle.transform.position = startingLeftPaddle;
            rightPaddle.transform.position = startingRightPaddle;
        }

        StartCoroutine(currentBall.GetComponent<BallMovement>().StartBall(isStartingPlayer1));
    }

    /// <summary>DestroyExtraBalls is a Method in the GameManager Class.
    /// <para>This method destroys all of the balls except for the original ball. The reason for this is because the game was reset, so we still need that original ball.</para>
    /// </summary>
    public void DestroyExtraBalls()
    {
        for(int i = 1; i < balls.Count; i++)
        {
            Destroy(balls[i]);
        }

        balls.Clear();
        balls.Add(ball);
    }

    /// <summary>DestroyAllBalls is a Method in the GameManager Class.
    /// <para>This method destroys all of the balls and this happens when the game is over and they leave to the main menu.</para>
    /// </summary>
    public void DestroyAllBalls()
    {
        foreach(GameObject ball in balls)
        {
            Destroy(ball);
        }

        balls.Clear();
    }
}
