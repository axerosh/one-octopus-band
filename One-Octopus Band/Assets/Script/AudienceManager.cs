using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
	public int maxMemberCount;
	private int currentMemberCount = 0;

	private List<AudienceMember> members = new List<AudienceMember>();
	private AudienceSpawner spawner;

	void Start()
	{
		spawner = GetComponent<AudienceSpawner>();
		while (currentMemberCount < maxMemberCount)
		{
			AudienceMember newMember = spawner.SpawnAudienceMember();
			if (newMember != null)
			{
				++currentMemberCount;
				members.Add(newMember);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		foreach (var member in members)
		{
			spawner.DrawGizmosMember(member);
		}
	}
}
