using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class AudienceManager : MonoBehaviour
{
	private int score = 0;
    public Text scoreList;
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
		foreach (var instrument in System.Enum.GetValues(typeof(InstrumentType)))
		{
			var request = ScriptableObject.CreateInstance<Request>();
			request.instrumentType = (InstrumentType)instrument;
			request.Progress = 0;
			freeRequests.Add(request);
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


	public void OnSmacked(InstrumentType instrument)
	{
        List<Request> toRemove = new List<Request>();
        
        
        foreach (var requestMember in activeRequests)
        {
            var request = requestMember.Key;
            var member = requestMember.Value;
            Debug.Log(request.instrumentType + " " + request.Progress);
            if (instrument != request.instrumentType) continue;
            
            

            if (request.Progress == 5)
            {
                request.Progress = 0;
                toRemove.Add(request);
                score += 10;
				if (scoreList != null)
				{
					scoreList.text = $"Score: {score}";
				}
            }
            else
            {
                request.Progress++;
            }
        }
        foreach (var request in toRemove)
        {
			print("Remove request " + request);
            activeRequests.TryGetValue(request, out var member);
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
