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
			++spawnAttempts;
			
			spawnPos = new Vector3(
				Random.Range(
					transform.position.x - 0.5f * transform.localScale.x,
					transform.position.x + 0.5f * transform.localScale.x),
				0.0f,
				Random.Range(
					transform.position.z - 0.5f * transform.localScale.z,
					transform.position.z + 0.5f * transform.localScale.z));
			
			validPosition = true;
			foreach (Collider col in Physics.OverlapSphere(spawnPos, obstacleCheckRadius))
			{
				if (col.GetComponent<AudienceMember>())
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
			return null;
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, 0, transform.localScale.z));
	}

	public void DrawGizmosMember(AudienceMember member)
	{
		Gizmos.DrawWireSphere(member.transform.position, obstacleCheckRadius);
	}
}
