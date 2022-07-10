using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creep : MonoBehaviour, IDamageable
{
    int damage;
    public int health;
    int xp;
    int gold;
    public Slider healthSlider;
    public List<EnemyDetection> playersNearby = null;
   public Vector3 position { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ICreep creep = gameObject.GetComponent<ICreep>();
        damage = creep.damage;
        health = creep.health;
        xp = creep.xp;
        gold = creep.gold;

        healthSlider.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {

        position = transform.position;
        healthSlider.value = health;
    }
    public void TakeDamage(IDamageable caster, int damage)
    {
        health -= damage;
        if (health <= 0) 
        {
            if (typeof(IDamageable).IsAssignableFrom(typeof(Player)))
            {
                caster.TakeXp(xp, gold);
            }
            Destroy(gameObject);
            foreach (EnemyDetection player in playersNearby)
            {
                player.RemoveFromList(this);
            }

        }
    }
    public void TakeXp(int xp, int gold)
    {
        //not necessary
    }

}
