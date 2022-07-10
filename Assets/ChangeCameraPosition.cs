using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class ChangeCameraPosition : MonoBehaviour
{
    CinemachineVirtualCamera camera;
    CinemachineFramingTransposer screen;
    Slider x;
    Slider y;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        x = GameObject.Find("CameraXSlider").GetComponent<Slider>();
        y = GameObject.Find("CameraYSlider").GetComponent<Slider>();
        screen = camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        x.value = screen.m_ScreenX;
        y.value = screen.m_ScreenY;
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (y.value != screen.m_ScreenY)
        screen.m_ScreenY = y.value;
        if (x.value != screen.m_ScreenX)
            screen.m_ScreenX = x.value;
    }
}
