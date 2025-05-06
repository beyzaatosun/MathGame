using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sonucManager : MonoBehaviour
{
    private Button a;

    public void OyunaYenidenBasla()
    {
        
        
        SceneManager.LoadScene("gameLevel");
    }

    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("menuLevel");
    }
}
