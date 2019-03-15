///-----------------------------------------------------------------
///   Class:       StatisticsManager
///   Description: This class is responsible for managing all of the user stats and updating the GUI accordingly.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;
using TMPro;

public class StatisticsManager : MonoBehaviour
{
    public TMP_Text userText;
    public TMP_Text lifeTimeGoals;
    public TMP_Text lifeTimeBounces;
    public TMP_Text lifeTimeWins;
    public TMP_Text lifeTimeLosses;

    /// <summary>Start is a Method in the StatisticsManager Class.
    /// <para>This class prints out the stats for the current User onto the Stats GUI.</para>
    /// </summary>
    void Start()
    {
        if (Users.isPlayerOneStats)
        {
            SetUserName(Users.playerOneCurrentUserIndex);
            SetStats(Users.playerOneCurrentUserIndex);
        }
        else
        {
            SetUserName(Users.playerTwoCurrentUserIndex);
            SetStats(Users.playerTwoCurrentUserIndex);
        }
    }

    /// <summary>SetUserName is a Method in the StatisticsManager Class.
    /// <para>This method sets the username text at the top of the page to the current selected user's username.</para>
    /// </summary>
    /// <param name="userIndex"></param>
    void SetUserName(int userIndex)
    {
        userText.text = Users.users[userIndex].GetUserName();
    }

    /// <summary>SetStats is a Method in the StatisticsManager Class.
    /// <para>This method sets all of the stats text on the stats page given the statistics from the current selected user.</para>
    /// </summary>
    /// <param name="userIndex"></param>
    void SetStats(int userIndex)
    {
        lifeTimeGoals.text = Users.users[userIndex].GetLifeTimeGoals().ToString();
        lifeTimeBounces.text = Users.users[userIndex].GetLifeTimeBounces().ToString();
        lifeTimeWins.text = Users.users[userIndex].GetLifeTimeWins().ToString();
        lifeTimeLosses.text = Users.users[userIndex].GetLifeTimeLosses().ToString();
    }
}
