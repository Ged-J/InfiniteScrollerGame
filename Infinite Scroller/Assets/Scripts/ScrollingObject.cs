using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float backgroundSize = 18.75f; //width of background images
    private Transform cameraTransform; //camera follows player
    private Transform[] backgrounds; //holds transforms of images
    private float viewZone = 5;
    private int leftIndex;
    private int rightIndex;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        backgrounds = new Transform[transform.childCount]; 

        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = backgrounds.Length - 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTransform.position.x < (backgrounds[leftIndex].transform.position.x + viewZone))
            ScrollLeft();
        if (cameraTransform.position.x > (backgrounds[rightIndex].transform.position.x - viewZone))
            ScrollRight();

    }

    private void ScrollLeft()
    {
        Debug.Log("Scrolling Left");
        
        backgrounds[rightIndex].position = Vector3.right * (backgrounds[leftIndex].position.x - backgroundSize);
        
        leftIndex = rightIndex;
       
        rightIndex--;

        if (rightIndex == backgrounds.Length)
            rightIndex = backgrounds.Length - 1;

    }

    private void ScrollRight()
    {
        Debug.Log("Scrolling Right");
        //if scroll too far to the right, move the leftmost image to the right
        backgrounds[leftIndex].position = Vector3.right * (backgrounds[rightIndex].position.x + backgroundSize);
        // the rightmost image is the previous leftmost image
        rightIndex = leftIndex;
        //the leftmost image increments; it points to the next image in the sequence for scrolling right
        leftIndex++;

        if (leftIndex == backgrounds.Length)
            leftIndex = 0;

    }

}
