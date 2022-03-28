using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{   
    public LayerMask targetMask;
    public SpriteRenderer dot; // The dot in the middle of the crosshair
    public Color dotColor; // Original dot color
    public Color dotEmiColor; 

    void Start()
    {   
        Cursor.visible = false; // Hide the cursor
        dotColor = dot.color;
    }

    // Update is called once per frame
    void Update()
    {   
        // Keep crosshait rotate
        transform.Rotate(Vector3.forward * 40 * Time.deltaTime);

    }

    public void DetectTarget(Ray ray)
    {   
        // If the dot on the target, high light the dot
        if(Physics.Raycast(ray, 100, targetMask)){
            dot.color = dotEmiColor;
        }
        else{
            dot.color = dotColor;
        }
    }
}
