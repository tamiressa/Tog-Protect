using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    int delay = 0;
    GameObject a,b;
    Rigidbody2D rb;
    public GameObject bullet, explosion, sound, soundDamage, soundExplosion, texto;
    public float speed;
    int health=3;
    public bool canBlock;
    public bool isBlocking;
    public int energy;

    public Sprite[] mySprites;
    public SpriteRenderer spriteRenderer;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        a = transform.Find("a").gameObject;
        b = transform.Find("b").gameObject;

        canBlock = true;
        isBlocking = false;

    }

  

   
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal")*speed, 0));
        rb.AddForce(new Vector2(0, Input.GetAxis("Vertical") * speed));

        Block();

        Special();

        if (Input.GetKey(KeyCode.Space)&&delay>10)
        {
            Shoot();
        }

        delay++;

        texto.GetComponent<TextMeshProUGUI>().text = energy + "";

    }

    public void Damage()
    {

        if (isBlocking)
        {
            
            energy++;

        }
        else
        {
            health--;
            soundDamage.GetComponent<RandomPitchSoundPlayer>().PlayLaserSound();
            StartCoroutine(Blink());
            if (health == 0)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.1f);
                SceneManager.LoadScene(3);
            }
        }

    }

    IEnumerator Blink()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

   

    void Shoot()
    {
        sound.GetComponent<RandomPitchSoundPlayer>().PlayLaserSound();
        delay = 0;
        Instantiate(bullet, a.transform.position, Quaternion.identity);
        Instantiate(bullet, b.transform.position, Quaternion.identity);

        StartCoroutine(ShootAnim());

    }


    IEnumerator Cooldown()
    {
        GetComponent<Animator>().enabled = false;

        spriteRenderer.sprite = mySprites[3];
        GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 1);
        yield return new WaitForSeconds(1.5f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        spriteRenderer.sprite = mySprites[0];

        GetComponent<Animator>().enabled = true;


        isBlocking = false;
        yield return new WaitForSeconds(7f);

        canBlock = true;
        
    }
    void Block()
    {
        if (Input.GetKey(KeyCode.B) && canBlock)
        {
            isBlocking = true;
            canBlock = false;
            StartCoroutine(Cooldown());

        }

    }

    void Special()
    {
        if (Input.GetKeyDown(KeyCode.V) && energy >= 2)
        {
            

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().Die();
            }

            energy = 0;

            soundExplosion.GetComponent<RandomPitchSoundPlayer>().PlayLaserSound();

            StartCoroutine(SpecialAnim());
        }



    }


    IEnumerator ShootAnim()
    {
        GetComponent<Animator>().enabled = false;

        spriteRenderer.sprite = mySprites[1];
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = mySprites[0];

        GetComponent<Animator>().enabled = true;

    }

    IEnumerator SpecialAnim()
    {
        GetComponent<Animator>().enabled = false;

        spriteRenderer.sprite = mySprites[2];
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = mySprites[0];

        GetComponent<Animator>().enabled = true;


    }

}
