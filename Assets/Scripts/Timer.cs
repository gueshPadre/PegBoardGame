using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject startCanvas = null;
    public float timer { get; private set; }
    bool hasStarted = false;    //indicates when the timer has started


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
}
