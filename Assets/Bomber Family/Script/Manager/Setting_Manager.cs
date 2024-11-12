using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Setting_Manager : Singletion<Setting_Manager>
{
    private void Start()
    {
        // All Music Volume
        LearnAllMusic();

        // BackGround Music Volume
        LearnBackGroundMusic();

        // UI Music Volume
        LearnUIMusic();

        // Drop Music Volume
        LearnDropMusic();

        // StartFinish Music Volume
        LearnStartFinishMusic();

        // Exploded Music Volume
        LearnExplodedMusic();
    }
    #region Audio
    [SerializeField] private AudioMixer allMusicMixer;

    #region AllMusic
    [SerializeField] private Slider allMusicSlider;
    private void LearnAllMusic()
    {
        allMusicSlider.value = PlayerPrefs.GetInt("AllMusic", 0);
        allMusicSlider.onValueChanged.AddListener((x) => AllMusicMusic(x));
        SetAllSourceMusic(PlayerPrefs.GetInt("AllMusicSource", 1) == 0 ? true : false);
    }
    private void AllMusicMusic(float volume)
    {
        allMusicMixer.SetFloat("AllMusic", volume);
        PlayerPrefs.SetInt("AllMusic", (int)volume);
    }
    // Setting panelinde all music music açma kapama butonlarýnda atandý.
    public void SetAllSourceMusic(bool isAllMusicSourceMute)
    {
        PlayerPrefs.SetInt("AllMusicSource", isAllMusicSourceMute ? 0 : 1);
        Audio_Manager.Instance.SetAllSourceMusic(isAllMusicSourceMute);
    }
    #endregion

    #region Background
    [SerializeField] private Slider backGroundSlider;
    private void LearnBackGroundMusic()
    {
        if (backGroundSlider != null)
        {
            backGroundSlider.value = PlayerPrefs.GetInt("BackGroundMusic", 0);
            backGroundSlider.onValueChanged.AddListener((x) => BackGroundMusic(x));
            SetBackgroundSourcMusic(PlayerPrefs.GetInt("BackgroundSource", 1) == 0 ? true : false);
        }
    }
    private void BackGroundMusic(float volume)
    {
        allMusicMixer.SetFloat("BackGround", volume);
        PlayerPrefs.SetInt("BackGroundMusic", (int)volume);
    }
    // Setting panelinde background music açma kapama butonlarýnda atandý.
    public void SetBackgroundSourcMusic(bool isBackgroundSourceMute)
    {
        PlayerPrefs.SetInt("BackgroundSource", isBackgroundSourceMute ? 0 : 1);
        Audio_Manager.Instance.SetBackgroundSourcMusic(isBackgroundSourceMute);
    }
    #endregion

    #region UI
    [SerializeField] private Slider uISlider;
    private void LearnUIMusic()
    {
        if (uISlider != null)
        {
            uISlider.value = PlayerPrefs.GetInt("UIMusic", 0);
            uISlider.onValueChanged.AddListener((x) => UIMusic(x));
            SetUISourceMusic(PlayerPrefs.GetInt("UISource", 1) == 0 ? true : false);
        }
    }
    private void UIMusic(float volume)
    {
        allMusicMixer.SetFloat("UI", volume);
        PlayerPrefs.SetInt("UIMusic", (int)volume);
    }
    // Setting panelinde uI music açma kapama butonlarýnda atandý.
    public void SetUISourceMusic(bool isUISourceMute)
    {
        PlayerPrefs.SetInt("UISource", isUISourceMute ? 0 : 1);
        Audio_Manager.Instance.SetUISourceMusic(isUISourceMute);
    }
    #endregion

    #region Drop
    [SerializeField] private Slider dropSlider;
    private void LearnDropMusic()
    {
        if (dropSlider != null)
        {
            dropSlider.value = PlayerPrefs.GetInt("DropMusic", 0);
            dropSlider.onValueChanged.AddListener((x) => DropMusic(x));
            SetDropSourceMusic(PlayerPrefs.GetInt("DropSource", 1) == 0 ? true : false);
        }
    }
    private void DropMusic(float volume)
    {
        allMusicMixer.SetFloat("Drop", volume);
        PlayerPrefs.SetInt("DropMusic", (int)volume);
    }
    // Setting panelinde drop music açma kapama butonlarýnda atandý.
    public void SetDropSourceMusic(bool isDropSourceMute)
    {
        PlayerPrefs.SetInt("DropSource", isDropSourceMute ? 0 : 1);
        Audio_Manager.Instance.SetDropSourceMusic(isDropSourceMute);
    }
    #endregion

    #region Start Finish
    [SerializeField] private Slider startFinishSlider;
    private void LearnStartFinishMusic()
    {
        if (startFinishSlider != null)
        {
            startFinishSlider.value = PlayerPrefs.GetInt("StartFinishMusic", 0);
            startFinishSlider.onValueChanged.AddListener((x) => StartFinishMusic(x));
            SetStartFinishSourceMusic(PlayerPrefs.GetInt("StartFinishSource", 1) == 0 ? true : false);
        }
    }
    private void StartFinishMusic(float volume)
    {
        allMusicMixer.SetFloat("StartFinish", volume);
        PlayerPrefs.SetInt("StartFinishMusic", (int)volume);
    }
    // Setting panelinde startFinish music açma kapama butonlarýnda atandý.
    public void SetStartFinishSourceMusic(bool isStartFinishSourceMute)
    {
        PlayerPrefs.SetInt("StartFinishSource", isStartFinishSourceMute ? 0 : 1);
        Audio_Manager.Instance.SetStartFinishSourceMusic(isStartFinishSourceMute);
    }
    #endregion

    #region Exploded
    [SerializeField] private Slider explodedSlider;
    private void LearnExplodedMusic()
    {
        if (explodedSlider != null)
        {
            explodedSlider.value = PlayerPrefs.GetInt("ExplodedMusic", 0);
            explodedSlider.onValueChanged.AddListener((x) => ExplodedMusic(x));
            SetExplodedSourceMusic(PlayerPrefs.GetInt("ExplodedSource", 1) == 0 ? true : false);
        }
    }
    private void ExplodedMusic(float volume)
    {
        allMusicMixer.SetFloat("Exploded", volume);
        PlayerPrefs.SetInt("ExplodedMusic", (int)volume);
    }
    // Setting panelinde exploded music açma kapama butonlarýnda atandý.
    public void SetExplodedSourceMusic(bool isExplodedSourceMute)
    {
        PlayerPrefs.SetInt("ExplodedSource", isExplodedSourceMute ? 0 : 1);
        Audio_Manager.Instance.SetExplodedSourceMusic(isExplodedSourceMute);
    }
    #endregion
    #endregion

    #region Vibration
    //public static bool canVibration;
    //[SerializeField] private Toggle vibrationToggle;
    //private void LearnVibration()
    //{
    //    if (canMoveUIToggle != null)
    //    {
    //        canVibration = PlayerPrefs.GetInt("canVibration", canVibration ? 0 : 1) == 0 ? true : false;
    //        vibrationToggle.isOn = canVibration;
    //    }
    //}
    //public void ChangeVibration(bool isActive)
    //{
    //    canVibration = isActive;
    //    PlayerPrefs.SetInt("canVibration", canVibration ? 0 : 1);
    //}
    #endregion

    #region Move UI
    //public static bool canMoveUI;
    //[SerializeField] private Toggle canMoveUIToggle;
    //private void LearnMoveUI()
    //{
    //    if (canMoveUIToggle != null)
    //    {
    //        canMoveUI = PlayerPrefs.GetInt("canMoveUI", canMoveUI ? 0 : 1) == 0 ? true : false;
    //        canMoveUIToggle.isOn = canMoveUI;
    //    }
    //}
    //public void ChangeMoveUI(bool isActive)
    //{
    //    canMoveUI = isActive;
    //    PlayerPrefs.SetInt("canMoveUI", canMoveUI ? 0 : 1);
    //}
    #endregion

    #region Language
    //public static int languageNumber;
    //[SerializeField] private TMP_Dropdown languageDropdown;
    //private void LearnLanguage()
    //{
    //    if (languageDropdown != null)
    //    {
    //        languageNumber = PlayerPrefs.GetInt("languageNumber", languageNumber);
    //        languageDropdown.value = languageNumber;
    //    }
    //}
    //public void ChangeLanguage(int langNumber)
    //{
    //    languageNumber = langNumber;
    //    PlayerPrefs.SetInt("languageNumber", languageNumber);
    //}
    #endregion
}