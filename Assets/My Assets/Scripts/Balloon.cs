using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Balloon : MonoBehaviour
{
    public UnityEvent BalloonPoppedEvent = new();

    [SerializeField] private float acceleration = 0.2f;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(Vector3.up * acceleration, ForceMode.Acceleration);
    }

    public void PopBalloon()
    {
        BalloonPoppedEvent.Invoke();
        Destroy(gameObject);
    }
}
