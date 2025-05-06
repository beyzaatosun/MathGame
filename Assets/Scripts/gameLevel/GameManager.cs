using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject karePrefab;
    [SerializeField] private Transform karelerPaneli;

    [SerializeField] private Transform soruPaneli;
    [SerializeField] private TMP_Text soruText;
    private GameObject[] karelerDizisi = new GameObject[25];
    List<int> bolumDegerleriListesi = new List<int>();
    int kacinciSoru;

    int bolunenSayi, bolenSayi;
    int butonDegeri;
    bool butonaBasilsinMi;
    int dogruSonuc;
    int kalanHak;
    private int sorununZorlukDerecesi;
    [SerializeField] KalanHaklarManager kalanHaklarManager;
    [SerializeField] PuanManager puanManager;
    private GameObject gecerliKare;
    [SerializeField] private Sprite[] kareSprites;

    [SerializeField] private GameObject sonucPaneli;
    private void Awake()
    {
        kalanHak = 3;
        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        
        kalanHaklarManager.KalanHaklariKontrolEt(kalanHak);
    }
    void Start()

    {
        butonaBasilsinMi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        kareleriOlustur();
    }

    public void kareleriOlustur()
    {
        for (int i =0; i < 25; i++)
        {
            GameObject kare = Instantiate(karePrefab,karelerPaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[Random.Range(0, kareSprites.Length)];
            kare.transform.GetComponent<Button>().onClick.AddListener(ButonaBasildi);
            karelerDizisi[i] = kare;
        }
        BolumDegerleriniTexteYazdir();
        StartCoroutine(DoFadeRoutine());
        
        Invoke("SoruPaneliniAc",2f);
    }

    void ButonaBasildi()
    {
        if (butonaBasilsinMi)
        {
            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TMP_Text>().text);
            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            SonucuKontrolEt();
        }
        
    }

    void SonucuKontrolEt()
    {
        if (butonDegeri == dogruSonuc)
        {
            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerliKare.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            gecerliKare.transform.GetComponent<Button>().interactable = false;
            puanManager.PuanArtir(sorununZorlukDerecesi);
            bolumDegerleriListesi.RemoveAt(kacinciSoru);

            if (bolumDegerleriListesi.Count > 0)
            {
                SoruPaneliniAc();

            }
            else
            {
                OyunBitti();            
            }
            
            SoruPaneliniAc();
        }
        else
        
        {
                kalanHak--;
                kalanHaklarManager.KalanHaklariKontrolEt(kalanHak);
        }

        if (kalanHak <= 0)
        {
            OyunBitti();
            
        }
        
    }

    void OyunBitti()
    {
        butonaBasilsinMi = false;
        sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in karelerDizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.07f);
        }
    }

    void BolumDegerleriniTexteYazdir()
    {
        foreach (var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(1, 13);
            bolumDegerleriListesi.Add(rastgeleDeger);
            kare.transform.GetChild(0).GetComponent<TMP_Text>().text = rastgeleDeger.ToString();
        }
    }

    void SoruPaneliniAc()
    {
        SoruyuSor();
        butonaBasilsinMi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
        
    }

    void SoruyuSor()
    {
        bolenSayi = Random.Range(2, 11);
        kacinciSoru = Random.Range(0, bolumDegerleriListesi.Count);
        dogruSonuc = bolumDegerleriListesi[kacinciSoru];
        bolunenSayi = bolenSayi * dogruSonuc;
        if (bolunenSayi <= 40)
        {
            sorununZorlukDerecesi = 1;
        }
        else if (bolunenSayi > 40 && bolunenSayi <= 80)
        {
            sorununZorlukDerecesi = 2;
        }
        else
        {
            sorununZorlukDerecesi = 3;
        }
        
        soruText.text = bolunenSayi.ToString()+ " : "+bolenSayi.ToString();
    }
}
