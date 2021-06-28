using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject startCanvas = null;
    public float timer { get; private set; }
    bool hasStarted = false;    //indicates when the timer has started



    void Update()
    {
        if (hasStarted)
        {
            timer += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        hasStarted = true;
        startCanvas.gameObject.SetActive(false);
    }

    public void StopTimer()
    {
        hasStarted = false;
        timer = 0f;
    }

}
