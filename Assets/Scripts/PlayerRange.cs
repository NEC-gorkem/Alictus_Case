using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRange : MonoBehaviour
{

    
    private Collider closestCollider;
    private List<Collider> enemiesInRange;

    private void Start()
    {
        enemiesInRange = new List<Collider>();
    }

    private void Update()
    {
        if(enemiesInRange.Count > 0)
        {
            if (enemiesInRange.Count > 1)
            {
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    for (int j = i + 1; j < enemiesInRange.Count; j++)
                    {
                        float firstDistance = Vector3.Distance(enemiesInRange[i].transform.position, transform.position);
                        float secondDistance = Vector3.Distance(enemiesInRange[j].transform.position, transform.position);
                        if (firstDistance > secondDistance)
                        {
                            Collider temp = enemiesInRange[i];
                            enemiesInRange[i] = enemiesInRange[j];
                            enemiesInRange[j] = temp;
                        }
                    }
                }
            }
            closestCollider = enemiesInRange[0];
        }
        else
        {
            closestCollider = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemiesInRange.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemiesInRange.Remove(other);
        }
    }

    public Collider GetClosestCollider()
    {
        return closestCollider;
    }


}
