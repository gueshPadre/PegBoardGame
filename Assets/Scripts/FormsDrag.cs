using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FormsDrag : MonoBehaviour
{

    private float radiusDifficulty;
    public bool IsInList { get; private set; }   //Trigger to know if it's in the list or not

    bool isFollowing = false;   // Trigger to know when an object is following the mouse
    Vector3 initPos;        // Position at start
    Vector3 properPos;      // Position that should be locked at

    Vector3 initScale;      // Scale at start

    static List<FormsDrag> pegs = new List<FormsDrag>();    //keep track of the number of pegs we have
    Vector3 worldMousePos;

    // Start is called before the first frame update
    void Start()
    {
        initScale = transform.localScale;
        pegs.Add(this);         //Add every peg to the list
        IsInList = true;

        //Set radius difficulty
        radiusDifficulty = GameManager.Instance.difficulty;
    }


    private void OnMouseDown()
    {
        initPos = transform.position;
        isFollowing = true;
    }

    //Drop object when releasing the mouse click
    private void OnMouseUp()
    {
        isFollowing = false;
        if (CheckIfRightShape())
        {
            HandleCorrectConnect();
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.soundClip.wrong);
            transform.position = initPos;   // return to clicked position
        }
    }

    // Handles level two mechanic when done hovering over a form
    private void OnMouseExit()
    {
        //object is in correct place
        if (!isFollowing && IsInList)
        {
            transform.localScale = initScale;
        }
    }

    private void Update()
    {
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void LateUpdate()
    {
        if (isFollowing)
        {
            transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);   //Keeps the object at the same depth it was
        }
    }

    /// <summary>
    /// Makes sure that when the player releases the mouse button, it's over the right shape
    /// </summary>
    bool CheckIfRightShape()
    {
        int myLayerMask = ~LayerMask.GetMask("Default");    //exclude my own layerMask

        // Get all colliders and check if there's one that's a good fit
        Collider2D[] otherColliders = Physics2D.OverlapCircleAll(transform.position, radiusDifficulty, myLayerMask);

        //touching something AND touching the right tag (shape)
        for (int i = 0; i < otherColliders.Length; i++)
        {
            if (otherColliders[i].gameObject.tag == this.tag)
            {
                properPos = otherColliders[i].transform.position;
                otherColliders[i].enabled = false;      // Disable its collider so nothing else can match at the same place
                return true;
            }
        }
        return false;   //touching the wrong layer (shape)
    }


    /// <summary>
    /// Locks in the position, make it not clickable anymore and play the right sfx
    /// </summary>
    void HandleCorrectConnect()
    {
        // Set the right position and scale
        transform.position = properPos;
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Collider2D>().enabled = false;
        RemoveFromList();     // Remove the Peg from the list

        // Play the right sound effect
        if (pegs.Count <= 0)
        {
            SoundManager.Instance.PlaySound(SoundManager.soundClip.success);
            GameManager.Instance.ShowEndGameScreen();
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.soundClip.correct);
        }

    }


    /// <summary>
    /// Removes the peg from the list
    /// </summary>
    public void RemoveFromList()
    {
        pegs.Remove(this);
        IsInList = false;
    }

    /// <summary>
    /// To be able to see the radius of the difficulty
    /// </summary>
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(transform.position, radiusDifficulty);
    //}

}
