using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Weapon")
        {
            var tag = collision.gameObject.tag;
            if (tag == "Enemy")
            {
                //EnemyManager.Instance.DamageEnemy(collision.gameObject, Player.Instance.DamageCalc());
                GetComponent<Collider2D>().isTrigger = true;
                yield return new WaitForSecondsRealtime(1);

            }
            else
            {
                yield return new WaitForSecondsRealtime(1);
                Destroy(gameObject);
            }
        }

        if (gameObject.tag == "Player")
        {
            var tag = collision.gameObject.tag;
            if (tag == "Item" || tag == "Weapon" || tag == "Armor")
            {
                //DropManager.Instance.AddToInventory(collision.gameObject);
            }
        }

        if (gameObject.tag == "Enemy")
        {
            var tag = collision.gameObject.tag;
            if (tag == "Player")
            {
                //GameObject.Find("Player").GetComponent<Player>().DamagePlayer(1);
            }
        }

        if (gameObject.tag == "EnemyArrow")
        {
            var tag = collision.gameObject.tag;
            if (tag == "Player")
            {
                //GameObject.Find("Player").GetComponent<Player>().DamagePlayer(2);
                Destroy(gameObject);
            }
            else
            {
                yield return new WaitForSecondsRealtime(0.3f);
                Destroy(gameObject);
            }
        }
    }
}