using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickActivator : MonoBehaviour
{
    [SerializeField] private GameObject joystickVertical;
    [SerializeField] private GameObject joystickHorizontal;
    
    void Awake()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            joystickVertical.SetActive(true);
            joystickHorizontal.SetActive(true);
        }
    }

}
