using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardHandler : MonoBehaviour
{
	public Cards card;//ScriptableObject
	public GameObject cardPrefab;

	public cardData cardData; //local ScriptableObject sctruct
	public GameObject comboManager;
	public GameObject spawnManager;
	private ComboSystem comboSystem;
	private EnemySpawner spawnSystem;
	//private ComboUIManager myUi;
	[SerializeField]
	private string cardName; //Name of the card
	[SerializeField]
	private int cardValue; //Number Value of the card 
	[SerializeField]
	private GameObject cardSymbol; //Suit of the card
	private int symbolsRemaining;
	
	public Transform spawnCenter1, spawnCenter2, spawnCenter3, spawnCenter4, spawnCenter5, spawnCenter6, spawnCenter7, spawnLeft1, spawnLeft2, spawnLeft3, spawnLeft4, spawnLeft5, spawnRight1, spawnRight2, spawnRight3, spawnRight4, spawnRight5;//Transform position for symbol spawning

	

	private void Awake()
	{
		
		
		//Calls setSymbols function which spawns the symbols
		comboManager = GameObject.FindGameObjectWithTag("comboManager");
		comboSystem = comboManager.GetComponent<ComboSystem>();
		//myUi = comboManager.GetComponent<ComboUIManager>();

		spawnManager = GameObject.FindGameObjectWithTag("spawnManager");
		spawnSystem = spawnManager.GetComponent<EnemySpawner>();



	}
	

	//      CARD LAYOUT

	//  Left1   Center1  Right1

	//          Center2

	//  Left2   Center3  Right2

	//  Left3   Center4  Right3

	//  Left4   Center5  Right4

	//          Center6  

	//  Left5   Center7  Right5  

	//Instantiates symbols based on the symbol of the card at positions defined by transforms on the card prefab based upon the value of the card
	public void setSymbols()
	{
		//Populates local struct with data from ScriptableObject struct
		cardData = card.cardData;

		//sets card values to struct values
		cardName = cardData.cardName;
		cardSymbol = cardData.cardSymbol;
		cardValue = cardData.cardValue;

		symbolsRemaining = cardValue;
		switch (cardValue)
		{
			//ACE
			case 1:

				Instantiate(cardSymbol, spawnCenter4.position, cardSymbol.transform.rotation, spawnCenter4);

				break;

			//Two
			case 2:

				Instantiate(cardSymbol, spawnCenter1.position, cardSymbol.transform.rotation, spawnCenter1);
				Instantiate(cardSymbol, spawnCenter7.position, cardSymbol.transform.rotation, spawnCenter7);

				break;

			//Three
			case 3:

				Instantiate(cardSymbol, spawnCenter1.position, cardSymbol.transform.rotation, spawnCenter1);
				Instantiate(cardSymbol, spawnCenter4.position, cardSymbol.transform.rotation, spawnCenter4);
				Instantiate(cardSymbol, spawnCenter7.position, cardSymbol.transform.rotation, spawnCenter7);

				break;
			//Four
			case 4:
				Instantiate(cardSymbol, spawnLeft1.position, cardSymbol.transform.rotation, spawnLeft1);
				Instantiate(cardSymbol, spawnLeft5.position, cardSymbol.transform.rotation, spawnLeft5);

				Instantiate(cardSymbol, spawnRight1.position, cardSymbol.transform.rotation, spawnRight1);
				Instantiate(cardSymbol, spawnRight5.position, cardSymbol.transform.rotation, spawnRight5);

				break;

			//Five
			case 5:

				Instantiate(cardSymbol, spawnLeft1.position, cardSymbol.transform.rotation, spawnLeft1);
				Instantiate(cardSymbol, spawnLeft5.position, cardSymbol.transform.rotation, spawnLeft5);

				Instantiate(cardSymbol, spawnRight1.position, cardSymbol.transform.rotation, spawnRight1);
				Instantiate(cardSymbol, spawnRight5.position, cardSymbol.transform.rotation, spawnRight5);

				Instantiate(cardSymbol, spawnCenter4.position, cardSymbol.transform.rotation, spawnCenter4);

				break;

			//Six
			case 6:

				Instantiate(cardSymbol, spawnLeft1.position, cardSymbol.transform.rotation, spawnLeft1);
				Instantiate(cardSymbol, spawnLeft3.position, cardSymbol.transform.rotation, spawnLeft3);
				Instantiate(cardSymbol, spawnLeft5.position, cardSymbol.transform.rotation, spawnLeft5);

				Instantiate(cardSymbol, spawnRight1.position, cardSymbol.transform.rotation, spawnRight1);
				Instantiate(cardSymbol, spawnRight3.position, cardSymbol.transform.rotation, spawnRight3);
				Instantiate(cardSymbol, spawnRight5.position, cardSymbol.transform.rotation, spawnRight5);

				break;

			//Seven
			case 7:

				Instantiate(cardSymbol, spawnLeft1.position, cardSymbol.transform.rotation, spawnLeft1);
				Instantiate(cardSymbol, spawnLeft3.position, cardSymbol.transform.rotation, spawnLeft3);
				Instantiate(cardSymbol, spawnLeft5.position, cardSymbol.transform.rotation, spawnLeft5);

				Instantiate(cardSymbol, spawnRight1.position, cardSymbol.transform.rotation, spawnRight1);
				Instantiate(cardSymbol, spawnRight3.position, cardSymbol.transform.rotation, spawnRight3);
				Instantiate(cardSymbol, spawnRight5.position, cardSymbol.transform.rotation, spawnRight5);

				Instantiate(cardSymbol, spawnCenter3.position, cardSymbol.transform.rotation, spawnCenter3);

				break;

			//Eight
			case 8:

				Instantiate(cardSymbol, spawnLeft1.position, cardSymbol.transform.rotation, spawnLeft1);
				Instantiate(cardSymbol, spawnLeft3.position, cardSymbol.transform.rotation, spawnLeft3);
				Instantiate(cardSymbol, spawnLeft5.position, cardSymbol.transform.rotation, spawnLeft5);

				Instantiate(cardSymbol, spawnRight1.position, cardSymbol.transform.rotation, spawnRight1);
				Instantiate(cardSymbol, spawnRight3.position, cardSymbol.transform.rotation, spawnRight3);
				Instantiate(cardSymbol, spawnRight5.position, cardSymbol.transform.rotation, spawnRight5);

				Instantiate(cardSymbol, spawnCenter3.position, cardSymbol.transform.rotation, spawnCenter3);
				Instantiate(cardSymbol, spawnCenter5.position, cardSymbol.transform.rotation, spawnCenter5);

				break;

			//Nine
			case 9:

				Instantiate(cardSymbol, spawnLeft1.position, cardSymbol.transform.rotation, spawnLeft1);
				Instantiate(cardSymbol, spawnLeft2.position, cardSymbol.transform.rotation, spawnLeft2);
				Instantiate(cardSymbol, spawnLeft4.position, cardSymbol.transform.rotation, spawnLeft4);
				Instantiate(cardSymbol, spawnLeft5.position, cardSymbol.transform.rotation, spawnLeft5);

				Instantiate(cardSymbol, spawnRight1.position, cardSymbol.transform.rotation, spawnRight1);
				Instantiate(cardSymbol, spawnRight2.position, cardSymbol.transform.rotation, spawnRight2);
				Instantiate(cardSymbol, spawnRight4.position, cardSymbol.transform.rotation, spawnRight4);
				Instantiate(cardSymbol, spawnRight5.position, cardSymbol.transform.rotation, spawnRight5);

				Instantiate(cardSymbol, spawnCenter4.position, cardSymbol.transform.rotation, spawnCenter4);

				break;

			//Ten
			case 10:

				Instantiate(cardSymbol, spawnLeft1.position, cardSymbol.transform.rotation, spawnLeft1);
				Instantiate(cardSymbol, spawnLeft2.position, cardSymbol.transform.rotation, spawnLeft2);
				Instantiate(cardSymbol, spawnLeft4.position, cardSymbol.transform.rotation, spawnLeft4);
				Instantiate(cardSymbol, spawnLeft5.position, cardSymbol.transform.rotation, spawnLeft5);

				Instantiate(cardSymbol, spawnRight1.position, cardSymbol.transform.rotation, spawnRight1);
				Instantiate(cardSymbol, spawnRight2.position, cardSymbol.transform.rotation, spawnRight2);
				Instantiate(cardSymbol, spawnRight4.position, cardSymbol.transform.rotation, spawnRight4);
				Instantiate(cardSymbol, spawnRight5.position, cardSymbol.transform.rotation, spawnRight5);

				Instantiate(cardSymbol, spawnCenter2.position, cardSymbol.transform.rotation, spawnCenter2);
				Instantiate(cardSymbol, spawnCenter6.position, cardSymbol.transform.rotation, spawnCenter6);

				break;
			//Jack
			case 11:
				break;
			//Queen
			case 12:
				break;
			//King
			case 13:
				break;
		}
		

	}
	public void loseSymbol()
	{
		symbolsRemaining--;
		if (symbolsRemaining <= 0)
		{

			comboSystem.AddCardToCombo(cardData);
			spawnSystem.EnemyDead(card);
			//GameObject newCard = Instantiate(cardPrefab, myUi.handList.transform);
			//newCard.GetComponentInChildren<TMP_Text>().text = cardName;
			Destroy(gameObject);
		}
		
	}

}
