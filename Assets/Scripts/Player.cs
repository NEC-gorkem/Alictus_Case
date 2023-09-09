using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private UIManager uIManager;

    private FloatingJoystick floatingJoystick;
    [SerializeField]
    private Rigidbody playerRigidBody;
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private PlayerRange playerRangeScript;
    [SerializeField]
    private GameObject boomerang;
    [SerializeField]
    private Transform firingPoint;


    public float playerBaseSpeed;

    public bool isCharacterMoving;
    private bool isDeath;


    private Coroutine playerCoroutine;

    private void Awake()
    {
        uIManager = UIManager.Instance;

        isDeath = false;
        isCharacterMoving = false;
        floatingJoystick = FindObjectOfType<FloatingJoystick>();
        playerCoroutine = StartCoroutine(PlayerFires());
    }

    private void OnEnable()
    {
        uIManager.OnGetHit += PlayerDeath;
    }

    private void OnDisable()
    {
        uIManager.OnGetHit -= PlayerDeath;
    }


    private void FixedUpdate()
    {
        if (!isDeath)
        {
            Vector3 direction = (-1) * new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);

            if (Vector3.Magnitude(direction) < 0.001)
            {
                playerAnimator.SetBool("isCharacterMoving", false);
                playerRigidBody.velocity = Vector3.zero;
            }
            else
            {
                playerAnimator.SetBool("isCharacterMoving", true);
                playerRigidBody.velocity = direction * playerBaseSpeed * Time.deltaTime;
                playerRigidBody.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            playerRigidBody.velocity = Vector3.zero;
        }
        
    }

    private IEnumerator PlayerFires()
    {
        while(true)
        {
            Collider closestCollider = playerRangeScript.GetClosestCollider();
            if(closestCollider != null)
            {
                Boomerang firedboomerang = Instantiate(boomerang, firingPoint.position, Quaternion.Euler(-89.98f, 0, 0)).GetComponent<Boomerang>();
                firedboomerang.getTargetTransform(closestCollider.transform);
                yield return new WaitForSeconds(3);
            }

            yield return new WaitForSeconds(0.001f);
        }
    }

    private void PlayerDeath(int remHP)
    {
        if(remHP <= 0)
        {
            isDeath = true;
            playerAnimator.SetBool("isDeath", true);
            StopCoroutine(playerCoroutine);
            uIManager.RestartMenuShower();
        }
        
    }


}
