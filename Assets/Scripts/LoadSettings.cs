///-----------------------------------------------------------------
///   Class:       LoadSettings
///   Description: This class is responsible for loading all user settings at the start of the game.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    /// <summary>Start is a Method in the LoadSettings Class.
    /// <para>This class is a helper class that just loads all of the static settings from Settings class.</para>
    /// </summary>
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        /* Load all of the user settings before the game starts. */
        Settings.LoadSettings();
        MoreSettings.LoadMoreSettings();

        /* Load the user accounts, but make sure to do it only once. */
        if (!Users.hasLoadedUserAccounts)
        {
            UsersInitializer.CheckForUserAccounts();
        }
    }
}
