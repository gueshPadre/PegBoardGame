using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject completedCanvas = null;

    [Range(0f, 1f)]
    [Tooltip("The accuracy you need for the form to be placed. The higher the easier")]
    public float difficulty = 0.75f;

    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }


    /// <summary>
    /// Create the 'Completed' Canvas screen
    /// </summary>
    public void ShowEndGameScreen()
    {
        Instantiate(completedCanvas);
    }

}
