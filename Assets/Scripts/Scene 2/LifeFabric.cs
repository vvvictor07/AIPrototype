using UnityEngine;

namespace Assets.Scripts.Scene_2
{
    public class LifeFabric : MonoBehaviour
    {
        [SerializeField]
        private GameObject predatorPrefab = null;

        [SerializeField]
        private GameObject preyPrefab = null;

        [SerializeField]
        [Range(1, 100)]
        private int predatorCount = 2;

        [SerializeField]
        [Range(1, 100)]
        private int preyCount = 5;

        // Start is called before the first frame update
        private void Start()
        {
            if (predatorPrefab != null)
            {
                for (var i = 0; i < predatorCount; i++)
                {
                    var predator = Instantiate(
                            predatorPrefab,
                            Random.insideUnitCircle * predatorCount * 0.2f,
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
                            Random.insideUnitCircle * preyCount * 0.2f,
                            Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                            transform
                        );
                    predator.name = "Prey " + i;
                }
            }
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}