using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToMove : MonoBehaviour
{
    private Touch touch;
    private float speedModifier;

    // Start is called before the first frame update
    void Start()
    {
        speedModifier = 0.001f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1){
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved){
                transform.position += new Vector3(touch.deltaPosition.x * speedModifier, touch.deltaPosition.y * speedModifier, touch.deltaPosition.y * speedModifier); 
            }
        }
    } 
}
