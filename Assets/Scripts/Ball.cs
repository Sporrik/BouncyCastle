using TMPro;
using UnityEngine;
public class Ball : MonoBehaviour
{
    private int _hp = 5;
    [SerializeField] private TextMeshProUGUI _hpText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Harm"))
        {
            _hp--;
            _hpText.text = _hp.ToString();

            if (_hp < 1)
            {
                GameManager.instance.SpawnBall();
                Destroy(gameObject);
            }
        }
    }
}
