using UnityEngine;

public class BalloonPopper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Balloon balloon))
        {
            balloon.PopBalloon();
        }
    }
}
