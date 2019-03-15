///-----------------------------------------------------------------
///   Class:       LoadMoreSettings
///   Description: This class is responsible for setting all of the objects in the game scene to their appropriate color given from the MoreSettings menu.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class LoadMoreSettings : MonoBehaviour
{
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public GameObject background;
    public GameObject ball;
    public GameObject ballPrefab;

    /// <summary>Start is a Method in the LoadMoreSettings class.
    /// <para>This method calls the SetColors method.</para>
    /// </summary>
    void Start()
    {
        SetColors();
    }

    /// <summary>SetColors is a Method in the LoadMoreSettings class.
    /// <para>This method sets each object in the game scene to its appropriate color.</para>
    /// </summary>
    void SetColors()
    {
        leftPaddle.GetComponent<SpriteRenderer>().color = MoreSettings.paddleColor;
        rightPaddle.GetComponent<SpriteRenderer>().color = MoreSettings.paddleColor;
        background.GetComponent<Image>().color = MoreSettings.backgroundColor;
        ball.GetComponent<SpriteRenderer>().color = MoreSettings.ballColor;
        ballPrefab.GetComponent<SpriteRenderer>().color = MoreSettings.ballColor;
    }
}
