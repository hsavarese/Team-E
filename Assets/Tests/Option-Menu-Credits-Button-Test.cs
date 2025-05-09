/*using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class CreditsPanelTest
{
    [UnityTest]
    public IEnumerator CreditsButton_ActivatesCreditsPanel()
    {
        // 1. Create credits panel (initially inactive)
        var creditsPanel = new GameObject("CreditsPanel");
        creditsPanel.SetActive(false);

        // 2. Create the Credits button
        var buttonGO = new GameObject("CreditsButton");
        var button = buttonGO.AddComponent<Button>();

        // 3. Add an onClick listener that activates the panel
        button.onClick.AddListener(() => creditsPanel.SetActive(true));

        // 4. Simulate button click
        button.onClick.Invoke();
        yield return null;

        // 5. Assert the panel is now active
        Assert.IsTrue(creditsPanel.activeSelf);
    }
}*/