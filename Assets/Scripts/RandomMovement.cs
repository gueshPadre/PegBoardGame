using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [Tooltip("Movement speed of this form")]
    [SerializeField] float speed = 3f;

    Vector3 nextPos;
    FormsDrag form;


    void Start()
    {
        nextPos = FindRandomPos();
        form = GetComponent<FormsDrag>();       // Get reference to FormsDrag script
    }

    
    void Update()
    {
        if (form.IsInList)
        {
            // Move to the next random position
            if (transform.position != nextPos)
                transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.deltaTime * speed);
            else
                nextPos = FindRandomPos();
        }
    }


    /// <summary>
    /// Finds a new target random Position within the boundaries
    /// </summary>
    /// <returns>the random position</returns>
    Vector3 FindRandomPos()
    {
        //Set Boundaries
        float minX = 1.5f;
        float maxX = 8.5f;
        float minY = -4.5f;
        float maxY = 4.5f;

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        return new Vector3(x, y);
    }

}
