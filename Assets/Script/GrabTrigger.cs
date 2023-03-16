using UnityEngine;

public class GrabTrigger : MonoBehaviour
{
    [SerializeField] private RopeSpawner ropeSpawner;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Citizen"))
        {
            ropeSpawner.TimeShift();
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
