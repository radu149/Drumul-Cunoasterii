using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointerScript : MonoBehaviour
{
    public GameObject PointerArrow;

    public Vector3 PositionA = new Vector3(-1f, 1.25f, 6.35f);
    public Vector3 PositionB = new Vector3(-36.69f, 1.25f, 6.11f);
    public Vector3 PositionC = new Vector3(-78.05f, 1.25f, 6.11f);
    public Vector3 PositionD = new Vector3(-78.05f, -18.577f, 20.52f);
    public Vector3 startPos;    

    public float ArrowSpeed = 0.25f;  
    public float TargetUpY = 2f;
    public float TargetDownY = 1f;
    public bool ArrowAllowedUp = true;

    public float height = 1.2f;
    public float speed = 2f; 


    void Start()
    {
        PointerArrow.SetActive(true);
        startPos = PointerArrow.transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height;
        PointerArrow.transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);

//        if(ArrowAllowedUp == true && PointerArrow.transform.position.y <= TargetDownY)
//        {
//        PointerArrow.transform.position = new Vector3(transform.position.x, transform.position.y+ArrowSpeed, transform.position.z);
//        }
//        if(PointerArrow.transform.position.y >= TargetUpY)
//        {
//        PointerArrow.transform.position = new Vector3(transform.position.x, transform.position.y-ArrowSpeed, transform.position.z);
//        ArrowAllowedUp = false;
//        }
    }

    public void TeleportPlace1()
    {
        PointerArrow.transform.position = PositionA;
        PointerArrow.transform.rotation = Quaternion.Euler(-90f, 0, 0f);
        startPos = PositionA;
    }

    public void TeleportPlace2()
    {
        PointerArrow.transform.position = PositionB;
        PointerArrow.transform.rotation = Quaternion.Euler(-90f, -90f, 0f); 
        startPos = PositionB;      
    }

    public void TeleportPlace3()
    {
        PointerArrow.transform.position = PositionC;
        startPos = PositionC;
    }

    public void TeleportPlace4()
    {
        PointerArrow.transform.position = PositionD;
        PointerArrow.transform.rotation = Quaternion.Euler(-90f, 0, 0f);    
        startPos = PositionD;  
    }

    public void FinalArrowDelete()
    {
        PointerArrow.SetActive(false);
    }
}
