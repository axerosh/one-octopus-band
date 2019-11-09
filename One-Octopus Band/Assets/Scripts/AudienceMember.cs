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

	private void Start()
	{
		initY = transform.position.y;
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
		Color color = Color.black;
		switch (request.InstrumentType)
		{
			case InstrumentType.Drum:
				color = Color.red;
				break;
			//case InstrumentType.Chicken:
			//	color = Color.yellow;
			//	break;
            case InstrumentType.Drumshtick:
                color = Color.green;
                break;
            case InstrumentType.Guitar:
                color = Color.cyan;
                break;
            //case InstrumentType.Triangle:
            //    color = Color.grey;
            //    break;
		}
		GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
	}

	public void DisableRequest()
	{
		GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.white);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawRay(transform.position, new Vector3(0.0f, speed, 0.0f));
	}
}
