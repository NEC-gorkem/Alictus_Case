using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject skeleton;
    [SerializeField]
    private Transform playerTransform;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        SpawnCoins();
    }

    // We can make this generic so that we can use one coroutine.
    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            yield return new WaitForSeconds(4);
            Vector3 enemyCandidatePosition = new Vector3(0f, 0.8f, 0f);

            while (true)
            {
                yield return new WaitForSeconds(0.0005f);
                enemyCandidatePosition.x = Random.Range(-36f, 36f);
                enemyCandidatePosition.z = Random.Range(-36f, 36f);

                if (Vector3.Distance(enemyCandidatePosition, playerTransform.position) > 15f)
                {
                    GameObject spawned = Instantiate(skeleton, enemyCandidatePosition, Quaternion.identity);
                    break;
                }
            }
        }
    }

    private void SpawnCoins()
    {
        int numberOfCoinsGoingToBeSpawned = 20;
        for(int i = 0; i < numberOfCoinsGoingToBeSpawned; i++)
        {
            Vector3 coinCandidatePosition = new Vector3(0f, 0.25f, 0f);
            while (true)
            {
                coinCandidatePosition.x = Random.Range(-36f, 36f);
                coinCandidatePosition.z = Random.Range(-36f, 36f);

                if (Vector3.Distance(coinCandidatePosition, playerTransform.position) > 4f)
                {
                    GameObject spawned = Instantiate(coinPrefab, coinCandidatePosition, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
