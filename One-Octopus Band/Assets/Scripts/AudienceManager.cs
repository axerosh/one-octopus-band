using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


public class AudienceManager : MonoBehaviour
{
    private int score = 0;
    public Text scoreDisplay;
    public Text gameOverDisplay;
    public Button restart;
    public int maxFailedRequests = 10;
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

    private int failedRequests;

	void Start()
	{
		startTime = Time.time;

		foreach (var instrument in System.Enum.GetValues(typeof(InstrumentType)))
		{
			var request = ScriptableObject.CreateInstance<Request>();
			request.instrumentType = (InstrumentType)instrument;
			request.maxTimeLeft = 10.0;
			request.timeLeft = request.maxTimeLeft;
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

        gameOverDisplay.enabled = false;
        restart.enabled = false;
        restart.gameObject.SetActive(false);
        for (int i = 0; i < restart.transform.childCount; i++)
        {
            restart.transform.GetChild(i).gameObject.SetActive(false);
        }
	}

	private void Update()
	{
		if (Time.time >= newRequestTime) {
			newRequestTime = Time.time + Random.Range(minRequestCooldown, maxRequestCooldown);
			TryIssueRequest();
		}
        List<Request> toRemove = new List<Request>();
        foreach (var requestMember in activeRequests)
        {
            requestMember.Key.timeLeft -= Time.deltaTime;
            if (requestMember.Key.timeLeft < 0) toRemove.Add(requestMember.Key);

        }
        foreach (var request in toRemove)
        {
            activeRequests.TryGetValue(request, out var member);
            if (!request.met)
            {
                if (failedRequests >= maxFailedRequests)
                {

                    gameOverDisplay.enabled = true;
                    restart.enabled = true;
                    restart.gameObject.SetActive(true);
                    for (int i = 0; i < restart.transform.childCount; i++)
                    {
                        restart.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                else
                {
                    failedRequests++;

                    Debug.Log(failedRequests + " failed requests");
                }
            }
            request.timeLeft = request.maxTimeLeft;
            freeRequests.Add(request);
            freeMembers.Add(member);
            activeRequests.Remove(request);
            member.DisableRequest();
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

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(0);
    }
	
    public void OnSmacked(InstrumentType instrument)
	{
        List<Request> toRemove = new List<Request>();
        
        foreach (var requestMember in activeRequests)
        {
            var request = requestMember.Key;
            var member = requestMember.Value;

            if (request.instrumentType == instrument)
            {
                if (request.met)
                {
                    score += 10;
                    scoreDisplay.text = $"Score: {score}";
                }
                else
                {
                    request.met = true;
					member.CompleteRequest();
				}
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
