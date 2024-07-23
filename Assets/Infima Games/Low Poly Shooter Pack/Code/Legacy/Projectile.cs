//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;
using System.Collections;
using InfimaGames.LowPolyShooterPack;
using Random = UnityEngine.Random;
using System.Linq;

namespace InfimaGames.LowPolyShooterPack.Legacy
{
	public class Projectile : MonoBehaviour
	{

		[Range(5, 100)]
		[Tooltip("After how long time should the bullet prefab be destroyed?")]
		public float destroyAfter;

		[Tooltip("If enabled the bullet destroys on impact")]
		public bool destroyOnImpact = false;

		[Tooltip("Minimum time after impact that the bullet is destroyed")]
		public float minDestroyTime;

		[Tooltip("Maximum time after impact that the bullet is destroyed")]
		public float maxDestroyTime;

		private float _projectileDamage = 0;

		[Header("Impact Effect Prefabs")]
		public Transform[] bloodImpactPrefabs;

		public Transform[] metalImpactPrefabs;
		public Transform[] dirtImpactPrefabs;
		public Transform[] concreteImpactPrefabs;

		public void Init(float projectileDamage)
		{
			//Grab the game mode service, we need it to access the player character!
			var gameModeService = ServiceLocator.Current.Get<IGameModeService>();
			//Ignore the main player character's collision. A little hacky, but it should work.
			Physics.IgnoreCollision(gameModeService.GetPlayerCharacter().GetComponent<Collider>(),
				GetComponent<Collider>());

			//Start destroy timer
			StartCoroutine(DestroyAfter());
			_projectileDamage = projectileDamage; 

        }

		//If the bullet collides with anything
		private void OnCollisionEnter(Collision collision)
		{
			//Ignore collisions with other projectiles.
			if (collision.gameObject.GetComponent<Projectile>() != null)
				return;
			// //Ignore collision if bullet collides with "Player" tag
			if (collision.gameObject.CompareTag("Enemy"))
			{
				var FSMs = collision.gameObject.GetComponentsInParent<PlayMakerFSM>();
                if (FSMs.Length == 0)
                {
                    Debug.LogWarning("No PlayMakerFSM components found in parent objects.");
                }

                PlayMakerFSM damageSystem = null;
                for (int i = 0; i < FSMs.Length; i++)
				{
					if (FSMs[i].Fsm.Name == "Damage System")
					{
                        damageSystem = FSMs[i];
                    }
				}
				if (damageSystem != null)
				{
                    var floatVariables = damageSystem.FsmVariables.FloatVariables;
                    var projectileDamageVariable = floatVariables.FirstOrDefault(f =>
                    {
                        return f.Name == "DamageToApply";
                    });
                    if (projectileDamageVariable != null)
                    {
                        projectileDamageVariable.Value = _projectileDamage;
                    }
                    else
                    {
                        Debug.LogError($"Variable {_projectileDamage} not found in FloatVariables.");
                    }
                    damageSystem.SendEvent("Hit");

                }
                Destroy(gameObject);
            }
			//
			//If destroy on impact is false, start 
			//coroutine with random destroy timer
			if (!destroyOnImpact)
			{
				StartCoroutine(DestroyTimer());
			}
			//Otherwise, destroy bullet on impact
			else
			{
				Destroy(gameObject);
			}

			//If bullet collides with "Blood" tag
			if (collision.transform.tag == "Blood")
			{
				//Instantiate random impact prefab from array
				Instantiate(bloodImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "Metal" tag
			if (collision.transform.tag == "Metal")
			{
				//Instantiate random impact prefab from array
				Instantiate(metalImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "Dirt" tag
			if (collision.transform.tag == "Dirt")
			{
				//Instantiate random impact prefab from array
				Instantiate(dirtImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "Concrete" tag
			if (collision.transform.tag == "Concrete")
			{
				//Instantiate random impact prefab from array
				Instantiate(concreteImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "Target" tag
			if (collision.transform.tag == "Target")
			{
				//Toggle "isHit" on target object
				collision.transform.gameObject.GetComponent
					<TargetScript>().isHit = true;
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "ExplosiveBarrel" tag
			if (collision.transform.tag == "ExplosiveBarrel")
			{
				//Toggle "explode" on explosive barrel object
				collision.transform.gameObject.GetComponent
					<ExplosiveBarrelScript>().explode = true;
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "GasTank" tag
			if (collision.transform.tag == "GasTank")
			{
				//Toggle "isHit" on gas tank object
				collision.transform.gameObject.GetComponent
					<GasTankScript>().isHit = true;
				//Destroy bullet object
				Destroy(gameObject);
			}
		}

		private IEnumerator DestroyTimer()
		{
			//Wait random time based on min and max values
			yield return new WaitForSeconds
				(Random.Range(minDestroyTime, maxDestroyTime));
			//Destroy bullet object
			Destroy(gameObject);
		}

		private IEnumerator DestroyAfter()
		{
			//Wait for set amount of time
			yield return new WaitForSeconds(destroyAfter);
			//Destroy bullet object
			Destroy(gameObject);
		}
	}
}