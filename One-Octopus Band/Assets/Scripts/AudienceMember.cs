using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour
{
	public float minJumpSpeed;
	public float maxJumpSpeed;
	public float acceleration;

	private float initY;
	private float speed = 0.0f;
	private RequestBubble requestBubble;

	private void Start()
	{
		initY = transform.position.y;
		requestBubble = GetComponentInChildren<RequestBubble>();
	}

	private void Update()
	{
		if (speed <= 0.0 && transform.position.y < initY)
		{
			// Just landed
			speed = Random.Range(minJumpSpeed, maxJumpSpeed);
		}
		else
		{
			// In the air
			speed += acceleration * Time.deltaTime;
		}
		transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
	}

	public void SetRequest(Request request)
	{
		requestBubble.SetRequest(request);
	}

	public void DisableRequest()
	{
		requestBubble.DisableRequest();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay(transform.position, new Vector3(0.0f, speed, 0.0f));
	}
}
