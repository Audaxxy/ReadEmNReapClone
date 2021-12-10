using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObject/Cards", order = 1)]
public class Cards : ScriptableObject
{
	public cardData cardData;
}
[System.Serializable]
public struct cardData
{
	public string cardName;
	public int cardValue;
	public CardSuit cardSuit;
	public GameObject cardSymbol;
}
