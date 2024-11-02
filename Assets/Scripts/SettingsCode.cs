using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsCode : MonoBehaviour
{
    public AudioMixer BgmMixer;
    public AudioMixer SfxMixer;

    public void SetBGMVolume(float volume)
    {
        BgmMixer.SetFloat("Volume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SfxMixer.SetFloat("Volume", volume);
    }
}
