using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
      
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigd2D;

    float timer;
    int direction = 1;
    bool broken = true;
    
    public ParticleSystem smokeEffect;
    public GameObject FixEffect;

    public RubyControlloer rubyControlloer;
    public AudioClip fixedclip;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigd2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }


        Vector2 position = rigd2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigd2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyControlloer player = other.gameObject.GetComponent<RubyControlloer>();

        if (player != null)
        {
            player.ChangeHealth(-1);

        }

    }

    public void Fix()
    {
        broken = false;
        rigd2D.simulated = false;
        //optional if you added the fixed animation
        animator.SetTrigger("Fixed");
         //smokeEffect.Stop();
        Instantiate(FixEffect,rigd2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Destroy(smokeEffect.gameObject);
        GameObject go = GameObject.FindWithTag("RUBY");
        if(go != null)
        {
            RubyControlloer ruby = go.GetComponent<RubyControlloer>();
            rubyControlloer.PlaySound(fixedclip);
            ruby.TellMeFixed();
        }
    }
}
