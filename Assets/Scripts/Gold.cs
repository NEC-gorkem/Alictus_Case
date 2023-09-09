using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{

    private UIManager uIManager;

    private void Start()
    {
        uIManager = UIManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            uIManager.IncrementGoldCollected();
        }
    }
}
