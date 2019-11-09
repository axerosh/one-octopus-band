using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudienceManager : MonoBehaviour
{

	public int maxMemberCount;
	public int maxActiveRequestCount;
	public float minRequestCooldown;
	public float maxRequestCooldown;
	public RequestEvent onNewRequest;

	private float newRequestTime = 0.0f;
	private List<Request> freeRequests = new List<Request>();
	private List<AudienceMember> members = new List<AudienceMember>();
	private List<AudienceMember> freeMembers = new List<AudienceMember>();
	private Dictionary<Request, AudienceMember> activeRequests = new Dictionary<Request, AudienceMember>();
	private AudienceSpawner spawner;

	private float startTime;

	void Start()
	{
		startTime = Time.time;
		foreach (var request in System.Enum.GetValues(typeof(Request)))
		{
			freeRequests.Add((Request)request);
		}

		spawner = GetComponent<AudienceSpawner>();
		while (members.Count < maxMemberCount)
		{
			AudienceMember newMember = spawner.TrySpawnMember();
			if (newMember != null)
			{
				members.Add(newMember);
				freeMembers.Add(newMember);
			}
		}
	}

	private void Update()
	{
		if (Time.time >= newRequestTime) {
			newRequestTime = Time.time + Random.Range(minRequestCooldown, maxRequestCooldown);
			TryIssueRequest();
		}
	}

	private void TryIssueRequest()
	{
		if (activeRequests.Count < maxActiveRequestCount
			&& freeRequests.Count > 0 && freeMembers.Count > 0)
		{
			var request = freeRequests[Random.Range(0, freeRequests.Count)];
			var member = freeMembers[Random.Range(0, freeMembers.Count)];
			freeRequests.Remove(request);
			freeMembers.Remove(member);
			activeRequests.Add(request, member);
			member.SetRequest(request);
			onNewRequest.Invoke(request);
		}
	}

	public void OnRequestCompleted(Request request)
	{
		if (activeRequests.TryGetValue(request, out var member))
		{
			freeRequests.Add(request);
			freeMembers.Add(member);
			activeRequests.Remove(request);
			member.DisableRequest();
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
