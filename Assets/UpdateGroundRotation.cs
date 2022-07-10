using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGroundRotation : MonoBehaviour
{
    int x = 0;
    // Start is called before the first frame update
    void Start()
    {

        Rotate(-10);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    private void OnTriggerEnter(Collider other)
    {
        x -= 5;
        Debug.LogError(x);
        Rotate(x);
    }
    private void OnTriggerStay(Collider other)
    {
        x -= 5;
        Rotate(x);
    }

    public void Rotate(int x)
    {
        Debug.Log("test");
        Debug.LogError("rotate" + x);
        transform.Rotate(x, 0, 0);
    }
}
