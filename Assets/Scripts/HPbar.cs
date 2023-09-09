using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour
{
    [SerializeField]
    private RectTransform hPBar;

    private UIManager uIManager;

    private void Awake()
    {
        uIManager = UIManager.Instance;
    }

    private void OnEnable()
    {
        uIManager.OnGetHit += AdjustingHP;
    }

    private void OnDisable()
    {
        uIManager.OnGetHit -= AdjustingHP;
    }

    private void AdjustingHP(int health)
    {
        if(health == 4)
        {
            hPBar.localPosition = new Vector3(-75.6f, 0, 0);
        }
        else if(health == 3)
        {
            hPBar.localPosition = new Vector3(-151.2f, 0, 0);
        }
        else if(health == 2)
        {
            hPBar.localPosition = new Vector3(-226.8f, 0, 0);
        }
        else if(health == 1)
        {
            hPBar.localPosition = new Vector3(-302.4f, 0, 0);
        }
        else
        {
            hPBar.localPosition = new Vector3(-400f, 0, 0);
        }
    }
}
