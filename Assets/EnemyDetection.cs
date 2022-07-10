using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    Player player;
    List<Creep> creepList = new List<Creep>();
    Creep currentCreep = null;
    public Object select;
    Object currentSelect;
    // Start is called before the first frame update
    void Start()
    {
        currentSelect = GameObject.Find("select");
        player = gameObject.GetComponentInParent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (creepList.Count != 0)
        {
            Creep temp = null;
            foreach (Creep creep in creepList)
            {
                if (temp == null) temp = creep;
                else if (temp.health > creep.health) temp = creep;

            }
            InstantiateObject(temp);
            currentCreep = temp;
            player.collider = currentCreep.GetComponent<Collider>();

        }
        else if (currentSelect != null) Destroy(currentSelect);
       
    }

    private void OnTriggerEnter(Collider other)
    {
       if  (other.gameObject.tag == "creep")
        {
            Creep creep = other.GetComponent<Creep>();
            creep.playersNearby.Add(this);
            creepList.Add(creep);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "creep")
        {
            Creep creep = other.GetComponent<Creep>();
            if (creep == currentCreep) currentCreep = null;
            creep.playersNearby.Remove(this);
            RemoveFromList(creep);
         
        }
    }
    public void RemoveFromList(Creep creep)
    {
        if (currentCreep == creep) currentCreep = null;
       // if (currentSelect != null) Destroy(currentSelect);
        if (creepList.Contains(creep))
        {
            creepList.Remove(creep);
        }
    }
    void InstantiateObject(Creep creep)
    {
        if (currentSelect != null) Destroy(currentSelect);
        Vector3 pos = new Vector3(creep.transform.position.x, creep.transform.position.y, creep.transform.position.z);
        currentSelect = Instantiate(select, pos, Quaternion.Euler(-90,0,0));
    }
}
