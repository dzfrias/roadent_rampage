using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LootLocker.Requests;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool useLootLocker = true;

    public static GameManager instance;
    public static event Action onFinishLineReached;
    public static event Action<string> onBroadcastText;
    public static event Action onClearBroadcast;
    public static event Action<bool> onDarken;
    public GameObject player { get; private set; }

    [HideInInspector]
    public bool isPaused;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (useLootLocker)
        {
            StartCoroutine(ActivateLootLocker());
        }
    }

    IEnumerator ActivateLootLocker()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Error logging player in");
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }

    public void Unpause()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Finish()
    {
        onFinishLineReached?.Invoke();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = false;
    }

    public void BroadcastText(string text)
    {
        onBroadcastText?.Invoke(text);
    }

    public void ClearBroadcast()
    {
        onClearBroadcast?.Invoke();
    }

    public void Darken()
    {
        onDarken?.Invoke(true);
    }

    public void Undarken()
    {
        onDarken?.Invoke(false);
    }
}
