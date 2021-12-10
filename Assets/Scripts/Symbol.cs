using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
	private CardHandler myHandler;
	public AudioSource audioSource;
	public AudioClip symbolDeath;
	private void Awake()
	{
		myHandler = GetComponentInParent<CardHandler>();
	}
	public void gotShot()
    {
		audioSource.PlayOneShot(symbolDeath);
		myHandler.loseSymbol();
        Destroy(gameObject);
		
    }
}
