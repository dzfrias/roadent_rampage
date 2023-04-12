using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;
    private int levelToLoad;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeToLevel(int index)
    {
        levelToLoad = index;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
