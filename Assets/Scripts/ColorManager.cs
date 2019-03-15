///-----------------------------------------------------------------
///   Class:       ColorManager
///   Description: This class keeps track of what object's color is going to be changed.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public Toggle backgroundColorToggle;
    public Toggle ballColorToggle;
    public Toggle paddleColorToggle;

    /// <summary>Start is a Method in the ColorManager Class.
    /// <para>This method starts the background color as the first option as default for color changing.</para>
    /// </summary>
    private void Start()
    {
        MoreSettings.isBackgroundColor = true;
    }

    /// <summary>ToggleBackgroundColor is a Method in the ColorManager Class.
    /// <para>This method toggles the background color to be true.</para>
    /// </summary>
    public void ToggleBackgroundColor()
    {
        MoreSettings.isBackgroundColor = true;
        MoreSettings.isBallColor = false;
        MoreSettings.isPaddleColor = false;
    }

    /// <summary>ToggleBallColor is a Method in the ColorManager Class.
    /// <para>This method toggles the ball color to be true.</para>
    /// </summary>
    public void ToggleBallColor()
    {
        MoreSettings.isBallColor = true;
        MoreSettings.isBackgroundColor = false;
        MoreSettings.isPaddleColor = false;
    }

    /// <summary>TogglePaddleColor is a Method in the ColorManager Class.
    /// <para>This method toggles the paddle color to be true.</para>
    /// </summary>
    public void TogglePaddleColor()
    {
        MoreSettings.isPaddleColor = true;
        MoreSettings.isBackgroundColor = false;
        MoreSettings.isBallColor = false;
    }
}
