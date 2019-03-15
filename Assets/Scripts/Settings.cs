///-----------------------------------------------------------------
///   Class:       Settings
///   Description: This class is responsible for the static variables that hold the main user settings. It also loads the settings if they already exist in PlayerPrefs.
///   Author:      Keywi
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static int scoreToWin;
    public static string paddleSize;
    public static int time;
    public static string difficulty;
    public static int ballCount;
    public static string ballSize;
    public static int ballCountMax = 25;
    public static int maxScoreToWin = 25;
    public static int maxTimeToWin = 60;
    public static int maxBallCount = 25;

    public static bool isMultiplePlayers = false;

    public static SortedList<int, string> paddleSizeOptions = new SortedList<int, string>()
    {
        { 75, "Very Small" },
        { 150, "Small" },
        { 250, "Normal" },
        { 300, "Large" },
        { 375, "Very Large" }
    };

    public static SortedList<int, string> difficultyOptions = new SortedList<int, string>()
    {
        { 20, "Very Easy" },
        { 19, "Easy" },
        { 18, "Normal" },
        { 17, "Hard" },
        { 16, "Very Hard" },
        { 5, "Impossible" }
    };

    public static SortedList<int, string> ballSizeOptions = new SortedList<int, string>()
    {
        { 15, "Very Small" },
        { 25, "Small" },
        { 40, "Normal" },
        { 60, "Large" },
        { 100, "Very Large" }
    };

    /// <summary>LoadSettings is a Method in the Settings Class.
    /// <para>This method loads all of the settings if they exist in PlayerPrefs.</para>
    /// </summary>
    public static void LoadSettings()
    {
        if (PlayerPrefs.HasKey("scoreToWin"))
        {
            scoreToWin = PlayerPrefs.GetInt("scoreToWin");
        }
        else
        {
            scoreToWin = 5;
            PlayerPrefs.SetInt("scoreToWin", scoreToWin);
        }

        if (PlayerPrefs.HasKey("time"))
        {
            time = PlayerPrefs.GetInt("time");
        }
        else
        {
            time = int.MinValue;
        }

        if (PlayerPrefs.HasKey("paddleSize"))
        {
            paddleSize = PlayerPrefs.GetString("paddleSize");
        }
        else
        {
            paddleSize = "Normal";
            PlayerPrefs.SetString("paddleSize", paddleSize);
        }

        if (PlayerPrefs.HasKey("ballSize"))
        {
            ballSize = PlayerPrefs.GetString("ballSize");
        }
        else
        {
            ballSize = "Normal";
            PlayerPrefs.SetString("ballSize", ballSize);
        }

        if (PlayerPrefs.HasKey("difficulty"))
        {
            difficulty = PlayerPrefs.GetString("difficulty");
        }
        else
        {
            difficulty = "Normal";
            PlayerPrefs.SetString("difficulty", difficulty);
        }

        if (PlayerPrefs.HasKey("ballCount"))
        {
            ballCount = PlayerPrefs.GetInt("ballCount");
        }
        else
        {
            ballCount = 1;
            PlayerPrefs.SetInt("ballCount", ballCount);
        }
    }
}
