using UnityEngine;

// Dummy death screen to stop real DeathScreen stuff during tests
public class FakeDeathScreen : DeathScreen
{
    public override void ShowDeathScreen() { }
    public override void ReturnToHub() { }
    public override void ReturnToMainMenu() { }
}


