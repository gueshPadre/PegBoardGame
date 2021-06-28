using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;


    private void Start()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void GoToNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex +1);
    }

}
