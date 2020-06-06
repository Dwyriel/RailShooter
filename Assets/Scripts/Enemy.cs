using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int scorePerKill = 20;
    [SerializeField] GameObject ExplosionFX;
    [SerializeField] Transform parent;
    bool isDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /* In case of need / Shouldn't use for better hitboxes later when implemented
    private void AddNonTriggerBoxCollider()
    {
        Collider objBoxCollider = gameObject.AddComponent<BoxCollider>();
        objBoxCollider.isTrigger = false;
    }
    */
    
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            GameObject deathFX = Instantiate(ExplosionFX, transform.position, Quaternion.identity);
            deathFX.transform.parent = parent;
            Destroy(gameObject);
            ScoreBoard sb = FindObjectOfType<ScoreBoard>();
            sb.ScoreHit(scorePerKill);
        }
    }
}
