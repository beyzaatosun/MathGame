using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PuanManager : MonoBehaviour
{

    private int toplamPuan;
    private int puanArtisi;
    
    [SerializeField] private TMP_Text puanText;
    
    void Start()
    {
        puanText.text = toplamPuan.ToString();

    }

    public void PuanArtir(int zorlukSeviyesi)
    {
        puanArtisi = 5 * zorlukSeviyesi;
        toplamPuan += puanArtisi;
        puanText.text = toplamPuan.ToString();

    }

}
