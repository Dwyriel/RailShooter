using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hitsToDie = 2;
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

    private void OnParticleCollision(GameObject other)
    {
        hitsToDie--;
        if (!isDestroyed)
        {
            if (hitsToDie <= 0)
            {
                TriggerDeath();
            }
        }
    }

    private void TriggerDeath()
    {
        isDestroyed = true;
        GameObject deathFX = Instantiate(ExplosionFX, transform.position, Quaternion.identity);
        deathFX.transform.parent = parent;
        Destroy(gameObject);
        ScoreBoard sb = FindObjectOfType<ScoreBoard>();
        Announcer mp = FindObjectOfType<Announcer>();
        mp.AnnouncePwnage();
        sb.ScoreHit(scorePerKill);
    }
}
