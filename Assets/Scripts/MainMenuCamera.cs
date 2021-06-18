using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    

    public Camera camera;
    public float speed;
    void Update()
    {
        camera.transform.Rotate(0f, speed * Time.fixedDeltaTime, 0f);
    }
  
}
