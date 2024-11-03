using UnityEngine;
using UnityEngine.UI;


public class SaveCode : MonoBehaviour
{
    public int player_progress; // -1 if player has no progress, 0 = tutorial, 1,2,3 = beacons
    public float BGMvol;
    public Slider bgmSlider;
    public float SFXvol;
    public Slider sfxSlider;
    public GameObject continueBtn;

    // 3 things to save in this game : SFX volume, BGM volume, Player progress
    private void Awake()
    {
        player_progress = PlayerPrefs.GetInt("PlayerProgress", -1);
        BGMvol = PlayerPrefs.GetFloat("BGMvol", 0);
        SFXvol = PlayerPrefs.GetFloat("SFXvol", 0);
    }

    void Start()
    {
        if (player_progress < 1)
        {
            continueBtn.SetActive(false);
        }
        bgmSlider.value = BGMvol;
        sfxSlider.value = SFXvol;
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
