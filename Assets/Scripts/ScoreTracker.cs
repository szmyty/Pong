///-----------------------------------------------------------------
///   Class:       ScoreTracker
///   Description: This class is responsible for keeping track of the score of the game.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;

public class ScoreTracker : MonoBehaviour {

    GameObject gameManagerObject;
    GameManager gameManager;

    bool isInfiniteScore = false;

    /// <summary>Start is a Method in the ScoreTracker Class.
    /// <para>This method checks to see if the setting for a score limit is set to infinity. If it is, then it turns on a bool that declares that.</para>
    /// </summary>
    public void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        if(Settings.scoreToWin == int.MaxValue || Settings.scoreToWin == int.MinValue)
        {
            isInfiniteScore = true;
        }
    }

    /// <summary>OnTriggerEnter2D is a Method in the ScoreTracker Class.
    /// <para>This method checks for collisions on either wall in game. If there is a collision, that means someone scored a goal. It checks which wall it was and awards the player who scored a point. It also adds that to the user stats. If the score has reached the score limit, then it declares the winner.</para>
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ball" && gameObject.name == "WallLeft")
        {
            gameManager.player2Score += 1;
            Users.users[Users.playerTwoCurrentUserIndex].AddGoal();
            UsersInitializer.SaveLifeTimeGoals();
            gameManager.player2ScoreText.text = gameManager.player2Score.ToString();
            gameManager.ResetBall(false, collision.gameObject);
        }
        else if (collision.tag == "ball" && gameObject.name == "WallRight")
        {
            gameManager.player1Score += 1;
            Users.users[Users.playerOneCurrentUserIndex].AddGoal();
            UsersInitializer.SaveLifeTimeGoals();
            gameManager.player1ScoreText.text = gameManager.player1Score.ToString();
            gameManager.ResetBall(true, collision.gameObject);
        }

        if (!isInfiniteScore)
        {
            if (gameManager.player1Score >= Settings.scoreToWin)
            {
                gameManager.GameOver("Player 1 Wins!");
                Users.users[Users.playerOneCurrentUserIndex].AddWin();
                Users.users[Users.playerTwoCurrentUserIndex].AddLoss();
                UsersInitializer.SaveLifeTimeWins();
                UsersInitializer.SaveLifeTimeLosses();
            }
            else if (gameManager.player2Score >= Settings.scoreToWin)
            {
                gameManager.GameOver("Player 2 Wins!");
                Users.users[Users.playerTwoCurrentUserIndex].AddWin();
                Users.users[Users.playerOneCurrentUserIndex].AddLoss();
                UsersInitializer.SaveLifeTimeWins();
                UsersInitializer.SaveLifeTimeLosses();
            }
        }
    }
}
