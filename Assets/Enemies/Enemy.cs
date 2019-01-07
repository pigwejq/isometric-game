using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{

    [SerializeField] float maxHp = 100f;
    [SerializeField] float currentHp = 100f;
    [SerializeField] float minDistanceToPlayer = 6f;

    AICharacterControl ai;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;

        ai = GetComponent<AICharacterControl>();
        player = FindObjectOfType<Player>().transform;
    }

    public float healthAsPercentage
    {
        get { return currentHp / maxHp; }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= minDistanceToPlayer)
        {
            ai.SetTarget(player);
        }
        else
        {
            ai.SetTarget(transform);
        }
    }
}
