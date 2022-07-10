using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// each hero will have a similar class with his name (E.g: heroName)
public class HeroName : MonoBehaviour, IHero
{
    public float MovementSpeed { get; set; } = 5f;
    public float AttackRange { get; set; } = 1.3f;
    public int Health { get; set; } = 600;
    public int Mana { get; set; } = 400;
    public int Damage { get; set; } = 50;

    public float AttackSpeed { get; set; } = 300f;
    public Vector3 position { get; set; }

    Player player;
    //  i should create a class for the below
    int skill1CD = 10;
    int skill2CD = 20;
    int skill3CD = 30;
    int skill4CD = 40;
    float castTimerSkill1;
    float castTimerSkill2;
    float castTimerSkill3;
    float castTimerSkill4;
    int skill1ManaCost = 50;
    int skill2ManaCost = 70;
    int skill3ManaCost = 90;
    int skill4ManaCost = 100;
    void Start()
    {
        player = GetComponent<Player>();

    }
    // ,
    void Update()
    {
        //skill 1
        if (castTimerSkill1 <= 0)
        {
            castTimerSkill1 = 0;
            player.skill1Slider.gameObject.SetActive(false);
        }
        else if(castTimerSkill1 != 0)
        {
            castTimerSkill1 -= Time.deltaTime;
            player.skill1Slider.value = castTimerSkill1;
            player.skill1CDText.text = string.Format("{0:F1}", castTimerSkill1);
        }
        //skill 2
        if (castTimerSkill2 <= 0)
        {
            castTimerSkill2 = 0;
            player.skill2Slider.gameObject.SetActive(false);
        }
        else if (castTimerSkill2 != 0)
        {
            castTimerSkill2 -= Time.deltaTime;
            player.skill2Slider.value = castTimerSkill2;
            player.skill2CDText.text = string.Format("{0:F1}", castTimerSkill2);
        }
        //skill 3
        if (castTimerSkill3 <= 0)
        {
            castTimerSkill3 = 0;
            player.skill1Slider.gameObject.SetActive(false);
        }
        else if (castTimerSkill3 != 0)
        {
            castTimerSkill3 -= Time.deltaTime;
            player.skill3Slider.value = castTimerSkill3;
            player.skill3CDText.text = string.Format("{0:F1}", castTimerSkill3);
        }
        //skill 4
        if (castTimerSkill4 <= 0)
        {
            castTimerSkill4 = 0;
            player.skill4Slider.gameObject.SetActive(false);
        }
        else if (castTimerSkill4 != 0)
        {
            castTimerSkill4 -= Time.deltaTime;
            player.skill4Slider.value = castTimerSkill4;
            player.skill4CDText.text = string.Format("{0:F1}", castTimerSkill4);
        }
    }

    public void UseSkill1()
    {
        if (castTimerSkill1 <= 0)
        {
            if (player.Mana >= skill1ManaCost)
            {
                Debug.Log("heroName skill 1");
                castTimerSkill1 = skill1CD;
                player.skill1Slider.maxValue = skill1CD;
                player.skill1Slider.gameObject.SetActive(true);
                player.Mana -= skill1ManaCost;
            }
            else Debug.Log("Not enough mana");
        }
        else
        {
            Debug.Log("not ready");
        }

    }
    public void UseSkill2()
    {
        if (castTimerSkill2 <= 0)
        {
            if (player.Mana >= skill2ManaCost)
            {
                Debug.Log("heroName skill 1");
                castTimerSkill2 = skill2CD;
                player.skill2Slider.maxValue = skill2CD;
                player.skill2Slider.gameObject.SetActive(true);
                player.Mana -= skill2ManaCost;
            }
            else Debug.Log("Not enough mana");
        }
        else
        {
            Debug.Log("not ready");
        }

    }
    public void UseSkill3()
    {
        if (castTimerSkill3 <= 0)
        {
            if (player.Mana >= skill3ManaCost)
            {
                castTimerSkill3 = skill3CD;
                player.skill3Slider.maxValue = skill3CD;
                player.skill3Slider.gameObject.SetActive(true);
                player.Mana -= skill3ManaCost;
            }
            else Debug.Log("Not enough mana");
        }
        else
        {
            Debug.Log("not ready");
        }

    }
    public void UseSkill4()
    {
        if (castTimerSkill4 <= 0)
        {
            if (player.Mana >= skill4ManaCost)
            {
                castTimerSkill4 = skill4CD;
                player.skill4Slider.maxValue = skill4CD;
                player.skill4Slider.gameObject.SetActive(true);
                player.Mana -= skill4ManaCost;
            }
            else Debug.Log("Not enough mana");
        }
        else
        {
            Debug.Log("not ready");
        }

    }


}
