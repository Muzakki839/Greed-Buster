using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip tapCardBGM;
    [SerializeField] private AudioClip gameBGM;
    [SerializeField] private AudioClip winBGM;
    [SerializeField] private AudioClip loseBGM;
    [SerializeField] private AudioClip inputNameBGM;
    [SerializeField] private AudioClip leaderboardBGM;

    private SerialMessageHandler serialMessageHandler;

    private void Update()
    {
        serialMessageHandler = SerialMessageHandler.Instance;
        switch (serialMessageHandler.gameState)
        {
            case SerialMessageHandler.GameState.TapCard:
                PlayMusic(tapCardBGM);
                break;

            case SerialMessageHandler.GameState.Game:
                PlayMusic(gameBGM);
                break;

            case SerialMessageHandler.GameState.Win:
                PlayMusic(winBGM);
                break;

            case SerialMessageHandler.GameState.Lose:
                PlayMusic(loseBGM);
                break;

            case SerialMessageHandler.GameState.InputName:
                PlayMusic(inputNameBGM);
                break;

            case SerialMessageHandler.GameState.Leaderboard:
                PlayMusic(leaderboardBGM);
                break;
        }

    }

    private void PlayMusic(AudioClip music)
    {
        if (music == null)
        {
            Debug.LogError("Music clip is not assigned.");
            return;
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing.");
            return;
        }

        if (audioSource.clip != music || !audioSource.isPlaying)
        {
            audioSource.clip = music;
            audioSource.Play();
        }
    }
}
