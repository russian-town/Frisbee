using UnityEngine;

public class GameOverPanel : GamePanel
{
    protected override void Action()
    {
        Level.ReloadCurrentLevel();
    }
}
