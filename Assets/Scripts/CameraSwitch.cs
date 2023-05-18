using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] float time;

    public void SwitchWrap()
    {
        StartCoroutine(Switch());
    }

    private IEnumerator Switch()
    {
        cinemachineVirtualCamera.enabled = true;
        yield return new WaitForSeconds(time);
        cinemachineVirtualCamera.enabled = false;
    }
}
