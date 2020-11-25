using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFabric : MonoBehaviour
{
    [SerializeField]
    private Predator predatorPrefab;

    [SerializeField]
    private Prey preyPrefab;

    [SerializeField]
    [Range(1, 100)]
    private int predatorCount = 2;

    [SerializeField]
    [Range(1, 100)]
    private int preyCount = 5;



    // Start is called before the first frame update
    void Start()
    {
        if (predatorPrefab != null)
        {
            for (var i = 0; i < predatorCount; i++)
            {
                var predator = Instantiate(
                        predatorPrefab,
                        Random.insideUnitCircle * predatorCount * 2f,
                        Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                        transform
                    );
                predator.name = "Predator " + i;
            }
        }

        if (preyPrefab != null)
        {
            for (var i = 0; i < preyCount; i++)
            {
                var predator = Instantiate(
                        preyPrefab,
                        Random.insideUnitCircle * preyCount * 2f,
                        Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                        transform
                    );
                predator.name = "Prey " + i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
