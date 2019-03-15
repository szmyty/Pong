///-----------------------------------------------------------------
///   Class:       PlayerCount
///   Description: This class is responsible for managing the choice of how many players will be playing the game. This choice is made in the main menu.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class PlayerCount : MonoBehaviour
{
    public Button onePlayerButton;
    public Button twoPlayerButton;

    Color disabledColor = Color.gray;
    Color enabledColor = Color.white;

    /// <summary>Start is a Method in the PlayerCount Class.
    /// <para>This method disables the color of the two playerbutton and defaults the game to a one player game.</para>
    /// </summary>
    void Start()
    {
        twoPlayerButton.image.color = disabledColor;
        Settings.isMultiplePlayers = false;
    }

    /// <summary>OnePlayerGame is a Method in the PlayerCount Class.
    /// <para>This method disables the twoPlayerButton color and enables the onePlayerButton color. It sets the game to be a one player game.</para>
    /// </summary>
    public void OnePlayerGame()
    {
        onePlayerButton.image.color = enabledColor;
        twoPlayerButton.image.color = disabledColor;
        Settings.isMultiplePlayers = false;
    }

    /// <summary>TwoPlayerGame is a Method in the PlayerCount Class.
    /// <para>This method disables the onePlayerButton color and enables the twoPlayerButton color. It sets the game to be a two player game.</para>
    /// </summary>
    public void TwoPlayerGame()
    {
        onePlayerButton.image.color = disabledColor;
        twoPlayerButton.image.color = enabledColor;
        Settings.isMultiplePlayers = true;
    }
}
