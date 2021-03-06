﻿using FMODUnity;
using UnityEngine;

public class MainMusic : MonoBehaviour
{
    [EventRef] public string mainMusicSelection;
    private FMOD.Studio.EventInstance mainMusicEvent;
    
    void Start()
    {
        mainMusicEvent = RuntimeManager.CreateInstance(mainMusicSelection);
        mainMusicEvent.setParameterByName("songSelect", 0f, true);
        mainMusicEvent.start();
        mainMusicEvent.release();
    }

    public void ChangeInGameSpiceLevel(string spice, float value)
    {
        mainMusicEvent.setParameterByName(spice, value, true);
    }

    public void ChangeEndGame(float value)
    {
        mainMusicEvent.setParameterByName("endGame", value, true);
    }
    
    public void ChangeSongSelection(float songNum)
    {
        mainMusicEvent.setParameterByName("songSelect", songNum, true);
    }

    private void OnDestroy()
    {
        mainMusicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}