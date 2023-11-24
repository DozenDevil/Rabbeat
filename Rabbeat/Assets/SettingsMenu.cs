using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource music;

    public void SetVolume(float volume)
    {
        music.volume = volume;
    }

    public void MuteVolume()
    {
        if(music.mute)
            music.mute = false;
        else
            music.mute = true;
    }
}
