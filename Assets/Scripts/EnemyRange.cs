using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    private Animator enemyAnimator;
    private Enemy scriptEnemy;

    private void Start()
    {
        enemyAnimator = transform.parent.gameObject.GetComponent<Animator>();
        scriptEnemy = transform.parent.gameObject.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            scriptEnemy.isPlayerInRange = true;
            enemyAnimator.SetBool("isPlayerInRange", true);
        }
    }




    private void OnTriggerExit(Collider other)
    {
        
        if(other.tag == "Player")
        {
            scriptEnemy.isPlayerInRange = false;
            enemyAnimator.SetBool("isPlayerInRange", false);
        }
        
    }

}
