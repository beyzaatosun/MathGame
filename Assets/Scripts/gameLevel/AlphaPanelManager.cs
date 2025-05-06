using UnityEngine;
using DG.Tweening;

public class AlphaPanelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private CanvasGroup alphaPanelCanvas;
    
    void Start()
    {
        alphaPanelCanvas.DOFade(0,0.3f);

    }


}
