using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource music;
    public Slider volumeSlider;
    public Toggle muteToggle;

    static bool valuesSet = false;

    public static class Settings
    {
        public static float volume_value { get; set; }
        public static bool mute_state { get; set; }
        public static float sfx_volume { get; set; }
    }

    private void SetDefaults()
    {
        if (!valuesSet)
        {
            Settings.volume_value = 1f;
            Settings.sfx_volume = 1f;
            Settings.mute_state = false;
            valuesSet = true;
        }
    }

    private void LoadSettings()
    {
        volumeSlider.value = Settings.volume_value;
        muteToggle.isOn = Settings.mute_state;
    }

    private void Start()
    {
        SetDefaults();

        LoadSettings();
    }

    public void SetVolume(float volume)
    {
        Settings.volume_value = volume;
        music.volume = Settings.volume_value;
    }

    public void MuteVolume()
    {
        if(music.mute)
        {
            Settings.mute_state = false;
            music.mute = false;
        }
        else
        {
            Settings.mute_state = true;
            music.mute = true;
        }        
    }
}
