///-----------------------------------------------------------------
///   Class:       Users
///   Description: This class is responsible for holding the static variables pertaining to the users in the game.
///   Author:      Keywi
///-----------------------------------------------------------------

using System.Collections.Generic;


public class Users
{
    public static List<User> users = new List<User>();
    public static int playerOneCurrentUserIndex = 0;
    public static int playerTwoCurrentUserIndex = 1;
    public static bool hasLoadedUserAccounts = false;
    public static bool isPlayerOneStats = true;
}
