using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class ScoringZone : MonoBehaviour
{
    public UnityEvent scoreTrigger;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ScoreZone");
        // If the ball enters
        if (other.TryGetComponent(out BallController ball))
        {
            // Calling the event
            scoreTrigger.Invoke();

            // Reset the ball again
            ball.ResetPosition();
        }
    }
}