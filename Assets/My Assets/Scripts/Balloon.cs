using UnityEngine;
using UnityEngine.Events;

public class Balloon : MonoBehaviour
{
    public UnityEvent BalloonPoppedEvent = new();

    public void PopBalloon()
    {
        BalloonPoppedEvent.Invoke();
        Destroy(gameObject);
    }
}
