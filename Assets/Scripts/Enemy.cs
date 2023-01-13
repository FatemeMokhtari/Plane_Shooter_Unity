using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
      public GameObject EnemyBullet;
      public GameObject EnemyExplosionPrefab;
      public Transform GunPointer1;
      public Transform GunPointer2;
      public float EnemyBulletTime = 0.5f;
      public float speed = 1f;

      public Healthbar healthbar;
      public float health = 10f;
      float barSize = 1f;
      float damage = 0;
      void Start()
      {
            StartCoroutine(Enemyshooting());
            damage = barSize / health;
      }
      void Update()
      {
            transform.Translate(UnityEngine.Vector2.down * speed * Time.deltaTime);
      }
      void DamageHealthbar()
      {
            if (health > 0)
            {
                  health -= 1;
                  barSize = barSize - damage;
                  healthbar.SetSize(barSize);
            }
      }
      private void OnTriggerEnter2D(Collider2D other)
      {
            if (other.tag == "PlayerBullet")
            {
                  DamageHealthbar();
                  Destroy(other.gameObject);
                  if (health <= 0)
                  {
                        Destroy(gameObject);
                        GameObject EnemyExplosion = Instantiate(EnemyExplosionPrefab, transform.position, UnityEngine.Quaternion.identity);
                        Destroy(EnemyExplosion, 0.4f);
                  }

            }
      }
      void EnemyFire()
      {
            Instantiate(EnemyBullet, GunPointer1.position, UnityEngine.Quaternion.identity);
            Instantiate(EnemyBullet, GunPointer2.position, UnityEngine.Quaternion.identity);
      }

      IEnumerator Enemyshooting()
      {
            while (true)
            {
                  yield return new WaitForSeconds(EnemyBulletTime);
                  EnemyFire();
            }

      }
}
