using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FormsDrag : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("The accuracy you need for the form to be placed. The higher the easier")]
    public float radiusDifficulty = 1f;


    bool isFollowing = false; // Trigger to know when an object is following the mouse
    Type myCollider;     //Open type to check what collider exactly it hits
    Vector3 initPos;
    Vector3 properPos;


    Vector3 worldMousePos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        myCollider = GetComponent<Collider2D>().GetType(); // Get my Collider, no matter which one it is
    }


    private void OnMouseDown()
    {
        isFollowing = true;
    }

    //Drop object when releasing the mouse click
    private void OnMouseUp()
    {
        isFollowing = false;
        if (CheckIfRightShape())
        {
            HandleCorrectConnect();
            SoundManager.Instance.PlaySound(SoundManager.soundClip.correct);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.soundClip.wrong);
            transform.position = initPos;   // return to initial position
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
        //Collider2D otherCollider;

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
    /// Locks in the position and make it not clickable anymore
    /// </summary>
    void HandleCorrectConnect()
    {
        transform.position = properPos;
        GetComponent<Collider2D>().enabled = false;
    }



    /// <summary>
    /// To be able to see the radius of the difficulty
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radiusDifficulty);
    }

}
