using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIOptions : MonoBehaviour
{
    private int volume = 2;
    private int sfx = 8;

    [SerializeField] private TextMeshProUGUI volumeLabel;
    [SerializeField] private TextMeshProUGUI sfxLabel;

    private void Start()
    {
        volumeLabel.text = volume.ToString();
        sfxLabel.text = sfx.ToString();
    }

    public void ChangeMusicVolume(int valueChange)
    {
        volume += valueChange;
        volume = Mathf.Clamp(volume, 0, 10); //para no poder pasar del rango 0-10
        volumeLabel.text = volume.ToString();
        AudioManager.Instance.ChangeAudioMusic(volume);
    }

    public void ChangeSFXVolume(int valueChange)
    {
        sfx += valueChange;
        sfx = Mathf.Clamp(sfx, 0, 10);
        sfxLabel.text = sfx.ToString();
        AudioManager.Instance.ChangeAudioSFX(sfx);
    }
}
