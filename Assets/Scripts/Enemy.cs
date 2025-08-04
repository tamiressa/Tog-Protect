using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;
    public GameObject bullet, explosion, sound;
    public Color bulletcolor;

    public Sprite[] mySprites;
    private SpriteRenderer spriteRenderer;

    public float xSpeed;
    public float ySpeed;
    public int score;

    public bool canShoot;
    public float fireRate;
    public float health;
    public bool missil;
    public bool astronauta;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
       
        if (!canShoot) return;
        fireRate = fireRate + (Random.Range(fireRate / -2, fireRate / 2));
        InvokeRepeating("Shoot", fireRate, fireRate);
        
    }

    [System.Obsolete]
    void Update()
    {
        if (missil) {
            MovimentoMissil();
        
        } else { rb.linearVelocity = new Vector2(xSpeed, ySpeed * -1); }
    }

    void MovimentoMissil()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle + 155); 
        rb.linearVelocity = direction * ySpeed;

        Debug.DrawLine(transform.position, player.transform.position, bulletcolor);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Spaceship>().Damage();
            Die();
        }
    }


    public void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")+score);
        Destroy(gameObject);

    }

    public void Damage()
    {
        health--;
        if (health == 0)
            Die();
    }


    IEnumerator Blink()
    {
        spriteRenderer.sprite = mySprites[1];
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = mySprites[0];
    }

    void Shoot()
    {
        sound.GetComponent<RandomPitchSoundPlayer>().PlayLaserSound();

        if (astronauta)
        {
            StartCoroutine(Blink());
        }

        GameObject temp = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().ChangeDirection();
        temp.GetComponent<Bullet>().ChangeColor(bulletcolor);

    }



}
