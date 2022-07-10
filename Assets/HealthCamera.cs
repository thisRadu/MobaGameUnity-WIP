using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCamera : MonoBehaviour
{
    public int x = 0;
    public int y = 180;
    public int z = 0;

    // Start is called before the first frame update
    void Start()
    {
      x = 0;
      y = 180;
      z = 0;

}

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(x, y, z);
    }
}
