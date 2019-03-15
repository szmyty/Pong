///-----------------------------------------------------------------
///   Class:       UserAccountManager
///   Description: This class is responsible for managing the user account GUI page.
///   Author:      Keywi
///-----------------------------------------------------------------

using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserAccountManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject canvasParent;

    public TMP_Text playerOneChoice;
    public TMP_Text playerTwoChoice;

    public GameObject playerOneUnderline;
    public GameObject playerTwoUnderline;

    public TMP_Text feedback;
    public TMP_InputField inputField;

    public GameObject deleteUserPrompt;
    public GameObject exitButton;
    public GameObject yesButton;
    public GameObject noButton;
    public TMP_Text userText;
    public TMP_Text feedbackPrompt;

    const int PLAYER_1 = 0;
    const int PLAYER_2 = 1;

    bool wasDuplicate = false;

    bool isPlayer1 = true;
    bool isPlayer2 = false;

    /// <summary>Start is a Method in the UserAccountManager Class.
    /// <para>This method initializes the player choice text fields and generates all of the user buttons that have been loaded.</para>
    /// </summary>
    void Start()
    {
        playerOneChoice.text = Users.users[Users.playerOneCurrentUserIndex].GetUserName();
        playerTwoChoice.text = Users.users[Users.playerTwoCurrentUserIndex].GetUserName();
        GenerateUserButtons();
        SetButtonInteractables();
    }

    /// <summary>CreateNewUser is a Method in the UserAccountManager Class.
    /// <para>This method creates a new user with the name given in the input field and gives feedback if there is no name or the name has reached the character limit.</para>
    /// </summary>
    public void CreateNewUser()
    {
        if(inputField.text == "")
        {
            ShowFeedback("Please Enter a Username.");
        }
        else
        {
            AddUserAccount(inputField.text);           
            inputField.text = "";
            ShowFeedback("");
        }
    }

    /// <summary>CharacterLimit is a Method in the UserAccountManager Class.
    /// <para>This method reports to the user in the feedback text field that they have reached the character limit for a new username.</para>
    /// </summary>
    public void CharacterLimit()
    {
        if (inputField.text.Count() == inputField.characterLimit)
        {
            ShowFeedback("Character limit reached.\n(15 characters)");
        }
        else
        {
            ShowFeedback("");
        }
    }

    /// <summary>ShowFeedback is a Method in the UserAccountManager Class.
    /// <para>This method takes text as a parameter and sets the feedback text to be that text.</para>
    /// </summary>
    /// <param name="text"></param>
    void ShowFeedback(string text)
    {
        feedback.text = text;
    }

    /// <summary>AddUserAccount is a Method in the UserAccountManager Class.
    /// <para>This method adds a new user given the new user's name and calls fix duplicates to check to make sure that the name isn't already a user. It then saves the user.</para>
    /// </summary>
    /// <param name="newUser"></param>
    public void AddUserAccount(string newUser)
    {
        Users.users.Add(new User(newUser));
        FixDuplicates();
        if (!wasDuplicate)
        {
            AddUserButton(Users.users[Users.users.Count - 1]);
        }
        else
        {
            wasDuplicate = false;
        }

        UsersInitializer.SaveUserAccounts();
    }
    
    /// <summary>GenerateUserButtons is a Method in the UserAccountManager Class.
    /// <para>This method generates a button for all of the loaded users.</para>
    /// </summary>
    public void GenerateUserButtons()
    {
        foreach(User user in Users.users)
        {
            AddUserButton(user);
        }
    }

    /// <summary>AddUserButton is a Method in the UserAccountManager Class.
    /// <para>This method adds a new button with a callback function and the text of the username.</para>
    /// </summary>
    /// <param name="user"></param>
    public void AddUserButton(User user)
    {
        GameObject userButton = Instantiate(buttonPrefab);
        /* Adds hover effect to each button. */
        userButton.AddComponent<OnHover>();
        userButton.GetComponent<OnHover>().scaleSize = 1.05f;

        userButton.transform.SetParent(canvasParent.transform, false);
        userButton.transform.localScale = new Vector3(1, 1, 1);

        TMP_Text buttonText = userButton.GetComponentInChildren<TMP_Text>();
        userButton.name = user.GetUserName();

        buttonText.text = user.GetUserName();

        Button tempButton = userButton.GetComponent<Button>();

        /* Adds an event listener to the button. */
        tempButton.onClick.AddListener(() => SetCurrentUserName(user.GetUserName()));
    }

    /// <summary>SetCurrentUserName is a Method in the UserAccountManager Class.
    /// <para>This method is the callback function for all user buttons. It sets the player's current user choice.</para>
    /// </summary>
    /// <param name="userName"></param>
    public void SetCurrentUserName(string userName)
    {
        int userIndex = Users.users.FindIndex(user => user.GetUserName() == userName);

        if (isPlayer1)
        {
            Users.playerOneCurrentUserIndex = userIndex;
            playerOneChoice.text = Users.users[userIndex].GetUserName();
        }
        else
        {
            Users.playerTwoCurrentUserIndex = userIndex;
            playerTwoChoice.text = Users.users[userIndex].GetUserName();
        }

        SetButtonInteractables();
    }

    /// <summary>SetButtonInteractables is a Method in the UserAccountManager Class.
    /// <para>This method disables and enables the appropriate buttons. It disables only user buttons that already have been chosen.</para>
    /// </summary>
    public void SetButtonInteractables()
    {
        string playerOneUserName = Users.users[Users.playerOneCurrentUserIndex].GetUserName();
        string playerTwoUserName = Users.users[Users.playerTwoCurrentUserIndex].GetUserName();

        foreach (Transform child in canvasParent.transform)
        {
            string buttonName = child.GetComponent<Button>().name;
            if (buttonName == playerOneUserName || buttonName == playerTwoUserName)
            {
                child.GetComponent<Button>().interactable = false;
            }
            else
            {
                child.GetComponent<Button>().interactable = true;
            }
        }
    }

    /// <summary>PlayerOneClick is a Method in the UserAccoutManager Class.
    /// <para>This method just focuses on player one to be the player currently choosing.</para>
    /// </summary>
    public void PlayerOneClick()
    {
        if (!isPlayer1)
        {
            isPlayer1 = true;
        }

        if (isPlayer2)
        {
            isPlayer2 = false;
        }

        if (!playerOneUnderline.activeInHierarchy)
        {
            playerOneUnderline.SetActive(true);
        }

        if (playerTwoUnderline.activeInHierarchy)
        {
            playerTwoUnderline.SetActive(false);
        }
    }

    /// <summary>PlayerTwoClick is a Method in the UserAccoutManager Class.
    /// <para>This method just focuses on player two to be the player currently choosing.</para>
    /// </summary>
    public void PlayerTwoClick()
    {
        if (!isPlayer2)
        {
            isPlayer2 = true;
        }

        if (isPlayer1)
        {
            isPlayer1 = false;
        }

        if (!playerTwoUnderline.activeInHierarchy)
        {
            playerTwoUnderline.SetActive(true);
        }

        if (playerOneUnderline.activeInHierarchy)
        {
            playerOneUnderline.SetActive(false);
        }
    }

    /// <summary>DeleteUserPrompt is a Method in the UserAccountManager Class.
    /// <para>This method toggles the delete user prompt.</para>
    /// </summary>
    public void DeleteUserPrompt()
    {
        if (isPlayer1)
        {
            userText.text = Users.users[Users.playerOneCurrentUserIndex].GetUserName() + "?";
        }
        else
        {
            userText.text = Users.users[Users.playerTwoCurrentUserIndex].GetUserName() + "?";
        }

        bool isActive = deleteUserPrompt.activeInHierarchy;

        if (!isActive)
        {
            ToggleButtons(false);
        }
        else
        {
            ToggleButtons(true);
        }

        feedbackPrompt.text = "";

        if (!yesButton.activeInHierarchy)
        {
            yesButton.SetActive(true);
        }

        if (!noButton.activeInHierarchy)
        {
            noButton.SetActive(true);
        }

        if (exitButton.activeInHierarchy)
        {
            exitButton.SetActive(false);
        }

        deleteUserPrompt.SetActive(!isActive);

        if (!deleteUserPrompt.activeInHierarchy)
        {
            SetButtonInteractables();
        }
    }

    /// <summary>ToggleButtons is a Method in the UserAccountManager Class.
    /// <para>This method turns off all buttons when the delete user prompt appears.</para>
    /// </summary>
    /// <param name="setting"></param>
    void ToggleButtons(bool setting)
    {
        Button[] allButtons = FindObjectsOfType<Button>();

        foreach (Button button in allButtons)
        {
            if (!(button.tag == "prompt"))
            {
                button.interactable = setting;
            }
        }
    }

    /// <summary>DeleteUser is a Method in the UserAccountManager Class.
    /// <para>This method deletes a user from the users list and updates the user list on the GUI. Also, if they try to delete Player1 or Player2, they get an error feedback message and are asked to exit.</para>
    /// </summary>
    public void DeleteUser()
    {
        if (isPlayer1) {
            if(Users.playerOneCurrentUserIndex == PLAYER_1)
            {
                CreateExitMenu("Player 1.");
                return;
            }
            else if(Users.playerOneCurrentUserIndex == PLAYER_2)
            {
                CreateExitMenu("Player 2.");
                return;
            }
            else
            {
                Users.users.RemoveAt(Users.playerOneCurrentUserIndex);
                playerOneChoice.text = Users.users[PLAYER_1].GetUserName();
            }
        }
        else
        {
            if (Users.playerTwoCurrentUserIndex == PLAYER_1)
            {
                CreateExitMenu("Player 1.");
                return;
            }
            else if (Users.playerTwoCurrentUserIndex == PLAYER_2)
            {
                CreateExitMenu("Player 2.");
                return;
            }
            else
            {
                Users.users.RemoveAt(Users.playerTwoCurrentUserIndex);
                playerTwoChoice.text = Users.users[PLAYER_2].GetUserName();
            }
        }

        DestroyUserButtons();
        GenerateUserButtons();

        ToggleButtons(true);
        feedbackPrompt.text = "";
        deleteUserPrompt.SetActive(false);
        UsersInitializer.SaveUserAccounts();
    }

    /// <summary>CreateExitMenu is a Method in the UserAccountManager Class.
    /// <para>This method shows the exit menu to the user.</para>
    /// </summary>
    /// <param name="player"></param>
    void CreateExitMenu(string player)
    {
        feedbackPrompt.text = "You can't delete " + player;
        yesButton.SetActive(false);
        noButton.SetActive(false);
        exitButton.SetActive(true);
    }

    /// <summary>DestroyUserButtons is a Method in the UserAccountManager Class.
    /// <para>This method destroys all of the user account buttons.</para>
    /// </summary>
    void DestroyUserButtons()
    {
        foreach(Transform child in canvasParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>LoadStats is a Method in the UserAccountManager Class.
    /// <para>This method sets a bool to see if the current user is player1 or player2 and will load those stats accordingly.</para>
    /// </summary>
    public void LoadStats()
    {
        if (isPlayer1)
        {
            Users.isPlayerOneStats = true;
        }
        else
        {
            Users.isPlayerOneStats = false;
        }
    }

    /// <summary>FixDuplicates is a Method in the UserAccountManager Class.
    /// <para>This method checks for duplicates in the users list and fixes them if it finds them.</para>
    /// </summary>
    public void FixDuplicates()
    {
        for (int i = 0; i < Users.users.Count; i++)
        {
            string userName = Users.users[i].GetUserName();
            int count = 1;

            int occurences = Users.users.Where(user => user.GetUserName().Equals(userName)).Count();
            if (occurences >= 2)
            {
                string tempUserName = "";
                while (occurences >= 2)
                {
                    tempUserName = string.Format("{0}({1})", userName, count++);
                    occurences = Users.users.Where(user => user.GetUserName().Equals(tempUserName)).Count() + 1;
                }
                userName = tempUserName;
                Users.users[i].SetUserName(userName);
                DestroyUserButtons();
                GenerateUserButtons();
                wasDuplicate = true;
            }
        }
    }
}
