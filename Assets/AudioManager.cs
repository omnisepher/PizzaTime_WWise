using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance;

    [SerializeField] private AK.Wwise.Event themeSong;

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
                themeSong.ExecuteAction(gameObject,AkActionOnEventType.AkActionOnEventType_Pause,500,AkCurveInterpolation.AkCurveInterpolation_Log1);
                Debug.Log("Paused");
            }
            else
            {
                themeSong.ExecuteAction(gameObject,AkActionOnEventType.AkActionOnEventType_Resume,500,AkCurveInterpolation.AkCurveInterpolation_Log1);
                Debug.Log("Resumed");
            }

            isPlaying = !isPlaying;
        }
    }
}
