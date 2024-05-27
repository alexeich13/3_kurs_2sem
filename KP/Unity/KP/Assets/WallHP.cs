using UnityEngine;

public class WallHP : MonoBehaviour, IDamagable
{
    public float health = 100f;
    public float healthF = 100f;
    public GameObject damageParticle;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); 
    }
    public void TakeDamage(float damage, Vector3 position, Vector3 direction)
    {
        Debug.Log("Hit!");

        health = damage - healthF;
        GameObject particle = Instantiate(damageParticle, position + direction * 0.02f, Quaternion.Euler(direction));
        Destroy(particle, 0.5f);
        if (gameManager != null)
        {
            gameManager.OnHit(health);
        }
    }
}
