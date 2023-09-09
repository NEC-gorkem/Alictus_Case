using System.Collections;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private UIManager uIManager;


    private Transform targetTransform;
    private Coroutine boomerangCoroutine;
    private Rigidbody rigidbodyboomerang;

    public void getTargetTransform(Transform tTransform)
    {
        rigidbodyboomerang = GetComponent<Rigidbody>();
        targetTransform = tTransform;
        boomerangCoroutine = StartCoroutine(BulletLifeTime());
        uIManager = UIManager.Instance;
    }

    private IEnumerator BulletLifeTime()
    {
        Vector3 direction = Vector3.Normalize(targetTransform.position - transform.position);

        rigidbodyboomerang.velocity = direction * 8;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            StopCoroutine(boomerangCoroutine);
            other.GetComponent<Rigidbody>().detectCollisions = false;
            uIManager.IncrementEnemyKilled();
            Enemy collidedEnemyScript = other.GetComponent<Enemy>();
            collidedEnemyScript.isDeath = true;
            other.GetComponent<Animator>().SetBool("isDeath", true);
            Destroy(gameObject);
        }

    }
}
