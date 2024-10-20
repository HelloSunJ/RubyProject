using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyControlloer ruby = other.GetComponent<RubyControlloer>();

        if (ruby != null)
        {
            if (ruby.health < ruby.maxHealth)
            {
            ruby.ChangeHealth(1);
            Destroy(gameObject);

                ruby.PlaySound(collectedClip);

            }
        }
    }
}
