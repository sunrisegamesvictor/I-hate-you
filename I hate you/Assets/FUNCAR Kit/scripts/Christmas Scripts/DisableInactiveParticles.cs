using UnityEngine;

//temporary work around for particles bug in Unity 5.3.1, this script can be removed in 5.3.2 and newer//unity issue tracker 755423
public class DisableInactiveParticles : MonoBehaviour {

	ParticleSystem.Particle[] unused = new ParticleSystem.Particle[1];

	void Awake()
	{
		GetComponent<ParticleSystemRenderer>().enabled = false;
	}

	void LateUpdate()
	{
		GetComponent<ParticleSystemRenderer>().enabled = GetComponent<ParticleSystem>().GetParticles(unused) > 0;
	}
}
	
