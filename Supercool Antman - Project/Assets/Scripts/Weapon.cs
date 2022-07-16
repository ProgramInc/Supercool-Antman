using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage;

    [SerializeField] GameObject impactPrefab;
    [SerializeField] float weaponHitRadius;
    [SerializeField] Transform weaponTip;

    public delegate void EnemyWasHitAction();
    public static EnemyWasHitAction OnEnemyWasHit;

    public delegate void SwordSwooshAction();
    public static SwordSwooshAction OnSwordSwoosh;
    public delegate void LightSaberSwooshAction();
    public static LightSaberSwooshAction OnLightSaberSwoosh;

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        print(gameObject.name + " hit" + collision.name);
        if (CompareTag("Lazer"))
        {
            Instantiate(impactPrefab, collision.transform.position, Quaternion.Euler(0, 0, collision.transform.rotation.z));
            if (collision.GetComponentInParent<Enemy>())
            {
                collision.GetComponentInParent<Enemy>().Health -= Damage;
                print(collision.GetComponentInParent<Enemy>().Health);
            }
            Destroy(gameObject);
        }
        else
        {
            if (collision.GetComponentInParent<Enemy>())
            {
                collision.GetComponentInParent<Enemy>().Health -= Damage;
                print(collision.GetComponentInParent<Enemy>().Health);
                *//*Instantiate(impactPrefab, collision.transform.position, Quaternion.Euler(0, 0, collision.transform.rotation.z));*//*
            }
        }
    }*/

    public void DoDamage()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(weaponTip.position, weaponHitRadius);
        
        foreach (Collider2D enemyCollider in enemiesInRange)
        {
            Beetle beetle = enemyCollider.GetComponent<Beetle>();
            if (beetle != null)
            {
                beetle.Health -= Damage;
                GetComponentInParent<WeaponEffect>().TriggerZoom();
                Instantiate(impactPrefab, beetle.transform.position, Quaternion.Euler(0, 0, Random.Range(-45, 45))); ;
                OnEnemyWasHit?.Invoke();
            }
            else
            {
                Mantis Mantis = enemyCollider.GetComponent<Mantis>();
                if (Mantis != null)
                {
                    Mantis.Health -= Damage;
                    GetComponentInParent<WeaponEffect>().TriggerZoom();
                    Instantiate(impactPrefab, Mantis.transform.position, Quaternion.Euler(0, 0, Random.Range(-45, 45))); ;
                    OnEnemyWasHit?.Invoke();
                }
            }
        }
    }

    void SwordSwoosh()
    {
        OnSwordSwoosh?.Invoke();
    }
    void LightSaberSwoosh()
    {
        OnLightSaberSwoosh?.Invoke();
    }

    /* private void OnDrawGizmos()
     {
         Gizmos.DrawSphere(weaponTip.position, weaponHitRadius);
     }*/
}
