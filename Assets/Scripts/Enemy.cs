using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private UIManager uIManager;

    private Rigidbody enemyRigidBody;
    private Transform playerLocation;
    private float enemyBaseSpeed;
    


    // [HideInInspector]
    public bool isPlayerInRange;
    // [HideInInspector]
    public bool isDeath;
    private bool isAnimationRunning;
    

    [SerializeField]
    private GameObject enemyBullet;
    [SerializeField]
    private Transform firingPoint;

    private void Start()
    {
        isDeath = false;
        isAnimationRunning = false;
        isPlayerInRange = false;

        enemyRigidBody = GetComponent<Rigidbody>();
        playerLocation = GameObject.Find("MainCharacterRigged").transform;
        uIManager = UIManager.Instance;

        enemyBaseSpeed = 200f;
    }

    private void FixedUpdate()
    {
        if (!isDeath)
        {
            Vector3 direction = Vector3.Normalize(playerLocation.position - transform.position);
            if (isAnimationRunning || isPlayerInRange)
            {
                enemyRigidBody.velocity = Vector3.zero;
                enemyRigidBody.rotation = Quaternion.LookRotation(direction);
            }
            else
            {

                enemyRigidBody.velocity = direction * enemyBaseSpeed * Time.deltaTime;
                enemyRigidBody.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            enemyRigidBody.velocity = Vector3.zero;
        }
        
        
    }

    public void Throw()
    {
        EnemyBullet firedBullet = Instantiate(enemyBullet, firingPoint.position, Quaternion.identity).GetComponent<EnemyBullet>();
        firedBullet.getPlayerTransform(playerLocation);
    }


    public void FireAnimationEnded()
    {
        isAnimationRunning = false;
    }

    public void FireAnimationStarted()
    {
        isAnimationRunning = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDeath && collision.transform.tag == "Player")
        {
            uIManager.DecrementplayerHealth(5);
        }
    }

    public void DeathEnd()
    {
        Destroy(gameObject);
    }

}
