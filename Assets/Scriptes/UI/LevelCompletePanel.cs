using UnityEngine;

public class LevelCompletePanel : GamePanel
{
    protected override void Action()
    {
        Level.LoadNextLevel();
    }
}
