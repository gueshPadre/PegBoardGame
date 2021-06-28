using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;


    private void Start()
    {
        //Singleton pattern
        DontDestroyOnLoad(this);
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
        // Only go to next level if we're on the first level
        if (currentIndex == 0)
        {
            SceneManager.LoadScene(currentIndex + 1); 
        }
    }

}
