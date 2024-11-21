using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip moveSFX;
    [SerializeField] private AudioClip missSFX;
    [SerializeField] private AudioClip matchSFX;
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnPointsUpdated.AddListener(PointsUpdated);
        GameManager.Instance.OnGameStateUpdated.AddListener(GameStateUpdated);
    }


    private void OnDestroy()
    {
        GameManager.Instance.OnPointsUpdated.RemoveListener(PointsUpdated);
        GameManager.Instance.OnGameStateUpdated.RemoveListener(GameStateUpdated);
    }

    private void GameStateUpdated(GameManager.GameState newState)
    {
        if(newState == GameManager.GameState.GameOver)
        {
            sfxSource.PlayOneShot(gameOverSFX);
        }

        if(newState == GameManager.GameState.InGame)
        {
            sfxSource.PlayOneShot(matchSFX);
        }
    }

    private void PointsUpdated()
    {
        sfxSource.PlayOneShot(matchSFX);
    }

    public void Move()
    {
        sfxSource.PlayOneShot(moveSFX);
    }

    public void Miss()
    {
        sfxSource.PlayOneShot(missSFX);
    }

    public void ChangeAudioSFX(int valueChange)
    {
        float newVolume = 0f;
        newVolume = (float)valueChange / 10;        
        sfxSource.volume = newVolume;
    }

    public void ChangeAudioMusic(int valueChange)
    {
        float newVolume = 0f;
        newVolume = (float)valueChange / 10;       
        musicSource.volume = newVolume;
    }
}
