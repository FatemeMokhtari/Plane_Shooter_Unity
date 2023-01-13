using System.Diagnostics;
using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
      public float speed = 10f;
      public float minX;
      public float minY;
      // 1.37 & -1.37
      public float maxX;
      public float maxY;

      public GameObject Explotion;


      public PlayerHealthbar playerHealthbar;
      public float health = 20f;
      float BarFillAmount = 1f;
      float damage = 0;
      void Start()
      {
            damage = BarFillAmount / health;
      }


      void Update()
      {

            float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

            float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
            float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
            transform.position = new UnityEngine.Vector2(newXpos, newYpos);
      }

      private void OnTriggerEnter2D(Collider2D other)
      {
            if (other.tag == "EnemyBullet")
            {
                    DamagePlayerHealthbar();
                   Destroy(other.gameObject);
                   if (health <= 0)
                   {
                  Destroy(gameObject);
                  GameObject blast = Instantiate(Explotion, transform.position, UnityEngine.Quaternion.identity);
                  Destroy(blast, 2f);
                  } 

            }
      }

      void DamagePlayerHealthbar()
      {
            if (health > 0)
            {
                  health -= 1;
                  BarFillAmount = BarFillAmount - damage;
                  playerHealthbar.SetAmount(BarFillAmount);
            }
      }
}
