using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private int _teamID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.instance.OnGoalScored(_teamID);

            Destroy(collision.gameObject);
        }
    }
}
