using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bulletPrefab;

    public GameObject speedUp;
    public GameObject bonusSound;
    private SpriteRenderer bulletSprite;
    [SerializeField] Sprite bullet1;
    [SerializeField] Sprite bullet2;
    [SerializeField] Sprite bullet3;
    public Bullet bullet;
    private int speedUpCount = 1;
    private int weaponCount = 1;

    [SerializeField] GameObject explosion;
    private float speed = 5f;
    public float reloadTime = 0.5f;
    private float sizeOfBullet = 1f;

    float elapsedTime = 0f;

    private void Start()
    {

    }

    private void Update()
    {

        //Отсчёт времени после выстрела
        elapsedTime += Time.deltaTime;

        //Создание экземпляра снаряда на расстоянии 0.8 единицы перед игроком
        if (elapsedTime > reloadTime)
        {
            Vector3 spawnPos = transform.position;
            if (weaponCount == 1 )
            {
                bullet.transform.localScale = new Vector3(sizeOfBullet, sizeOfBullet, 0);
                Instantiate(bulletPrefab, spawnPos + new Vector3(0, 0.9f), Quaternion.identity);
            }
            else if (weaponCount == 2)
            {
                bullet.transform.localScale = new Vector3(sizeOfBullet, sizeOfBullet, 0);
                Instantiate(bulletPrefab, spawnPos + new Vector3(-0.4f, 0.7f), Quaternion.identity);
                Instantiate(bulletPrefab, spawnPos + new Vector3(0.4f, 0.7f), Quaternion.identity);
            }
            else if (weaponCount == 3)
            {
                bullet.transform.localScale = new Vector3(sizeOfBullet, sizeOfBullet, 0);
                Instantiate(bulletPrefab, spawnPos + new Vector3(0, 0.9f), Quaternion.identity);
                Instantiate(bulletPrefab, spawnPos + new Vector3(-0.4f, 0.7f), Quaternion.identity);
                Instantiate(bulletPrefab, spawnPos + new Vector3(0.4f, 0.7f), Quaternion.identity);
            }
            else
            {
                bullet.transform.localScale = new Vector3(sizeOfBullet, sizeOfBullet, 0);
                Instantiate(bulletPrefab, spawnPos + new Vector3(0, 0.9f), Quaternion.identity);
                Instantiate(bulletPrefab, spawnPos + new Vector3(-0.4f, 0.7f), Quaternion.identity);
                Instantiate(bulletPrefab, spawnPos + new Vector3(0.4f, 0.7f), Quaternion.identity);
                Instantiate(bulletPrefab, spawnPos + new Vector3(-0.8f, 0.3f), Quaternion.identity);
                Instantiate(bulletPrefab, spawnPos + new Vector3(0.8f, 0.3f), Quaternion.identity);
            }
            elapsedTime = 0f;
        }
        ShipMovement();
    }

    void ShipMovement()
    {
        if (Input.touchCount > 0)
        {
        //Перемещение игрока по оси x
        Touch touch = Input.GetTouch(0); // Get the touch data for the first finger
        Vector2 position = touch.position; // Get the position in screen-space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position); // Convert the position to world space
        Vector3 shipPosition = this.transform.position;
        this.transform.position = Vector3.MoveTowards(new Vector3(shipPosition.x, shipPosition.y, shipPosition.z), new Vector3(worldPosition.x, worldPosition.y, shipPosition.z), Time.deltaTime * speed); // Move the ship
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bulletSprite = bulletPrefab.GetComponent<SpriteRenderer>();
        if (other.gameObject.tag == "Meteor")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            bulletSprite.sprite = bullet1;
            speedUpCount = 1;
            weaponCount = 1;
            gameManager.PlayerDied();
        }
        else if (other.gameObject.tag == "Weapon")
        {
            Instantiate(bonusSound,new Vector3(0, 0, 0), Quaternion.identity);
            weaponCount++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Speed")
        {
            Instantiate(bonusSound, new Vector3(0, 0, 0), Quaternion.identity);
            reloadTime -= 0.15f;
            if (speedUpCount == 1)
                bulletSprite.sprite = bullet2;
            else if (speedUpCount == 2)
                bulletSprite.sprite = bullet3;
            speedUpCount++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Size")
        {
            Instantiate(bonusSound, new Vector3(0, 0, 0), Quaternion.identity);
            sizeOfBullet += 2f;
            Destroy(other.gameObject);
        }
    }
}
