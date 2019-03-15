///-----------------------------------------------------------------
///   Class:       User
///   Description: This class contains a User object.
///   Author:      Keywi
///-----------------------------------------------------------------


public class User
{
    string userName;
    int lifeTimeGoals;
    int lifeTimeBounces;
    int lifeTimeWins;
    int lifeTimeLosses;

    public User(string newUserName)
    {
        userName = newUserName;
        lifeTimeGoals = 0;
        lifeTimeBounces = 0;
        lifeTimeWins = 0;
        lifeTimeLosses = 0;
    }

    public User(string newUserName, int newLifeTimeGoals, int newLifeTimeBounces, int newLifeTimeWins, int newLifeTimeLosses)
    {
        userName = newUserName;
        lifeTimeGoals = newLifeTimeGoals;
        lifeTimeBounces = newLifeTimeBounces;
        lifeTimeWins = newLifeTimeWins;
        lifeTimeLosses = newLifeTimeLosses;
    }

    /// <summary>AddGoal is a Method in the User Class.
    /// <para>This method adds a goal to the lifeTimeGoals statistic for this user.</para>
    /// </summary>
    public void AddGoal()
    {
        lifeTimeGoals += 1;
    }

    /// <summary>AddBounce is a Method in the User Class.
    /// <para>This method adds a bounce to the lifeTimeBounces statistic for this user.</para>
    /// </summary>
    public void AddBounce()
    {
        lifeTimeBounces += 1;
    }

    /// <summary>AddWin is a Method in the User Class.
    /// <para>This method adds a win to the lifeTimeWins statistic for this user.</para>
    /// </summary>
    public void AddWin()
    {
        lifeTimeWins += 1;
    }

    /// <summary>AddLoss is a Method in the User Class.
    /// <para>This method adds a loss to the lifeTimeLosses statistic for this user.</para>
    /// </summary>
    public void AddLoss()
    {
        lifeTimeLosses += 1;
    }

    /// <summary>GetUserName is a Method in the User Class.
    /// <para>This method returns the username of this user.</para>
    /// </summary>
    /// <returns></returns>
    public string GetUserName()
    {
        return userName;
    }

    /// <summary>SetUserName is a Method in the User Class.
    /// <para>This method sets the username of this user.</para>
    /// </summary>
    /// <returns></returns>
    public void SetUserName(string newUserName)
    {
        userName = newUserName;
    }

    /// <summary>GetLifeTimeGoals is a Method in the User Class.
    /// <para>This method returns the lifeTimeGoals of this user.</para>
    /// </summary>
    /// <returns></returns>
    public int GetLifeTimeGoals()
    {
        return lifeTimeGoals;
    }

    /// <summary>GetLifeTimeBounces is a Method in the User Class.
    /// <para>This method returns the lifeTimeBounces of this user.</para>
    /// </summary>
    /// <returns></returns>
    public int GetLifeTimeBounces()
    {
        return lifeTimeBounces;
    }

    /// <summary>GetLifeTimeWins is a Method in the User Class.
    /// <para>This method returns the lifeTimeWins of this user.</para>
    /// </summary>
    /// <returns></returns>
    public int GetLifeTimeWins()
    {
        return lifeTimeWins;
    }

    /// <summary>GetLifeTimeLosses is a Method in the User Class.
    /// <para>This method returns the lifeTimeLosses of this user.</para>
    /// </summary>
    /// <returns></returns>
    public int GetLifeTimeLosses()
    {
        return lifeTimeLosses;
    }
}
