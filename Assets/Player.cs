using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Threading.Tasks;


public class Player : MonoBehaviour, IDamageable
{
    IHero hero;
    // [System.NonSerialized]
    public float MovementSpeed { get; set; }
    public float AttackRange { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }
    public int Damage { get; set; }
    public Vector3 position { get; set; }
    public float AttackSpeed { get; set; }

    //booleans
    public bool canAttack = false;
    private bool attackSpeedTimer = true;
    public bool autoAttack = true;
    private bool cancelAttack = true;

    //start stats
    public int level = 1;
    public int xp = 0;
    public int gold = 400;
    TextMeshProUGUI goldText;
    // Health
    public int maxHealth;

    // List<Slider> healthSlider;
    Slider healthSlider;
    Slider healthSliderOnChar;
    Text healthText;
    // Mana
    public int maxMana;

    Slider manaSlider;
    Text manaText;
    //damage

    IDamageable moveTwords;
    //anim
    Animator anim;
    [SerializeField]
    public Collider collider;
    int animCount = 0;
    //xp
    Slider xpUi;
    TextMeshProUGUI levelUi;
    //position

    //
    Rigidbody rb;
    float x;
    float z;
    Joystick joystick;
    CinemachineVirtualCamera camera;
    Button attackCreepButton;
    Image image1;
    // Skills
    Button skill1;
    Button skill2;
    Button skill3;
    Button skill4;

    // Speel
    Joystick spelllHandler;
    float spellHandlerX;
    float spellHandlerZ;
    GameObject speelCastDirection;
    GameObject activeSpeelCastDirection;

    //public float castTimerSkill1;

    public Slider skill1Slider;
    public TextMeshProUGUI skill1CDText;
    public Slider skill2Slider;
    public TextMeshProUGUI skill2CDText;
    public Slider skill3Slider;
    public TextMeshProUGUI skill3CDText;
    public Slider skill4Slider;
    public TextMeshProUGUI skill4CDText;




    // Start is called before the first frame update
    void Start()
    {
        //get Hero's stats
        hero = GetComponent<IHero>();
        Health = hero.Health;
        maxHealth = Health;
        Mana = hero.Mana;
        maxMana = Mana;
        MovementSpeed = hero.MovementSpeed;
        AttackRange = hero.AttackRange;
        Damage = hero.Damage;
        AttackSpeed = hero.AttackSpeed;
        //speels
        spelllHandler = GameObject.Find("Skill2Button").GetComponent<Joystick>();
        speelCastDirection = Resources.Load<GameObject>("prefabs/SpellCastDirection"); //GameObject.Find("SpellCastDirection");


        //skills
        //skills
        skill1 = GameObject.Find("Skill1Button").GetComponent<Button>();
        skill2 = GameObject.Find("Skill2Button").GetComponent<Button>();
        skill3 = GameObject.Find("Skill3Button").GetComponent<Button>();
        skill4 = GameObject.Find("Skill4Button").GetComponent<Button>();
        //skill 1
        skill1Slider = skill1.GetComponentInChildren<Slider>();
        skill1Slider.gameObject.SetActive(false);
        skill1CDText = skill1Slider.GetComponentInChildren<TextMeshProUGUI>();
        //skill 2
        skill2Slider = skill2.GetComponentInChildren<Slider>();
        skill2Slider.gameObject.SetActive(false);
        skill2CDText = skill2Slider.GetComponentInChildren<TextMeshProUGUI>();
        //skill 3
        skill3Slider = skill3.GetComponentInChildren<Slider>();
        skill3Slider.gameObject.SetActive(false);
        skill3CDText = skill3Slider.GetComponentInChildren<TextMeshProUGUI>();
        //skill 4
        skill4Slider = skill4.GetComponentInChildren<Slider>();
        skill4Slider.gameObject.SetActive(false);
        skill4CDText = skill4Slider.GetComponentInChildren<TextMeshProUGUI>();

        // Button use events
        skill1.onClick.AddListener(hero.UseSkill1);
        skill2.onClick.AddListener(hero.UseSkill2);
        skill3.onClick.AddListener(hero.UseSkill3);
        skill4.onClick.AddListener(hero.UseSkill4);

        //stats &  camera
        attackCreepButton = GameObject.Find("AttackCreepButton").GetComponent<Button>();
        camera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        goldText = GameObject.Find("GoldImg").GetComponentInChildren<TextMeshProUGUI>();
        joystick = GameObject.Find("FixedJoystick").GetComponent<Joystick>();
        xpUi = GameObject.Find("Xp").GetComponent<Slider>();
        levelUi = xpUi.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        healthSliderOnChar = GetComponentInChildren<Slider>();
        manaSlider = GameObject.Find("ManaSlider").GetComponent<Slider>();
        manaText = GameObject.Find("ManaSlider").GetComponentInChildren<Text>();
        healthText = GameObject.Find("HealthSlider").GetComponentInChildren<Text>();


        //Chinemachine
        camera.Follow = this.transform;



        //  healthSlider.Add(gameObject.GetComponentInChildren<Slider>());
        attackCreepButton.onClick.AddListener(this.Attack);
        rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        xpUi.maxValue = level * 200;
        xpUi.value = xp;
        levelUi.text = level.ToString();
    }

