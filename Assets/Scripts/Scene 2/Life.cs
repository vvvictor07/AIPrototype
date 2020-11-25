using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameObject must have Collider2D attached
[RequireComponent(typeof(Collider2D))]
public class Life : MonoBehaviour
{
    private Collider2D agentCollider;

    private Animator animator;

    [SerializeField]
    private LifeCharacteristics lifeCharacteristics;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
