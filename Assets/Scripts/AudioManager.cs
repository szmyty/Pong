///-----------------------------------------------------------------
///   Class:       AudioManager
///   Description: This class is responsible for playing the correct sounds at the correct moments (i.e. the button sound when a button is clicked, etc.)
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip pauseSound;
    public AudioClip buttonClickSound;
    public AudioClip bounceSound;
    public AudioClip scoreSound;

    AudioSource audioSource;

    /// <summary>Start is a Method in the AudioManager Class.
    /// <para>This method gets the AudioSource component.</para>
    /// </summary>
    private void Start()
    {      
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    /// <summary>PlayButtonClick is a Method in the AudioManager Class. 
    /// <para>This method plays the button sound when buttons are clicked.</para>
    /// </summary>
    public void PlayButtonClick()
    {
        audioSource.clip = buttonClickSound;
        Play();
    }

    /// <summary>PlayPauseSound is a Method in the AudioManager Class.
    /// <para>This method plays the pause sound when the game is paused.</para>
    /// </summary>
    public void PlayPauseSound()
    {
        audioSource.clip = pauseSound;
        Play();
    }

    /// <summary>PlayBouncesound is a Method in the AudioManager Class.
    /// <para>This method plays the bounce sound when the ball bounces off of a paddle.</para>
    /// </summary>
    public void PlayBounceSound()
    {
        audioSource.clip = bounceSound;
        Play();
    }

    /// <summary>PlayScoreSound is a Method in the AudioManager Class.
    /// <para>This method plays the score sound when someone gets a goal.</para>
    /// </summary>
    public void PlayScoreSound()
    {
        audioSource.clip = scoreSound;
        Play();
    }

    /// <summary>Play is a Method in the AudioManager Class.
    /// <para>This method plays the sound that is currently the audioSource's clip.</para>
    /// </summary>
    public void Play()
    {
        audioSource.Play();
    }
}
