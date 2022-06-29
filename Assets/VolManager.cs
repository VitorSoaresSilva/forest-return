using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolManager : MonoBehaviour
{
    FMOD.Studio.EventInstance SFXVolumeTestEvent;

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float MasterVolume = 1f;

    void Awake()
    {
        //Music = FMODUnity.RunTimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RunTimeManager.Get("bus:/Master/SFX");
        Master = FMODUnity.RunTimeManager.GetBus("bus:/Master");
        //SFXVolumeTestEvent = FMODUnity.RunTimeManager.CreateInstance("event:/SFX/SFXVolumeTeste");
    }

    void update()
    {
        Music.setVolume (MusicVolume);
        SFX.setVolume (SFXVolume);
        Master.setVolume (MasterVolume);
    }

    public void MasterVolumeLevel (float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel (float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel (float newSFXVolume)
    {
        SFXVolume = newSFXVolume;

        FMOD.Studio.PLAYBACK_STATE PbState;
        SFXVolumeTestEvent.getPlaybackState (out PbState);
        if(PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start ();
        }
    }
}
