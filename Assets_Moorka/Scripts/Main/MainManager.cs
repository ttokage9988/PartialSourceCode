using Cysharp.Threading.Tasks;
using UnityEngine;
public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject taskCanvas;

    //無効化切り替え
    [SerializeField] PlayerMover playerMover;
    [SerializeField] MouseController mouseController;
    [SerializeField] CursorController cursorController;

    [SerializeField] AudioSource _introAudioSource;
    [SerializeField] AudioSource _loopAudioSource;

    private async void Start()
    {
        //イントロ終了後にループ部分の再生を開始
        _introAudioSource.Play();
        await UniTask.WaitUntil(() => _introAudioSource.isPlaying == false);
        _loopAudioSource.Play();
    }

    public void ActiveTaskCanvas(GameObject taskCube = null)
    {
        taskCanvas.SetActive(true);
        if (taskCube != null)
        {
            taskCanvas.GetComponent<TaskCanvasManager>().CurrentTaskCube = taskCube;
        }
    }

    public void EnableUIMode()
    {
        playerMover.enabled = false;
        mouseController.enabled = false;
        cursorController.CursorEnable();
    }

    public void DisableUIMode()
    {
        playerMover.enabled = true;
        mouseController.enabled = true;
        cursorController.CursorDisable();
    }

    public void NextScene()
    {
        SceneController.Instance.NextScene();
    }
}
