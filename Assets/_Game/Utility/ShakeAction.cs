using System;
using FMODUnity;
using FMOD.Studio;
using UnityEngine;

public class ShakeAction : MonoBehaviour
{
    public bool startShake;
    public bool endShake;

    public float shakeStrength;
    private float shakeStrengthTime;
    
    private EventInstance shakeSoundEvent;

    public static event Action<int> OnShake;

    private void Start()
    {
        shakeStrengthTime = Time.time + shakeStrength * 100 * Time.deltaTime;
        shakeSoundEvent = RuntimeManager.CreateInstance("event:/SFX/BasicShake");
        shakeSoundEvent.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
    }

    private void Update()
    {
        if (endShake)
        {
            if (Time.time < shakeStrengthTime)
            {
                var i = FindObjectOfType<Hand>().GetComponentInChildren<Ingredient>();
                if (i != null) OnShake(i.ingNum);
            }

            endShake = false;
            shakeStrengthTime = Time.time + shakeStrength * 100 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        startShake = false;
        endShake = true;
        
        // Play a sound when during changes
        PLAYBACK_STATE playbackState;
        shakeSoundEvent.getPlaybackState(out playbackState);
        if (playbackState != PLAYBACK_STATE.PLAYING)
        {
            shakeSoundEvent.start();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        startShake = true;
        endShake = false;    
    }

    private void OnDestroy()
    {
        shakeSoundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
