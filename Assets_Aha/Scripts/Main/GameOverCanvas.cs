using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    public void Retry()
    {
        SceneController.LoadScene(GameScenes.Scenes.Main);
    }

    public void GoToStageSelect()
    {
        SceneController.LoadScene(GameScenes.Scenes.StageSelect);
    }
}
