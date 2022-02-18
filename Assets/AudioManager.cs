using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance;

    [SerializeField] private AK.Wwise.Event themeSong;
    [SerializeField] private AK.Wwise.Event clockTick;

    private uint themeSongID;
    private bool isPlaying = false;
    private void Awake()
    {
        AudioManagerInstance = this;
    }

    private void Start()
    {
        if (themeSong.IsValid())
        {
            themeSong.Post(gameObject);
            // themeSongID = AkSoundEngine.PostEvent("MusicMenu", gameObject);
            isPlaying = true;
        }
        if (clockTick.IsValid())
        {
            clockTick.Post(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // themeSong.Stop(gameObject,3000,AkCurveInterpolation.AkCurveInterpolation_Linear);
            // AkSoundEngine.StopPlayingID(themeSongID,3000,AkCurveInterpolation.AkCurveInterpolation_Linear);
            // Debug.Log("Stopped");
            if (isPlaying)
            {
                themeSong.ExecuteAction(gameObject, AkActionOnEventType.AkActionOnEventType_Pause, 500, AkCurveInterpolation.AkCurveInterpolation_Log1);
                Debug.Log("Paused");
            }
            else
            {
                themeSong.ExecuteAction(gameObject, AkActionOnEventType.AkActionOnEventType_Resume, 500, AkCurveInterpolation.AkCurveInterpolation_Log1);
                Debug.Log("Resumed");
            }

            isPlaying = !isPlaying;
        }
        if (GameManager.timer < 5.0f && GameManager.timer > 0.0f)
        {
            AkSoundEngine.SetState("Time", "Danger");
        }
        else
        {
            AkSoundEngine.SetState("Time", "Safe");
        }
    }
}
