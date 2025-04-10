using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PauseMenuPlayModeTests
{
    [UnityTest] //make sure the game starts unpaused and UI elements are hidden
    public IEnumerator GameStarts_UnpausedAndPanelsInactive()
    {
        var pauseMenu = new GameObject().AddComponent<pause_menu>();
        pauseMenu.PausePanel = new GameObject();
        pauseMenu.Dark_Overlay = new GameObject();
    // since pauseMenu is not public pauseMenu.Start() won't work use this instead
        pauseMenu.PausePanel.SetActive(false);
        pauseMenu.Dark_Overlay.SetActive(false);

        yield return null;

        Assert.IsFalse(pause_menu.isPaused);
        Assert.IsFalse(pauseMenu.PausePanel.activeSelf);
        Assert.IsFalse(pauseMenu.Dark_Overlay.activeSelf);
    }

[UnityTest] //test 2, pausing the game
public IEnumerator PauseGame_ActivatesUIAndPausesTime()
{
    var pauseObject = new GameObject("PauseMenuObject");
    var pauseMenu = pauseObject.AddComponent<pause_menu>();

    var pausePanel = new GameObject("PausePanel");
    var darkOverlay = new GameObject("DarkOverlay");

    pausePanel.transform.SetParent(pauseObject.transform);
    darkOverlay.transform.SetParent(pauseObject.transform);

    pauseMenu.PausePanel = pausePanel;
    pauseMenu.Dark_Overlay = darkOverlay;

    pauseObject.SetActive(true);
    pausePanel.SetActive(false);
    darkOverlay.SetActive(false);

    
    pauseMenu.PauseGame();

   

    // trying to see why it keeps failing
    Assert.IsTrue(pausePanel.activeSelf, "PausePanel was not activated.");
    Assert.IsTrue(darkOverlay.activeSelf, "DarkOverlay was not activated.");
    Assert.IsTrue(pause_menu.isPaused, "Pause state not set.");
    Assert.AreEqual(0f, Time.timeScale, "Time did not pause.");
    yield return null;
}


    [UnityTest] //test 3, resume game should hide the ui and resume game time
    public IEnumerator ResumeGame_DeactivatesUIAndResumesTime()
    {
        var pauseMenu = new GameObject().AddComponent<pause_menu>();
        pauseMenu.PausePanel = new GameObject();
        pauseMenu.Dark_Overlay = new GameObject();

        pauseMenu.PauseGame();
        yield return null;

        pauseMenu.ResumeGame();
        yield return null;

        Assert.IsFalse(pauseMenu.PausePanel.activeSelf); 
        Assert.IsFalse(pauseMenu.Dark_Overlay.activeSelf);
        Assert.IsFalse(pause_menu.isPaused);
        Assert.AreEqual(1f, Time.timeScale);
    }

    [UnityTest] //test 4, manually toggle the reume to confirm state change logic
    public IEnumerator TogglingPause_ChangesPauseStateCorrectly()
    {
        var pauseMenu = new GameObject().AddComponent<pause_menu>();
        pauseMenu.PausePanel = new GameObject();
        pauseMenu.Dark_Overlay = new GameObject();

        pauseMenu.PausePanel.SetActive(false);
        pauseMenu.Dark_Overlay.SetActive(false);


        pauseMenu.PauseGame();
        yield return null;

        Assert.IsTrue(pause_menu.isPaused);

        pauseMenu.ResumeGame();
        yield return null;

        Assert.IsFalse(pause_menu.isPaused);
    }

    [UnityTest] //test 5, loadMainMenu should reset the pause state and unpause the game, so the user isn't stuck on pause in the main menu
    public IEnumerator LoadMainMenu_ResetsPauseStateAndTime()
    {
        var pauseMenu = new GameObject().AddComponent<pause_menu>();
        pauseMenu.PausePanel = new GameObject();
        pauseMenu.Dark_Overlay = new GameObject();

        pause_menu.isPaused = true;
        Time.timeScale = 0f;

        pauseMenu.loadMainMenu();

        yield return null;

        Assert.IsFalse(pause_menu.isPaused);
        Assert.AreEqual(1f, Time.timeScale);
    }
}

