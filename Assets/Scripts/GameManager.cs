using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject completedCanvas = null;
    [SerializeField] GameObject starPrefab = null;

    [Tooltip("The amount of time that determines how many stars player gets upon level completion")]
    [SerializeField] float timerThresholdBest, timerThresholdMid;

    [Range(0f, 1f)]
    [Tooltip("The accuracy you need for the form to be placed. The higher the easier")]
    public float difficulty = 0.75f;

    public static GameManager Instance;

    private GameObject canvasInst;  //the instance instantiated of the completedCanvas

    // Start is called before the first frame update
    void Start()
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


    /// <summary>
    /// Create the 'Completed' Canvas screen. Called from FormsDrag
    /// </summary>
    public void ShowEndGameScreen()
    {
        Timer time = GetComponent<Timer>();
        float timer = time.timer;
        canvasInst = Instantiate(completedCanvas);

        if (timer <= timerThresholdBest)
        {
            print("Get 3 stars with a time of: " + timer.ToString("##.##"));
            ShowStars(3);
        }
        else if (timer > timerThresholdBest && timer < timerThresholdMid)
        {
            print("Get 2 stars with " + timer.ToString("##.##"));
            ShowStars(2);
        }
        else
        {
            print("Get 1 star with " + timer.ToString("##.##"));
            ShowStars(1);
        }

        time.StopTimer();
    }

    /// <summary>
    /// Create and display the stars that the player got according to his time
    /// </summary>
    /// <param name="starNum">The amount of stars the player deserves</param>
    void ShowStars(int starNum)
    {
        Vector3 initPos = new Vector3(-100f, -90f);
        int index = 0;  // keep track of the iterations
        List<GameObject> stars = new List<GameObject>();    //list to keep track of the instantiated stars

        for (int x = -100; x < 300; x += 100)    //position in X of the stars
        {
            if(index >= 3) { break; }

            //Instantiate all three black stars
            GameObject starImage = Instantiate(starPrefab,initPos,Quaternion.identity);
            stars.Add(starImage);
            starImage.GetComponent<RectTransform>().SetParent(canvasInst.transform,false);
            Vector3 starPos = starImage.GetComponent<RectTransform>().localPosition;
            starPos.x = x;
            starImage.GetComponent<RectTransform>().localPosition = starPos;

            if (index < starNum )
            {
                stars[index].GetComponent<Image>().color = Color.white;        // Make the stars appear yellow
            }     
            index++;
        }
    }

}
