using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceSpawner : MonoBehaviour
{
	public int maxSpawnAttempts;
	public float obstacleCheckRadius;
	public AudienceMember memberPrefab;

	public AudienceMember TrySpawnMember()
	{	
		Vector3 spawnPos = Vector3.zero;
		bool validPosition = false;
		int spawnAttempts = 0;

		while (!validPosition && spawnAttempts < maxSpawnAttempts)
		{
			print("Attempt " + spawnAttempts);
			++spawnAttempts;
			
			spawnPos = new Vector3(
				Random.Range(
					transform.position.x - 0.5f * transform.lossyScale.x,
					transform.position.x + 0.5f * transform.lossyScale.x),
				transform.position.y,
				Random.Range(
					transform.position.z - 0.5f * transform.lossyScale.z,
					transform.position.z + 0.5f * transform.lossyScale.z));
			
			validPosition = true;
			foreach (Collider col in Physics.OverlapSphere(spawnPos, obstacleCheckRadius))
			{
				if (col.GetComponentInParent<AudienceMember>())
				{
					validPosition = false;
					break;
				}
			}
		}
		
		if (validPosition)
		{
			return Instantiate(memberPrefab, spawnPos, Quaternion.identity);
		}
		else
		{
			print("No more attempts, mate!");
			return null;
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x, 0, transform.lossyScale.z));
	}

	public void DrawGizmosMember(AudienceMember member)
	{
		Gizmos.DrawWireSphere(member.transform.position, obstacleCheckRadius);
	}
}
