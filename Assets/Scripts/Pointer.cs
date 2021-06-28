using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Enlarge the form when hovering over it with the mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Camera.main.transform.forward * 1000, 1000);
        if (hit)
        {
            hit.collider.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
    }
}
