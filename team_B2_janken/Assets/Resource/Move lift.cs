using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CubeMove : MonoBehaviour
{
    private float speed = 10.0f;
    private bool flag;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
 
    void Update()
    {
        if(flag)
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        
        else if(!flag)
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
    }
 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PointA")
            flag = true;
        
        else if (other.gameObject.name == "PointB")
            flag = false;
    }
}