using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class GateContronller : MonoBehaviour
{
    [SerializeField] private GameObject doorWing;
    [SerializeField] public bool isOpen = false;
    [SerializeField] private LayerMask layerCharacter;
    private MeshRenderer doorRend;
    public int colorNumber = 0;
    MeshRenderer doorWingRend;
    private void Awake()
    {
        doorWingRend = doorWing.GetComponent<MeshRenderer>();
        doorRend = GetComponent<MeshRenderer>();
    }


    private void FixedUpdate()
    {
        StateManager();
        CheckPosToOpen();
    }

    void StateManager()
    {

        if (isOpen)
        {
            //open
            if (doorWing.transform.localEulerAngles.y >= 270)
            {
                doorWing.transform.localEulerAngles = new Vector3(0, 270, 0);
            }
            else
            {
                doorWing.transform.Rotate(Vector3.down * 30f);
            }
        }
        else
        {
            // close
            if (doorWing.transform.localEulerAngles.y < 270)
            {
                doorWing.transform.localEulerAngles = new Vector3(0, 360, 0);
            }
            else
            {
                doorWing.transform.Rotate(Vector3.up * 5f);
            }
        }
    }
    void CheckPosToOpen()
    {
        Vector3 dir = transform.localScale.z > 0 ? Vector3.left : Vector3.right;
        Vector3 posStart = dir == Vector3.left ? new Vector3(3, 0, 0) : new Vector3(-3, 0, 0);
        bool ray = Physics.Raycast(transform.position + posStart, dir, 10f, layerCharacter);
        Debug.DrawRay(transform.position + posStart, dir * 10, Color.green);
        isOpen = ray ? true : false;
    }
    public void SetColor(Material color, int colorNumber)
    {
        this.colorNumber = colorNumber;
        doorWingRend.material.color = color.color;
        doorRend.material.color = color.color;

    }
    public int GetColorNumber()
    {
        return colorNumber;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == 6)
        {
            int dirX = transform.localScale.z < 0 ? 1 : -1;
            Debug.Log(dirX);
            Transform doorPos = doorScript_Instance.TransDoor(dirX, colorNumber);
            collision.transform.position = doorPos.position + new Vector3(dirX, -2.5f, 0);

        }
    }

}

