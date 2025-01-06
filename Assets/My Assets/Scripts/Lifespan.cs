using System.Collections;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    [SerializeField] private int lifespanSeconds = 5;

    private void Start()
    {
        StartCoroutine("LifespanRoutine");
    }

    private IEnumerator LifespanRoutine()
    {
        yield return new WaitForSeconds(lifespanSeconds);
        Destroy(gameObject);
    }
}
