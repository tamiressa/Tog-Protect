using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
    public float rate;
    public GameObject[] enemies;
    public int waves = 1;
    public float tempo;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
        StartCoroutine(Cooldown());
    }



    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(tempo);
        waves++;
        StartCoroutine(Cooldown());
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < waves; i++)
        {

            Instantiate(enemies[(int)Random.Range(0, enemies.Length)], new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);


        }
    }
}
