using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
	public float pocessingTime;

	public RequestEvent onRequestCompleted;

	private class RequestProcess
	{
		public Request request;
		public float endTime;

		public RequestProcess(Request request, float endTime)
		{
			this.request = request;
			this.endTime = endTime;
		}
	}

	private Queue<RequestProcess> requestQueue = new Queue<RequestProcess>();
	
	public void OnNewRequest(Request request)
	{
		requestQueue.Enqueue(new RequestProcess(request, Time.time + pocessingTime));
	}

	void Update()
    {
        while (requestQueue.Count > 0 && Time.time >= requestQueue.Peek().endTime)
		{
			onRequestCompleted.Invoke(requestQueue.Dequeue().request);
		}
    }
}
