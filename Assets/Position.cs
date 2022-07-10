using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public GameObject character;
    public float posX = 20f;
    public float posZ = 20f;
    public float posY = 10f;
    public float rotX = 60f;
    public float rotZ = 5f;
    public float rotY = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = new Vector3(rotX, rotY, rotZ);
        Quaternion q = Quaternion.Euler(rot);
        transform.rotation = q;
        Vector3 position = new Vector3(character.transform.position.x + posX, character.transform.position.y + posY, character.transform.position.z + posZ);
        transform.position = position;
       // transform.position = Vector3.Slerp(transform.position, position, 2f);
    }
}
