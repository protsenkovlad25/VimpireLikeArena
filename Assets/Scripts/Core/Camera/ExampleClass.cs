using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    Camera cam;
    Vector3 pos1;
    Vector3 pos2;


    void Start()
    {
        cam = GetComponent<Camera>();

        pos1 = new Vector3(0, cam.pixelHeight / 2, 0);
        pos2 = new Vector3(cam.pixelWidth, cam.pixelHeight / 2, 0);
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(pos1);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        
        Ray ray2 = cam.ScreenPointToRay(pos2);
        Debug.DrawRay(ray2.origin, ray2.direction * 100, Color.yellow);
    }
}
