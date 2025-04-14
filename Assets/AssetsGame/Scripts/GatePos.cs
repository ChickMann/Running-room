using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GatePos : MonoBehaviour
{
    [SerializeField] bool right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

        if (right)
        {
            switch (Camera.main.pixelWidth)
            {
                case 800:
                    transform.position = new Vector3(28, 0, 0);
                    break;
                case 1280 :
                    transform.position = new Vector3(30, 0, 0);
                    break;
                case 1920 :
                    transform.position = new Vector3(30, 0, 0);
                    break;
                case 2160:
                    transform.position = new Vector3(34, 0, 0);
                    break;
                case 642:
                    transform.position = new Vector3(30, 0, 0);
                    break;
                case 722:
                    transform.position = new Vector3(34, 0, 0);
                    break;
                case 2960:
                    transform.position = new Vector3(35, 0, 0);
                    break;
                case 2560:
                    transform.position = new Vector3(30, 0, 0);
                    break;
            }
        }
        else
        {
            switch (Camera.main.pixelWidth)
            {
                case 800:
                    transform.position = new Vector3(-28, 0, 0);
                    break;
                case 1280:
                    transform.position = new Vector3(-30, 0, 0);
                    break;
                case 1920:
                    transform.position = new Vector3(-30, 0, 0);
                    break;
                case 2160:
                    transform.position = new Vector3(-34, 0, 0);
                    break;
                case 642:
                    transform.position = new Vector3(-30, 0, 0);
                    break;
                case 722:
                    transform.position = new Vector3(-34, 0, 0);
                    break;
                case 2960:
                    transform.position = new Vector3(-35, 0, 0);
                    break;
                case 2560:
                    transform.position = new Vector3(-30, 0, 0);
                    break;
            }
        }
        
    }
}
