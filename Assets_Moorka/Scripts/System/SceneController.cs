using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : SingletonMonoBehaviour<SceneController>
{
    [SerializeField] List<string> sceneList = new List<string>();
    int sceneNumber = 1;

    public void NextScene()
    {
        if (sceneNumber >= sceneList.Count) sceneNumber = 0;
        SceneManager.LoadScene(sceneList[sceneNumber]);
        sceneNumber++;
    }
}
