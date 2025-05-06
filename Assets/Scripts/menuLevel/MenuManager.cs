using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup startGroup, exitGroup;
    void Start()
    {
        FadeOut();
    }

    // Update is called once per frame
    void FadeOut()
    {
        startGroup.DOFade(1, 0.8f);
        exitGroup.DOFade(1, 0.8f).SetDelay(0.5f);


    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene("gameLevel");
    }
}
