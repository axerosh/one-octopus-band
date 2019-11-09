using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestBubble : MonoBehaviour
{
	public Sprite drumSprite;
	public Sprite drumstickSprite;
	public Sprite guitarSprite;
	public GameObject tail;

	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		SetVisible(false);
	}

	private void SetVisible(bool flag)
	{
		spriteRenderer.enabled = flag;
		tail.SetActive(flag);
	}

	public void SetRequest(Request request)
    {
        switch (request)
		{
			case Request.Drum:
				spriteRenderer.sprite = drumSprite;
				break;
			case Request.Drumstick:
				spriteRenderer.sprite = drumstickSprite;
				break;
			case Request.Guitar:
				spriteRenderer.sprite = guitarSprite;
				break;
		}
		SetVisible(true);
	}

	public void DisableRequest()
	{
		SetVisible(false);
	}
}
