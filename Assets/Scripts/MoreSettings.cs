///-----------------------------------------------------------------
///   Class:       MoreSettings
///   Description: This class holds static variables for the colors that have been picked from the more settings menu. Also, it has a method to load all of these choices from PlayerPrefs.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;

public class MoreSettings : MonoBehaviour
{
    public static bool isBackgroundColor = true;
    public static bool isBallColor = false;
    public static bool isPaddleColor = false;

    public static Color backgroundColor;
    public static Color ballColor;
    public static Color paddleColor;

    const int FullAlpha = 1;

    /// <summary> LoadMoreSettings is a Method in the MoreSettings Class.
    /// <para>This method loads all of the MoreSettings from PlayerPrefs if they exist.</para>
    /// </summary>
    public static void LoadMoreSettings()
    {
        if (PlayerPrefs.HasKey("backgroundColorR"))
        {
            backgroundColor.r = PlayerPrefs.GetFloat("backgroundColorR");
            backgroundColor.g = PlayerPrefs.GetFloat("backgroundColorG");
            backgroundColor.b = PlayerPrefs.GetFloat("backgroundColorB");
            backgroundColor.a = FullAlpha;
        }
        else
        {
            backgroundColor = Color.black;
            backgroundColor.a = FullAlpha;
            PlayerPrefs.SetFloat("backgroundColorR", backgroundColor.r);
            PlayerPrefs.SetFloat("backgroundColorG", backgroundColor.g);
            PlayerPrefs.SetFloat("backgroundColorB", backgroundColor.b);
        }

        if (PlayerPrefs.HasKey("ballColorR"))
        {
            ballColor.r = PlayerPrefs.GetFloat("ballColorR");
            ballColor.g = PlayerPrefs.GetFloat("ballColorG");
            ballColor.b = PlayerPrefs.GetFloat("ballColorB");
            ballColor.a = FullAlpha;
        }
        else
        {
            ballColor = Color.white;
            ballColor.a = FullAlpha;
            PlayerPrefs.SetFloat("ballColorR", ballColor.r);
            PlayerPrefs.SetFloat("ballColorG", ballColor.g);
            PlayerPrefs.SetFloat("ballColorB", ballColor.b);
        }

        if (PlayerPrefs.HasKey("paddleColorR"))
        {
            paddleColor.r = PlayerPrefs.GetFloat("paddleColorR");
            paddleColor.g = PlayerPrefs.GetFloat("paddleColorG");
            paddleColor.b = PlayerPrefs.GetFloat("paddleColorB");
            paddleColor.a = FullAlpha;
        }
        else
        {
            paddleColor = Color.white;
            paddleColor.a = FullAlpha;
            PlayerPrefs.SetFloat("paddleColorR", paddleColor.r);
            PlayerPrefs.SetFloat("paddleColorG", paddleColor.g);
            PlayerPrefs.SetFloat("paddleColorB", paddleColor.b);
        }
    }
}
