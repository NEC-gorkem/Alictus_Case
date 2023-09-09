using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private UIManager uIManager;


    private Transform playerTransform;
    private Coroutine BulletCoroutine;
    private Rigidbody rigidbodyEnemyBullet;


    public void getPlayerTransform(Transform pTransform)
    {
        rigidbodyEnemyBullet = GetComponent<Rigidbody>();
        playerTransform = pTransform;
        BulletCoroutine = StartCoroutine(BulletLifeTime());
        uIManager = UIManager.Instance;
    }

    private IEnumerator BulletLifeTime()
    {
        Vector3 direction = Vector3.Normalize(playerTransform.position - transform.position);
        rigidbodyEnemyBullet.velocity = direction * 10;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StopCoroutine(BulletCoroutine);
            Destroy(gameObject);
            uIManager.DecrementplayerHealth(1);
        }
        
    }

}
