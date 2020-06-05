﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject ExplosionFX;
    [SerializeField] Transform parent;
    bool isDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /* In case of need / Shouldn't use for better hitboxes
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
        }
    }
}
