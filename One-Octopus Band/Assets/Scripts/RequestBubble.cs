using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestBubble : MonoBehaviour
{
	public Sprite drumSprite;
	public Sprite triangleSprite;
	public Sprite drumstickSprite;
	public Sprite guitarSprite;
	public Sprite maracasSprite;
	public Sprite operaSprite;
	public Sprite cowbellSprite;
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
        switch (request.instrumentType)
		{
			case InstrumentType.Drum:
				spriteRenderer.sprite = drumSprite;
				break;
			case InstrumentType.Triangle:
				spriteRenderer.sprite = triangleSprite;
				break;
			// case InstrumentType.Drumstick:
			// 	spriteRenderer.sprite = drumstickSprite;
			// 	break;
			case InstrumentType.Guitar:
				spriteRenderer.sprite = guitarSprite;
				break;
			case InstrumentType.Maracas:
				spriteRenderer.sprite = maracasSprite;
				break;
			case InstrumentType.Opera:
				spriteRenderer.sprite = operaSprite;
				break;
			case InstrumentType.Cowbell:
				spriteRenderer.sprite = cowbellSprite;
				break;
		}
		SetVisible(true);
	}

	public void DisableRequest()
	{
		SetVisible(false);
	}
}
