using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MovePlayer>().Die();
        }
    }
}
