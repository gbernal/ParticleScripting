using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {

    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;
    public Gradient particlesColorGradient;
    public ParticleDecalPool splatDecalPool;

    List<ParticleCollisionEvent> collisionEvents;

	// Use this for initialization
	void Start () {

        collisionEvents = new List<ParticleCollisionEvent>();
		
	}

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++) {

            splatDecalPool.ParticleHit(collisionEvents[i], particlesColorGradient);
            EmitAtLocation(collisionEvents[i]);

        }

        
        
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {

        splatterParticles.transform.position = particleCollisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);

        ParticleSystem.MainModule psMain = splatterParticles.main;
        psMain.startColor = particlesColorGradient.Evaluate(Random.Range(0f, 1f));
        splatterParticles.Emit(1);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButton("Fire1"))
        {

            ParticleSystem.MainModule psMain = particleLauncher.main;
            psMain.startColor = particlesColorGradient.Evaluate(Random.Range(0f, 1f));
            particleLauncher.Emit(1);
        }
		
	}
}
