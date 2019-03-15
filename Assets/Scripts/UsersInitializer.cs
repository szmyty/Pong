///-----------------------------------------------------------------
///   Class:       UsersInitializer
///   Description: This class is responsible for managing all of the user stats and updating the GUI accordingly.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;
using System.Linq;

public class UsersInitializer : MonoBehaviour
{
    const int playerOneIndex = 0;
    const int playerTwoIndex = 1;

    /// <summary>CheckForUserAccounts is a Method in the UsersInitializer Class.
    /// <para>This method checks if there are existing user accounts and loads them. If there aren't any, it creates two default ones.</para>
    /// </summary>
    public static void CheckForUserAccounts()
    {
        if (PlayerPrefs.HasKey("userNames"))
        {
            LoadUserAccounts();
        }
        else
        {
            User player1 = new User("Player1");
            Users.users.Add(player1);
            User player2 = new User("Player2");
            Users.users.Add(player2);          
            SaveUserAccounts();
        }

        Users.playerOneCurrentUserIndex = playerOneIndex;
        Users.playerTwoCurrentUserIndex = playerTwoIndex;

        Users.hasLoadedUserAccounts = true;
    }

    /// <summary>SaveUserAccounts is a Method in the UsersInitializer Class.
    /// <para>This method calls all of the other save methods for an overall save.</para>
    /// </summary>
    public static void SaveUserAccounts()
    {
        SaveUserNames();
        SaveLifeTimeGoals();
        SaveLifeTimeBounces();
        SaveLifeTimeWins();
        SaveLifeTimeLosses();
    }

    /// <summary>SaveUserNames is a Method in the UsersInitializer Class.
    /// <para>This method saves the usersNames to PlayerPrefs.</para>
    /// </summary>
    public static void SaveUserNames()
    {
        string[] userNames = Users.users.Select(user => user.GetUserName()).ToArray();
        PlayerPrefsX.SetStringArray("userNames", userNames);
    }


    /// <summary>SaveLifeTimeGoals is a Method in the UsersInitializer Class.
    /// <para>This method saves the lifeTimeGoals to PlayerPrefs.</para>
    /// </summary>
    public static void SaveLifeTimeGoals()
    {
        int[] lifeTimeGoals = Users.users.Select(user => user.GetLifeTimeGoals()).ToArray();
        PlayerPrefsX.SetIntArray("lifeTimeGoals", lifeTimeGoals);
    }


    /// <summary>SaveLifeTimeBounces is a Method in the UsersInitializer Class.
    /// <para>This method saves the lifeTimeBounces to PlayerPrefs.</para>
    /// </summary>
    public static void SaveLifeTimeBounces()
    {
        int[] lifeTimeBounces = Users.users.Select(user => user.GetLifeTimeBounces()).ToArray();
        PlayerPrefsX.SetIntArray("lifeTimeBounces", lifeTimeBounces);
    }


    /// <summary>SaveLifeTimeWins is a Method in the UsersInitializer Class.
    /// <para>This method saves the lifeTimeWins to PlayerPrefs.</para>
    /// </summary>
    public static void SaveLifeTimeWins()
    {
        int[] lifeTimeWins = Users.users.Select(user => user.GetLifeTimeWins()).ToArray();
        PlayerPrefsX.SetIntArray("lifeTimeWins", lifeTimeWins);
    }


    /// <summary>SaveLifeTimeLosses is a Method in the UsersInitializer Class.
    /// <para>This method saves the lifeTimeLosses to PlayerPrefs.</para>
    /// </summary>
    public static void SaveLifeTimeLosses()
    {
        int[] lifeTimeLosses = Users.users.Select(user => user.GetLifeTimeLosses()).ToArray();
        PlayerPrefsX.SetIntArray("lifeTimeLosses", lifeTimeLosses);
    }

    /// <summary>LoadUserAccounts is a Method in the UserInitializer Class.
    /// <para>This method loads all user settings from PlayerPrefs and creates the users list using the loaded settings.</para>
    /// </summary>
    public static void LoadUserAccounts()
    {
        string[] userNames = PlayerPrefsX.GetStringArray("userNames");
        int userCount = userNames.Count();
        int[] lifeTimeGoals = PlayerPrefsX.GetIntArray("lifeTimeGoals");
        int[] lifeTimeBounces = PlayerPrefsX.GetIntArray("lifeTimeBounces");
        int[] lifeTimeWins = PlayerPrefsX.GetIntArray("lifeTimeWins");
        int[] lifeTimeLosses = PlayerPrefsX.GetIntArray("lifeTimeLosses");    

        for (int i = 0; i < userCount; i++)
        {
            Users.users.Add(new User(userNames[i], lifeTimeGoals[i], lifeTimeBounces[i], lifeTimeWins[i], lifeTimeLosses[i]));
        }
    }
}
