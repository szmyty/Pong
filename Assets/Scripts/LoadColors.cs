using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///-----------------------------------------------------------------
///   Class:       LoadColors
///   Description: This class is responsible for loading the correct color to the rawImages in the more settings menu to show the user the current color choice they have for each object.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine.UI;

public class LoadColors : MonoBehaviour
{
    public RawImage backgroundColorImage;
    public RawImage ballColorImage;
    public RawImage paddleColorImage;

    /// <summary>Start is a Method in the LoadColors Class.
    /// <para>This method calls the LoadImageColors method at the start of the scene.</para>
    /// </summary>
    void Start()
    {
        LoadImageColors();
    }

    /// <summary>LoadImageColors is a Method in the LoadColors Class.
    /// <para>This method sets the color of each rawImage in the MoreSettings scene. This helps the user to know the current colors selected for each object.</para>
    /// </summary>
    public void LoadImageColors()
    {
        backgroundColorImage.color = MoreSettings.backgroundColor;
        ballColorImage.color = MoreSettings.ballColor;
        paddleColorImage.color = MoreSettings.paddleColor;
    }
}
