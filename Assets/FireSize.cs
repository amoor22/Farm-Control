using UnityEngine;
using System.Collections.Generic;
public class FireSize : MonoBehaviour
{
    float ParticleStartSize;
    ParticleSystemRenderer renderer; ParticleSystem system;
    List<ParticleCollisionEvent> Events = new List<ParticleCollisionEvent>();
    public List<GameObject> Friendly = new List<GameObject>();
    #region Unity methods

    void Start()
    {
        renderer = GetComponent<ParticleSystemRenderer>();
        system = GetComponent<ParticleSystem>();
        ParticleStartSize = renderer.minParticleSize;
    }

    
    void Update()
    {
        ParticleStartSize += Time.deltaTime * 0.05f;
        renderer.minParticleSize = ParticleStartSize;
        foreach(GameObject i in Friendly){
            system.GetCollisionEvents(i, Events);
        }
        if(Events.Count > 0){
            Debug.Log("hit");
        }

    }
  
    #endregion
}
