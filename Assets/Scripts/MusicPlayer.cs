using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private string track;
    [SerializeField] private float fadeDuration = 1f;

    private string startLevel;

    void Start()
    {
        if (AudioManager.instance.IsPlaying(track))
        {
            return;
        }

        startLevel = SceneManager.GetActiveScene().name;

        DontDestroyOnLoad(gameObject);

        AudioManager.instance.Play(track);
        SceneManager.activeSceneChanged += SceneChanged;
    }

    void SceneChanged(Scene s1, Scene s2)
    {
        if (startLevel == s2.name) return;
        AudioManager.instance.Stop(track, fadeDuration);
        startLevel = SceneManager.GetActiveScene().name;
        Destroy(gameObject);
    }
}
