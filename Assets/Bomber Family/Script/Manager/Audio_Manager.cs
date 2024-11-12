using UnityEngine;
using System.Collections;

public class Audio_Manager : Singletion<Audio_Manager>
{
    [SerializeField] private AudioClip menuClip;
    [SerializeField] private AudioClip gameClip;
    [SerializeField] private AudioClip goldChance;
    [SerializeField] private AudioClip gameStart;
    [SerializeField] private AudioClip gameSuccess;
    [SerializeField] private AudioClip gameFailed;
    [SerializeField] private AudioClip gameDrop;
    [SerializeField] private AudioClip gamePatlama;
    [Space]
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource gameUISource;
    [SerializeField] private AudioSource gameDropSource;
    [SerializeField] private AudioSource gameStartFinishSource;
    [SerializeField] private AudioSource gameExplodedSource;

    #region Background Fonksiyonlarý
    //public void OfferSound()
    //{
    //    StartCoroutine(ChanceMusic(offerClip));
    //}
    public void MenuSound()
    {
        StartCoroutine(ChanceMusic(menuClip));
    }
    public void GameSound()
    {
        StartCoroutine(ChanceMusic(gameClip));
    }
    //public void RoomSound()
    //{
    //    StartCoroutine(ChanceMusic(roomClip));
    //}
    IEnumerator ChanceMusic(AudioClip newClip)
    {
        bool chancedMusic = false;
        int chancedDirection = -1;
        while (!chancedMusic)
        {
            yield return null;
            if (chancedDirection == -1)
            {
                if (backgroundSource.volume > 0)
                {
                    backgroundSource.volume += chancedDirection * Time.deltaTime;
                }
                else
                {
                    chancedDirection = 1;
                    backgroundSource.clip = newClip;
                    backgroundSource.Play();
                }
            }
            else
            {
                if (backgroundSource.volume < 1)
                {
                    backgroundSource.volume += chancedDirection * Time.deltaTime;
                }
                else
                {
                    chancedMusic = true;
                }
            }
        }
    }
    #endregion

    #region Play Fonksiyonlarý
    public void PlayGoldChance()
    {
        gameUISource.clip = goldChance;
        gameUISource.Play();
    }
    public void PlayGameStart()
    {
        gameStartFinishSource.clip = gameStart;
        gameStartFinishSource.Play();
    }
    public void PlayGameSuccess()
    {
        gameStartFinishSource.clip = gameSuccess;
        gameStartFinishSource.Play();
    }
    public void PlayGameFailed()
    {
        gameStartFinishSource.clip = gameFailed;
        gameStartFinishSource.Play();
    }
    public void PlayGameDrop()
    {
        gameDropSource.clip = gameDrop;
        gameDropSource.Play();
    }
    public void PlayGamePatlama()
    {
        gameExplodedSource.clip = gamePatlama;
        gameExplodedSource.Play();
    }
    #endregion

    #region Set Fonksiyonlarý
    public void SetAllSourceMusic(bool isAllMusicSourceMute)
    {
        backgroundSource.mute = isAllMusicSourceMute;
        gameUISource.mute = isAllMusicSourceMute;
        gameDropSource.mute = isAllMusicSourceMute;
        gameStartFinishSource.mute = isAllMusicSourceMute;
        gameExplodedSource.mute = isAllMusicSourceMute;
    }
    public void SetBackgroundSourcMusic(bool isBackgroundSourceMute)
    {
        backgroundSource.mute = isBackgroundSourceMute;
    }
    public void SetUISourceMusic(bool isUISourceMute)
    {
        gameUISource.mute = isUISourceMute;
    }
    public void SetDropSourceMusic(bool isDropSourceMute)
    {
        gameDropSource.mute = isDropSourceMute;
    }
    public void SetStartFinishSourceMusic(bool isStartFinishSourceMute)
    {
        gameStartFinishSource.mute = isStartFinishSourceMute;
    }
    public void SetExplodedSourceMusic(bool isExplodedSourceMute)
    {
        gameExplodedSource.mute = isExplodedSourceMute;
    }
    #endregion
}