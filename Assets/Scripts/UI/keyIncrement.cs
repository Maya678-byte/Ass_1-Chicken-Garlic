using UnityEngine;

public class keyIncrement : MonoBehaviour
{
    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            KeyScoring.Instance.AddKey();
            //Destroy(gameObject);
        }
    }
}
