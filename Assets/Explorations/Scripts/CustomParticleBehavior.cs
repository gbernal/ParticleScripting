using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Particle {
	public GameObject obj;
	public Vector3 normal;

	public Particle(GameObject obj, Vector3 normal) {
		this.obj = obj;
		this.normal = normal;
	}

}

public class CustomParticleBehavior : MonoBehaviour {

    public System.Action<CustomParticleBehavior> OnResultsReady;

    public float particleScale;
	public bool spawnParticles;
	public float spawnStart;
	public bool particlesArePhysical;
	public float particleLifetime;
	public float normalOffset;
	public float normalSpeed;
	public int maxParticles;
	public float rigidBodyStart;
	public bool isChild;
	public bool disableMesh;
	public GameObject particleMesh;
	public Vector3 initialVelocity;

	SkinnedMeshRenderer skin;
	Mesh baked;
	List<Particle> particles;
	float timeRemaining;
	float spawnTimeRemaining;
	// Use this for initialization
	void Start () {
		skin = this.GetComponentInParent<SkinnedMeshRenderer>();
		particles = new List<Particle>();
		if (rigidBodyStart > 0)
			timeRemaining = rigidBodyStart;
		spawnTimeRemaining = spawnStart;
	}

	private void spawnParticlesAtVertices() {
		baked = new Mesh();
		skin.BakeMesh(baked);
		for (int i = 0; i < baked.vertexCount; i++) {
			GameObject obj;
			if (particleMesh == null) //defaults to sphere
				obj = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			else 
				obj = Instantiate (particleMesh);
			obj.transform.localScale = new Vector3 (particleScale, particleScale, particleScale);
			obj.transform.position = this.transform.TransformPoint (baked.vertices [i] + normalOffset * baked.normals[i]);
			if (particlesArePhysical) {
				Rigidbody rb = obj.AddComponent<Rigidbody> ();
				rb.useGravity = true;
			}
			Vector3 normal = new Vector3 (0, 0, 0); //TODO: implement this
			Destroy (obj, particleLifetime);
			particles.Add (new Particle(obj, normal));
			if (isChild)
				obj.transform.parent = this.transform;
		}
	}

	private void spawnParticlesAtTriangles() {
		baked = new Mesh();
		skin.BakeMesh(baked);
		for (int i = 0; i < baked.triangles.Length; i+=3 ) {
			GameObject obj;
			if (particleMesh == null) //defaults to sphere
				obj = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			else 
				obj = Instantiate (particleMesh);
			obj.transform.localScale = new Vector3 (particleScale, particleScale, particleScale);
			Vector3 BC; //compute random barycentric coordinates
			BC.x = Random.Range (0, 1.0f);
			BC.y = Random.Range (0, 1.0f);
			BC.z = Random.Range (0, 1.0f);
			//BC /= BC.x + BC.y + BC.z;
			Vector3 v1 = baked.vertices [baked.triangles [i]];
			Vector3 v2 = baked.vertices [baked.triangles [i + 1]];
			Vector3 v3 = baked.vertices [baked.triangles[i + 2]];
			Vector3 pos = BC.x * v1 + BC.y * v2 + BC.z * v3;
			Vector3 offset = new Vector3 (0, 0, 0);
			//compute triangle normal
			Vector3 normal = Vector3.Cross (v1 - v2, v1 - v3).normalized;
			if (normalOffset != 0)
				offset = normalOffset * normal;
			obj.transform.position = this.transform.TransformPoint (pos + offset);
			if (particlesArePhysical) {
				Rigidbody rb = obj.AddComponent<Rigidbody> ();
				rb.useGravity = true;
				rb.velocity = initialVelocity;
			}
			Destroy (obj, particleLifetime);
			particles.Add (new Particle(obj, normal));
			if (isChild && !particlesArePhysical)
				obj.transform.parent = this.transform;
		}
	}

	private void clearDestroyedParticles() {
		particles.RemoveAll (item => item.obj == null);
	}

	private void addRigidBodiesToParticles() {
		for (int i = 0; i < particles.Count; i++) {
			GameObject obj = particles [i].obj;
			if (obj == null)
				continue;
			Rigidbody rb = obj.AddComponent<Rigidbody> ();
			rb.useGravity = true;
			rb.velocity = initialVelocity;
			obj.transform.parent = null;
		}
	}

	private void addNormalSpeed() {
		for (int i = 0; i < particles.Count; i++) {
			Rigidbody rb = particles [i].obj.GetComponent<Rigidbody> ();
			Vector3 normal = particles [i].normal;
			rb.velocity += normalSpeed * normal;
		}
	}


	// Update is called once per frame
	void Update () {
//		this.transform.Translate(new Vector3(-.01f, 0, 0));
//		this.transform.Rotate(new Vector3(0, 0.3f, 0));
		if (rigidBodyStart > 0) {
			timeRemaining -= Time.deltaTime;
			if (timeRemaining <= 0) {
				addRigidBodiesToParticles ();
				addNormalSpeed ();
				rigidBodyStart = 0;
			}
		}
		if (spawnParticles && spawnTimeRemaining <= 0) {
			if (skin.enabled && disableMesh)
				skin.enabled = false;
			if (particles.Count < maxParticles)
			spawnParticlesAtTriangles ();
			//spawnParticlesAtVertices();
			clearDestroyedParticles ();
		} else
			spawnTimeRemaining -= Time.deltaTime;
	}
}
