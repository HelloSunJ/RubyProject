using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RubyControlloer : MonoBehaviour
{
#if (!UNITY_EDITOR && UNITY_ANDROID)
    public float speed = 3f;
#else
     float speed = 50f;
#endif

    Rigidbody2D rigidbody2d;

    public AudioClip Hitclip;
    public AudioClip Throwclip;
    public AudioClip questclip;



    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health { get { return currentHealth; } }
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;

    PlayerMoveON moves;

    public GameObject projectilePrefab;
    public GameObject HitEffect;
  

    


    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public const int GOL_FIXED_ROBOT = 3;
    int FixedRobot;
    public int FixedRobotCount
    {
        get
        {
            return FixedRobotCount;
        }
        set
        {
            FixedRobotCount = value;
        }
    }

    // Start is called before the first frame update

    AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
        moves = GetComponent<PlayerMoveON>();


        // currentHealth = 1; // 체력이 가득 차면 못먹음 


        //  QualitySettings.vSyncCount = 0;
        //  Application.targetFrameRate = 60;
    }
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
#if (!UNITY_ANDROID)

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
#else
        Vector2 move = moves.MoveInput.normalized;
#endif

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;

        position = position + move * speed * Time.deltaTime; //이동~

        rigidbody2d.MovePosition(position);

        //transform.position = position;       
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Luch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Talk ();
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            animator.SetTrigger("Hit"); //피격시 
            PlaySound(Hitclip);
            isInvincible = true;
            invincibleTimer = timeInvincible;
            Instantiate(HitEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        // Debug.Log(currentHealth + "/" + maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

    }
 

    public void Talk()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

        if (hit.collider != null)
        {
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                if (IsQuestCompelet())
                {
                    character.ChageDisplayDialog();
                }
                character.DisplayDialog();
            }
        }
    }



    public void TellMeFixed()
    {
        FixedRobot++;
    }
        

   
    public bool IsQuestCompelet()
    {
        bool val = false;
        if (FixedRobot == GOL_FIXED_ROBOT)
        {
            PlaySound(questclip);
            val = true;

        }
        return val;

    }
    public void Luch()
    {

            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);

            animator.SetTrigger("Launch");
            PlaySound(Throwclip);

     }
}

