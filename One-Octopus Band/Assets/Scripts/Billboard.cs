using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	private void Start()
	{
		Align();
	}

	private void Update()
    {
		Align();
	}

	private void Align()
	{
		if (Camera.current != null)
		{
			transform.rotation = Camera.current.transform.rotation;
		}
	}
}
