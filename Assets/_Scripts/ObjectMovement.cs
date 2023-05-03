using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] Vector2 speedMinMax;
    private float speed;
    [SerializeField] bool isHealth;

    [SerializeField] int hitValue;
    [SerializeField] bool doesDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        float rndValue = Random.Range(speedMinMax.x, speedMinMax.y);
        speed = rndValue;
        if (!isHealth)
        {
            transform.localScale = (Vector3.one / 2) * (100 / rndValue);
            rb.AddTorque(rndValue);
        }
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    /// <summary>
    /// Moves the object down
    /// </summary>
    private void Movement()
    {
        rb.velocity = Vector2.down * speed * Time.deltaTime;
    }

    /// <summary>
    /// Destroys the object and spawns an effect
    /// </summary>
    private void DestroyObject()
    {
        //Destory object
        Destroy(gameObject);

        //Spawn particals n shit
    }

    /// <summary>
    /// Does damage or heal the player, depending on what the doesDamage bool is set to
    /// </summary>
    /// <param name="player">The player GameObject</param>
    private void PlayerHit(GameObject player)
    {
        Health health = player.GetComponent<Health>();

        if (doesDamage)
        {
            health.TakeDamage(hitValue);
        }
        else
        {
            health.RestoreHealth(hitValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHit(other.gameObject);
            DestroyObject();
        }
        else if(!other.CompareTag("Object"))
        {
            DestroyObject();
        }
    }
}
