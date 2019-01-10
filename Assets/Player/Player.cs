using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField] float maxHp = 100f;
    [SerializeField] float currentHp = 100f;
    [SerializeField] float maxMp = 100f;
    [SerializeField] float currentMp = 100f;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        currentMp = maxMp;
    }

    public float healthAsPercentage { get { return currentHp / maxHp; } }

    public float manaAsPercentage
    {
        get { return currentMp / maxMp; }
    }

    public void TakeDamage(float damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage, 0f, maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
