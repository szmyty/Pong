using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    /// <summary>LoadGame is a Method in the SceneLoader Class.
    /// <para>Loads the Game Scene.</para>
    /// </summary>
    public void LoadGame()
    {
        StartCoroutine(LoadLevelAsync("Game"));
    }

    /// <summary>LoadSettings is a Method in the SceneLoader Class.
    /// <para>Loads the Settings Scene.</para>
    /// </summary>
    public void LoadSettings()
    {
        StartCoroutine(LoadLevelAsync("Settings"));
    }

    /// <summary>LoadMainMenu is a Method in the SceneLoader Class.
    /// <para>Loads the MainMenu Scene.</para>
    /// </summary>
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelAsync("MainMenu"));
    }

    /// <summary>LoadMoreSettings is a Method in the SceneLoader Class.
    /// <para>Loads the MoreSettings Scene.</para>
    /// </summary>
    public void LoadMoreSettings()
    {
        StartCoroutine(LoadLevelAsync("MoreSettings"));
    }

    /// <summary>LoadUserAccounts is a Method in the SceneLoader Class.
    /// <para>Loads the UserAccounts Scene.</para>
    /// </summary>
    public void LoadUserAccounts()
    {
        StartCoroutine(LoadLevelAsync("UserAccounts"));
    }

    /// <summary>LoadStats is a Method in the SceneLoader Class.
    /// <para>Loads the Stats Scene.</para>
    /// </summary>
    public void LoadStats()
    {
        StartCoroutine(LoadLevelAsync("Stats"));
    }

    /// <summary>LoadLevelAsync is a Coroutine in the SceneLoader Class.
    /// <para>Loads the given scene asynchronously.</para>
    /// </summary>
    IEnumerator LoadLevelAsync(string scene)
    {
        yield return new WaitForSeconds(0.5f);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
