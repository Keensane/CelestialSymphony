using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject particlePrefab;
    [SerializeField] GameObject smallParticlePrefab;
    bool isDestroyed = false;

    int meteorHP = 0;

    private void Start()
    {
        //Пока игра не запущена и метеоритов на сцене нет, обращаться к объекту GameManager нужно по другому
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (transform.localScale.x > 2f)
        {
            meteorHP = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet" && !isDestroyed && meteorHP != 0)
        {
            meteorHP--;
            Instantiate(smallParticlePrefab, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Bullet" && !isDestroyed && meteorHP == 0)
        {
            isDestroyed = true;
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); //Уничтожение метеора
            Destroy(other.gameObject); //Уничтожение снаряда
            gameManager.AddScore(); //увеличение счёта
        }
        else if (other.gameObject.tag == "Bullet" && isDestroyed)
        {
            Destroy(other.gameObject);
        }
    }
}
