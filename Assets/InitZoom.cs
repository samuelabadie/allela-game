using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InitZoom : MonoBehaviour
{
    [SerializeField] float startZoomValue;
    [SerializeField] float finalZoomValue;
    [SerializeField] float zoomSpeed;
    private bool alreadyZoom = false;
    private float step;

    [SerializeField] CinemachineVirtualCamera cvc;

    // Start is called before the first frame update
    void Start()
    {
        cvc = GetComponent<CinemachineVirtualCamera>();
        cvc.m_Lens.OrthographicSize = startZoomValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (alreadyZoom == false)
        {
            if (cvc.m_Lens.OrthographicSize < finalZoomValue)
            {
                zoomSpeed = 0;
                alreadyZoom =true;
            }

            step = zoomSpeed * Time.deltaTime;
            print(step);
            cvc.m_Lens.OrthographicSize = cvc.m_Lens.OrthographicSize - step;
            print(cvc.m_Lens.OrthographicSize);
        }


      
    }
}
