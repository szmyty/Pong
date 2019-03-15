///-----------------------------------------------------------------
///   Class:       SettingsManager
///   Description: This class is responsible for managing the GUI in the main Settings menu.
///   Author:      Keywi
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{ 
    public GameObject scoreToWinText;
    public GameObject scoreToWinInfinity;
    public Button lowerScoreToWinButton;
    public Button higherScoreToWinButton;

    public TextMeshProUGUI paddleSizeText;
    public Button lowerPaddleSizeButton;
    public Button higherPaddleSizeButton;

    public GameObject timeText;
    public GameObject timeInfinity;
    public Button lowerTimeButton;
    public Button higherTimeButton;

    public TextMeshProUGUI difficultyText;
    public Button lowerDifficultyButton;
    public Button higherDifficultyButton;

    public TextMeshProUGUI ballCountText;
    public Button lowerBallCountButton;
    public Button higherBallCountButton;

    public TextMeshProUGUI ballSizeText;
    public Button lowerBallSizeButton;
    public Button higherBallSizeButton;

    /// <summary> Start is a Method in the SettingsManager Class.
    /// <para>This function loads the settings from the Settings Class and sets the text of the UI with the correct values for each. Then it scans the buttons to turn off the buttons as necessary.</para>
    /// </summary>
    private void Start()
    {
        LoadSettingsToText();
        ScanButtons();
    }

    /// <summary>ScanButtons is a Method in the SettingsManager Class.
    /// <para>This method disables or enables all of the navigation buttons in the GUI based upon the current user settings. For example, if the scoreToWin setting is already at its minimum, then we can disable the down arrow.</para>
    /// </summary>
    public void ScanButtons()
    {
        if(Settings.scoreToWin == int.MinValue)
        {
            scoreToWinText.SetActive(false);
            scoreToWinInfinity.SetActive(true);
            lowerScoreToWinButton.interactable = false;
        }
        else if(Settings.scoreToWin == int.MaxValue)
        {
            scoreToWinText.SetActive(false);
            scoreToWinInfinity.SetActive(true);
            higherScoreToWinButton.interactable = false;
        }

        if(Settings.paddleSize == "Very Small")
        {
            lowerPaddleSizeButton.interactable = false;
        }
        else if(Settings.paddleSize == "Very Large")
        {
            higherPaddleSizeButton.interactable = false;
        }

        if(Settings.time == int.MinValue)
        {
            timeText.SetActive(false);
            timeInfinity.SetActive(true);
            lowerTimeButton.interactable = false;
        }
        else if(Settings.time == int.MaxValue)
        {
            timeText.SetActive(false);
            timeInfinity.SetActive(true);
            higherTimeButton.interactable = false;
        }

        if(Settings.difficulty == "Very Easy")
        {
            lowerDifficultyButton.interactable = false;
        }
        else if (Settings.difficulty == "Impossible")
        {
            higherDifficultyButton.interactable = false;
        }

        if(Settings.ballCount == 1)
        {
            lowerBallCountButton.interactable = false;
        }
        else if (Settings.ballCount == Settings.ballCountMax)
        {
            higherBallCountButton.interactable = false;
        }

        if(Settings.ballSize == "Very Small")
        {
            lowerBallSizeButton.interactable = false;
        }
        else if(Settings.ballSize == "Very Large")
        {
            higherBallSizeButton.interactable = false;
        }
    }

    /// <summary>LoadSettingsToText is a Method in the SettingsManager Class.
    /// <para>This method initializes all of the user settings to the text in the GUI to depict the current user settings to the user.</para>
    /// </summary>
    public void LoadSettingsToText()
    {
        scoreToWinText.GetComponent<TextMeshProUGUI>().text = Settings.scoreToWin.ToString();
        paddleSizeText.text = Settings.paddleSize;
        timeText.GetComponent<TextMeshProUGUI>().text = Settings.time.ToString();
        difficultyText.text = Settings.difficulty;
        ballCountText.text = Settings.ballCount.ToString();
        ballSizeText.text = Settings.ballSize;
    }

    /// <summary>IncreaseScoreToWin is a Method in the SettingsManager Class.
    /// <para>This method increases the current user setting for scoreToWin and updates the GUI to depict this change.</para>
    /// </summary>
    public void IncreaseScoreToWin()
    {
        if (Settings.scoreToWin == int.MaxValue)
        {
            Debug.Log("Error: Button should be disabled, score is already at max.");
            higherScoreToWinButton.interactable = false;
            return;
        }

        if (Settings.scoreToWin == int.MinValue)
        {
            Settings.scoreToWin = 1;
            scoreToWinInfinity.SetActive(false);
            scoreToWinText.SetActive(true);
        }
        else
        {
            Settings.scoreToWin = Settings.scoreToWin + 1;
        }
       
        if (Settings.scoreToWin == Settings.maxScoreToWin + 1)
        {
            //Set to infinity
            Settings.scoreToWin = int.MaxValue;
            higherScoreToWinButton.interactable = false;
            scoreToWinText.SetActive(false);
            scoreToWinInfinity.SetActive(true);
        }
        else
        {
            scoreToWinText.GetComponent<TextMeshProUGUI>().text = Settings.scoreToWin.ToString();
        }

        if (lowerScoreToWinButton.interactable == false)
        {
            lowerScoreToWinButton.interactable = true;
        }

        SetScoreToWin(Settings.scoreToWin);
    }

    /// <summary>IncreasePaddleSize is a Method in the SettingsManager Class.
    /// <para>This method increases the current user setting for paddleSize and updates the GUI to depict this change.</para>
    /// </summary>
    public void IncreasePaddleSize()
    {
        if(Settings.paddleSize == Settings.paddleSizeOptions.Values[Settings.paddleSizeOptions.Count - 1])
        {
            Debug.Log("Error: Button should be disabled, paddle size is already at max.");
            higherPaddleSizeButton.interactable = false;
            return;
        }

        int currentPaddleSizeIndex = Settings.paddleSizeOptions.IndexOfValue(Settings.paddleSize);
        Settings.paddleSize = Settings.paddleSizeOptions.Values[currentPaddleSizeIndex + 1];

        paddleSizeText.text = Settings.paddleSize;

        if(Settings.paddleSize == Settings.paddleSizeOptions.Values[Settings.paddleSizeOptions.Count - 1])
        {
            higherPaddleSizeButton.interactable = false;
        }

        if(lowerPaddleSizeButton.interactable == false)
        {
            lowerPaddleSizeButton.interactable = true;
        }

        SetPaddleSize(Settings.paddleSize);
    }

    /// <summary>IncreaseBallSize is a Method in the SettingsManager Class.
    /// <para>This method increases the current user setting for ballSize and updates the GUI to depict this change.</para>
    /// </summary>
    public void IncreaseBallSize()
    {
        if (Settings.ballSize == Settings.ballSizeOptions.Values[Settings.ballSizeOptions.Count - 1])
        {
            Debug.Log("Error: Button should be disabled, ball size is already at max.");
            higherBallSizeButton.interactable = false;
            return;
        }

        int currentBallSizeIndex = Settings.ballSizeOptions.IndexOfValue(Settings.ballSize);
        Settings.ballSize = Settings.ballSizeOptions.Values[currentBallSizeIndex + 1];

        ballSizeText.text = Settings.ballSize;

        if (Settings.ballSize == Settings.ballSizeOptions.Values[Settings.ballSizeOptions.Count - 1])
        {
            higherBallSizeButton.interactable = false;
        }

        if (lowerBallSizeButton.interactable == false)
        {
            lowerBallSizeButton.interactable = true;
        }

        SetBallSize(Settings.ballSize);
    }

    /// <summary>IncreaseDifficulty is a Method in the SettingsManager Class.
    /// <para>This method increases the current user setting for difficulty and updates the GUI to depict this change.</para>
    /// </summary>
    public void IncreaseDifficulty()
    {
        if (Settings.difficulty == Settings.difficultyOptions.Values[0])
        {
            Debug.Log("Error: Button should be disabled, difficulty is already at max.");
            higherDifficultyButton.interactable = false;
            return;
        }

        int currentDifficulty = Settings.difficultyOptions.IndexOfValue(Settings.difficulty);
        Settings.difficulty = Settings.difficultyOptions.Values[currentDifficulty - 1];

        difficultyText.text = Settings.difficulty;

        if (Settings.difficulty == Settings.difficultyOptions.Values[0])
        {
            higherDifficultyButton.interactable = false;
        }

        if (lowerDifficultyButton.interactable == false)
        {
            lowerDifficultyButton.interactable = true;
        }

        SetDifficulty(Settings.difficulty);
    }

    /// <summary>IncreaseTime is a Method in the SettingsManager Class.
    /// <para>This method increases the current user setting for time and updates the GUI to depict this change.</para>
    /// </summary>
    public void IncreaseTime()
    {
        if (Settings.time == int.MaxValue)
        {
            Debug.Log("Error: Button should be disabled, time is already at max.");
            higherTimeButton.interactable = false;
            return;
        }

        if(Settings.time == int.MinValue)
        {
            Settings.time = 1;
            timeInfinity.SetActive(false);
            timeText.SetActive(true);
        }
        else
        {
            Settings.time = Settings.time + 1;
        }

        if (Settings.time == Settings.maxTimeToWin + 1)
        {
            //Set to infinity
            Settings.time = int.MaxValue;
            higherTimeButton.interactable = false;
            timeText.SetActive(false);
            timeInfinity.SetActive(true);
        }
        else
        {
            timeText.GetComponent<TextMeshProUGUI>().text = Settings.time.ToString();
        }

        if (lowerTimeButton.interactable == false)
        {
            lowerTimeButton.interactable = true;
        }

        SetTime(Settings.time);
    }

    /// <summary>IncreaseBallCount is a Method in the SettingsManager Class.
    /// <para>This method increases the current user setting for ballCount and updates the GUI to depict this change.</para>
    /// </summary>
    public void IncreaseBallCount()
    {
        if(Settings.ballCount == Settings.ballCountMax)
        {
            Debug.Log("Error: Button should be disabled, ballCount is already at max.");
            higherBallCountButton.interactable = false;
            return;
        }

        Settings.ballCount = Settings.ballCount + 1;

        ballCountText.text = Settings.ballCount.ToString();

        if(Settings.ballCount == Settings.ballCountMax)
        {
            higherBallCountButton.interactable = false;
        }

        if(lowerBallCountButton.interactable == false)
        {
            lowerBallCountButton.interactable = true;
        }

        SetBallCount(Settings.ballCount);
    }

    /// <summary>DecreaseBallCount is a Method in the SettingsManager Class.
    /// <para>This method decreases the current user setting for ballCount and updates the GUI to depict this change.</para>
    /// </summary>
    public void DecreaseBallCount()
    {
        if(Settings.ballCount == 1)
        {
            Debug.Log("Error: Button should be disabled, ballCount is already at minimum.");
            lowerBallCountButton.interactable = false;
            return;
        }

        Settings.ballCount = Settings.ballCount - 1;

        ballCountText.text = Settings.ballCount.ToString();

        if(Settings.ballCount == 1)
        {
            lowerBallCountButton.interactable = false;
        }

        if (higherBallCountButton.interactable == false)
        {
            higherBallCountButton.interactable = true;
        }

        SetBallCount(Settings.ballCount);
    }

    /// <summary>DecreaseScoreToWin is a Method in the SettingsManager Class.
    /// <para>This method decreases the current user setting for scoreToWin and updates the GUI to depict this change.</para>
    /// </summary>
    public void DecreaseScoreToWin()
    {
        if (Settings.scoreToWin == int.MinValue)
        {
            Debug.Log("Error: Button should be disabled, score is already at minimum.");
            lowerScoreToWinButton.interactable = false;
            return;
        }

        if (Settings.scoreToWin == int.MaxValue)
        {
            Settings.scoreToWin = Settings.maxScoreToWin;
            scoreToWinInfinity.SetActive(false);
            scoreToWinText.SetActive(true);
        }
        else if(Settings.scoreToWin == 1)
        {
            Settings.scoreToWin = int.MinValue;
            lowerScoreToWinButton.interactable = false;
            scoreToWinText.SetActive(false);
            scoreToWinInfinity.SetActive(true);
        }
        else
        {
            Settings.scoreToWin = Settings.scoreToWin - 1;
        }

        scoreToWinText.GetComponent<TextMeshProUGUI>().text = Settings.scoreToWin.ToString();

        if (higherScoreToWinButton.interactable == false)
        {
            higherScoreToWinButton.interactable = true;
        }

        SetScoreToWin(Settings.scoreToWin);
    }

    /// <summary>DecreaseTime is a Method in the SettingsManager Class.
    /// <para>This method decreases the current user setting for time and updates the GUI to depict this change.</para>
    /// </summary>
    public void DecreaseTime()
    {
        if (Settings.time == int.MinValue)
        {
            Debug.Log("Error: Button should be disabled, time is already at minimum.");
            lowerTimeButton.interactable = false;
            return;
        }

        if (Settings.time == int.MaxValue)
        {
            Settings.time = Settings.maxTimeToWin;
            timeInfinity.SetActive(false);
            timeText.SetActive(true);
        }
        else if (Settings.time == 1)
        {
            Settings.time = int.MinValue;
            lowerTimeButton.interactable = false;
            timeText.SetActive(false);
            timeInfinity.SetActive(true);
        }
        else
        {
            Settings.time = Settings.time - 1;
        }

        timeText.GetComponent<TextMeshProUGUI>().text = Settings.time.ToString();

        if (higherTimeButton.interactable == false)
        {
            higherTimeButton.interactable = true;
        }

        SetTime(Settings.time);
    }

    /// <summary>DecreasePaddleSize is a Method in the SettingsManager Class.
    /// <para>This method decreases the current user setting for paddleSize and updates the GUI to depict this change.</para>
    /// </summary>
    public void DecreasePaddleSize()
    {
        if(Settings.paddleSize == Settings.paddleSizeOptions.Values[0])
        {
            Debug.Log("Error: Button should be disabled, paddle size is already at minimum.");
            lowerPaddleSizeButton.interactable = false;
            return;
        }

        int currentPaddleSizeIndex = Settings.paddleSizeOptions.IndexOfValue(Settings.paddleSize);
        Settings.paddleSize = Settings.paddleSizeOptions.Values[currentPaddleSizeIndex - 1];

        if(Settings.paddleSize == Settings.paddleSizeOptions.Values[0])
        {
            lowerPaddleSizeButton.interactable = false;
        }

        paddleSizeText.text = Settings.paddleSize;

        if(higherPaddleSizeButton.interactable == false)
        {
            higherPaddleSizeButton.interactable = true;
        }

        SetPaddleSize(Settings.paddleSize);
    }

    /// <summary>DecreaseBallSize is a Method in the SettingsManager Class.
    /// <para>This method decreases the current user setting for ballSize and updates the GUI to depict this change.</para>
    /// </summary>
    public void DecreaseBallSize()
    {
        if (Settings.ballSize == Settings.ballSizeOptions.Values[0])
        {
            Debug.Log("Error: Button should be disabled, ball size is already at minimum.");
            lowerBallSizeButton.interactable = false;
            return;
        }

        int currentBallSizeIndex = Settings.ballSizeOptions.IndexOfValue(Settings.ballSize);
        Settings.ballSize = Settings.ballSizeOptions.Values[currentBallSizeIndex - 1];

        if (Settings.ballSize == Settings.ballSizeOptions.Values[0])
        {
            lowerBallSizeButton.interactable = false;
        }

        ballSizeText.text = Settings.ballSize;

        if (higherBallSizeButton.interactable == false)
        {
            higherBallSizeButton.interactable = true;
        }

        SetBallSize(Settings.ballSize);
    }

    /// <summary>DecreaseDifficulty is a Method in the SettingsManager Class.
    /// <para>This method decreases the current user setting for difficulty and updates the GUI to depict this change.</para>
    /// </summary>
    public void DecreaseDifficulty()
    {
        if(Settings.difficulty == Settings.difficultyOptions.Values[Settings.difficultyOptions.Count-1])
        {
            Debug.Log("Error: Button should be disabled, difficulty is already at minimum.");
            lowerDifficultyButton.interactable = false;
            return;
        }

        int currentDifficulty = Settings.difficultyOptions.IndexOfValue(Settings.difficulty);

        Settings.difficulty = Settings.difficultyOptions.Values[currentDifficulty + 1];

        if(Settings.difficulty == Settings.difficultyOptions.Values[Settings.difficultyOptions.Count-1])
        {
            lowerDifficultyButton.interactable = false;
        }

        difficultyText.text = Settings.difficulty;

        if(higherDifficultyButton.interactable == false)
        {
            higherDifficultyButton.interactable = true;
        }

        SetDifficulty(Settings.difficulty);
    }

    /// <summary>SetScoreToWin is a Method in the SettingsManager Class.
    /// <para>This method sets the current setting for scoreToWin in PlayerPrefs.</para>
    /// </summary>
    /// <param name="newScoreToWin"></param>
    public void SetScoreToWin(int newScoreToWin)
    {
        PlayerPrefs.SetInt("scoreToWin", newScoreToWin);
    }

    /// <summary>SetPaddleSize is a Method in the SettingsManager Class.
    /// <para>This method sets the current setting for paddleSize in PlayerPrefs.</para>
    /// </summary>
    public void SetPaddleSize(string newPaddleSize)
    {
        PlayerPrefs.SetString("paddleSize", newPaddleSize);
    }

    /// <summary>SetBallSize is a Method in the SettingsManager Class.
    /// <para>This method sets the current setting for ballSize in PlayerPrefs.</para>
    /// </summary>
    public void SetBallSize(string newBallSize)
    {
        PlayerPrefs.SetString("ballSize", newBallSize);
    }

    /// <summary>SetTime is a Method in the SettingsManager Class.
    /// <para>This method sets the current setting for time in PlayerPrefs.</para>
    /// </summary>
    public void SetTime(int newTime)
    {
        PlayerPrefs.SetInt("time", newTime);
    }

    /// <summary>SetDifficulty is a Method in the SettingsManager Class.
    /// <para>This method sets the current setting for difficulty in PlayerPrefs.</para>
    /// </summary>
    public void SetDifficulty(string newDifficulty)
    {
        PlayerPrefs.SetString("difficulty", newDifficulty);
    }

    /// <summary>SetBallCount is a Method in the SettingsManager Class.
    /// <para>This method sets the current setting for ballCount in PlayerPrefs.</para>
    /// </summary>
    public void SetBallCount(int newBallCount)
    {
        PlayerPrefs.SetInt("ballCount", newBallCount);
    }
}


