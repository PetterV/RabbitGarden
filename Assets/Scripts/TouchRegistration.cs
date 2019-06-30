using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRegistration : MonoBehaviour
{
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject TopButton;
    public GameObject BottomButton;

    public bool touchInputActive = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!touchInputActive)
            {
                ActivateTouchInput();
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            DeactivateTouchInput();
        }
    }

    void ActivateTouchInput()
    {
        touchInputActive = true;
        LeftButton.SetActive(true);
        RightButton.SetActive(true);
        TopButton.SetActive(true);
        BottomButton.SetActive(true);
    }
    void DeactivateTouchInput()
    {
        touchInputActive = false;
        LeftButton.SetActive(false);
        RightButton.SetActive(false);
        TopButton.SetActive(false);
        BottomButton.SetActive(false);
    }
}
