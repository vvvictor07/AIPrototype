using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

// GameObject must have Collider2D attached
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(LifeCharacteristics))]
public class Life : MonoBehaviour
{
    private new Collider2D collider;

    private Animator animator;

    [SerializeField]
    public LifeCharacteristics LifeCharacteristics;

    public List<Transform> GetNearbyObjectsByRadius(float radius)
    {
        return Physics2D
            .OverlapCircleAll(transform.position, radius)
            .Where(x => x != collider)
            .Select(x => x.transform)
            .ToList();
    }

    // Start is called before the first frame update
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}