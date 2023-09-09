using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI enemyKilledIndicator;
    [SerializeField]
    private TextMeshProUGUI coinCollectedIndicator;
    [SerializeField]
    private GameObject RestartMenu;
    [SerializeField]
    private GameObject floatingJoyStick;

    private int playerHealth;
    private int EnemyKilled;

    public delegate void OnGetHitDelegate(int remaningHP);
    public event OnGetHitDelegate OnGetHit;

    private void Awake()
    {
        if(Instance == null)
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
        playerHealth = 5;
        EnemyKilled = 0;
        enemyKilledIndicator.text = "0";
        coinCollectedIndicator.text = PlayerPrefs.GetInt("GoldCollected", 0).ToString();
    }


    public void IncrementEnemyKilled()
    {
        EnemyKilled++;
        enemyKilledIndicator.text = EnemyKilled.ToString();
    }

    public void IncrementGoldCollected()
    {
        PlayerPrefs.SetInt("GoldCollected", PlayerPrefs.GetInt("GoldCollected" , 0) + 1);
        coinCollectedIndicator.text = PlayerPrefs.GetInt("GoldCollected", 0).ToString();
    }

    public void DecrementplayerHealth(int damage)
    {
        playerHealth -= damage;
        Debug.Log("player is taken damage, remaning health is: " + playerHealth.ToString());
        OnGetHit?.Invoke(playerHealth);
    }

    public void RestartMenuShower()
    {
        RestartMenu.SetActive(true);
        floatingJoyStick.SetActive(false);
    }

    public void RestartTheGameFromButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }







}
