using UnityEngine;

public class Ball : MonoBehaviour
{
    private int _hp = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Harm"))
        {
            _hp--;
            if (_hp < 1)
            {
                GameManager.instance.SpawnBall();
                Destroy(gameObject);
            }
        }
    }
}
