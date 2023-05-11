using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] private string track;
    [SerializeField] private float fadeDuration = 1f;

    private static MusicPlayer instance;
    private string startLevel;

    void Awake()
    {
        if (instance == null)
        {
            AudioManager.instance.Play(track);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            
            AudioManager.instance.Stop(instance.track, fadeDuration);
            instance.track = track;
            AudioManager.instance.Play(instance.track);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
