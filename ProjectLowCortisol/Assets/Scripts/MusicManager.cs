using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicMainMenu;
    [SerializeField] AudioSource musicGame;

    [SerializeField] Slider sliderMusic;

    public float sfxVolumne;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        musicGame.Stop();
    }
    private void Update()
    {
        if (sliderMusic != null)
        {
            UpdateVolumeMusic(sliderMusic.value);
            UpdateVolumeSFX(sliderMusic.value);
        }
    }
    public float GetSFXVolume()
    {
        return sfxVolumne;
    }

    void UpdateVolumeMusic(float volume)
    {
        musicMainMenu.volume = volume;
        musicGame.volume = volume;
    }
    void UpdateVolumeSFX(float volume)
    {
        sfxVolumne = volume;
    }

    public void PlayMusicGame()
    {
        musicGame.Play();
    }
    public void StopMusicGame() 
    {
        musicGame.Stop();
    }
    public void PlayMusicMenu()
    {
        musicMainMenu.Play();
    }
    public void StopMusicMenu() 
    {
        musicMainMenu.Stop();
    }


}
