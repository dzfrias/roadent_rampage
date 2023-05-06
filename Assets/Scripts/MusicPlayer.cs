using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private string track;

    void Start()
    {
        if (AudioManager.instance.IsPlaying(track))
        {
            return;
        }

        AudioManager.instance.Play(track);
        SceneManager.activeSceneChanged += SceneChanged;
    }

    void SceneChanged(Scene s1, Scene s2)
    {
        AudioManager.instance.Stop(track);
    }
}