    void AttackListener()
    {
        // to do - delete this method
        Attack();
    }

    void Update()
    {
        ManageSkills();
        UpdateStats();
        if (cancelAttack == false) Attack();
    }

    void FixedUpdate()
    {
        x = joystick.Horizontal;
        z = joystick.Vertical;
        if (x != 0 || z != 0)
        {
            cancelAttack = true;
            moveTwords = null;
            Vector3 move = new Vector3(x, 0, z);


            rb.MovePosition(transform.position + move * Time.deltaTime * MovementSpeed);

            if (move != Vector3.zero)
            {
                transform.forward = move;
                anim.SetBool("isRunning", true);

            }
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (moveTwords != null)
        {
            Vector3 direction = moveTwords.position - transform.position;
            direction.Normalize();

            rb.MovePosition(transform.position + direction * MovementSpeed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                transform.forward = direction;
                anim.SetBool("isRunning", true);

            }
            else
            {
                anim.SetBool("isRunning", false);
            }

            Attack();
        }
        position = transform.position;
    }
    public void Attack()
    {

        if (attackSpeedTimer == true)
        {
            if (collider == null) return;
            IDamageable target = collider.GetComponent<IDamageable>();

            if (target != null)
            {
                float dist = Vector3.Distance(target.position, transform.position);
                if (dist < AttackRange)
                {
                    if (moveTwords != null) moveTwords = null;
                    if (animCount < 2)
                    {
                        anim.Play("Attack01");
                        animCount++;
                    }
                    else
                    {
                        anim.Play("Attack02");
                        animCount = 0;
                    }
                    target.TakeDamage(this, Damage);
                    cancelAttack = false;
                    // to do - create an object that deals damage
                    attackSpeedTimer = false;
                    Task.Delay(System.Convert.ToInt32(AttackSpeed)).ContinueWith(t => attackSpeedTimer = true);

                }
                else
                {
                    moveTwords = target;
                }
            }
            else Debug.LogError("no target");
        }

    }
    public void TakeDamage(IDamageable caster, int damage)
    {

    }
    public void TakeXp(int _xp, int _gold)
    {
        gold += _gold;
        xp += _xp;
        if (xp >= level * 200)
        {
            xp -= level * 200;
            level++;
        }

        xpUi.maxValue = level * 200;
        xpUi.value = xp;
        levelUi.text = level.ToString();

    }

    void MovePlayer()
    {

    }
    public void UpdateMana(int plusMana)
    {
        int procentMana = (Mana * 100) / maxMana;
        maxMana += plusMana;
        Mana = (maxMana * procentMana) / 100;
    }
    public void UpdateHealth(int plusHealth)
    {
        int procentHealth = (Health * 100) / maxHealth;
        maxHealth += plusHealth;
        Health = (maxHealth * procentHealth) / 100;
    }
    void UpdateStats()
    {
        //health 
        healthText.text = $"{Health} / {maxHealth}";
        healthSlider.maxValue = maxHealth;
        healthSliderOnChar.maxValue = maxHealth;
        healthSlider.value = Health;
        healthSliderOnChar.value = Health;

        //mana
        manaText.text = $"{Mana} / {maxMana}";
        manaSlider.maxValue = maxMana;
        manaSlider.value = Mana;
        //gold
        goldText.text = $"{gold} gold";

    }
    void ManageSkills()
    {
        spellHandlerX = spelllHandler.Horizontal;
        spellHandlerZ = spelllHandler.Vertical;
        if (spellHandlerX != 0 || spellHandlerZ != 0)
        {
            if (activeSpeelCastDirection != null)
            {
                activeSpeelCastDirection.transform.forward = new Vector3(spellHandlerX * -1, 180, spellHandlerZ * -1);
            }
            else
            {
                activeSpeelCastDirection = Instantiate(speelCastDirection, this.transform.position, Quaternion.Euler(180, 0, 0));
            }
        }
        else
        {
            if (activeSpeelCastDirection != null)
            {
                hero.UseSkill2(); // to do - specific skill
                Destroy(activeSpeelCastDirection);
            }
        }
    }
}
