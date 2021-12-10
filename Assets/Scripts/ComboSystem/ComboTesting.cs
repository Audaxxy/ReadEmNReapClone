using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTesting : MonoBehaviour
{
    ComboSystem cs;

    public Cards[] CardSO_Straight;
    public Cards[] CardSO_2Pair;
    public Cards[] CardSO_FullHouse;

    //Create Deck
    List<CardType> ct = new List<CardType>();

    // Start is called before the first frame update
    void Start()
    {
        cs = GetComponent<ComboSystem>();

        if(cs == null)
        {
            Debug.Log("Combo System was null");
            return;
        }

        cs.pokerComboRetreivalDelegate += LogResults;


        for(int i = 0; i < 52; i ++)
        {
            ct.Add(new CardType((i % 13) + 1, (CardSuit)(i / 13)));
        }

        /*0 = 0Hearts
          1 = 1Hearts
          2 = 2Hearts
          3 = 3Heart
          4 = 4Hearts
          5 = 5Hearts
          6 = 6Hearts
          7 = 7Hearts
          8 = 8Hearts
          9 = 9Hearts
          10 = 10Hearts
           11= 11Hearts
           12= 12Hearts
          13 = 0Clubs
          14 = 1Clubs
          15 = 2Clubs
          16 = 3Clubs
          17 = 4Clubs
          18 = 5Clubs
          19 = 6Clubs
          20 = 7Clubs
          21 = 8Clubs
          22 = 9Clubs
          23 = 10Clubs
          24 = 11Clubs
          25 = 12Clubs
          26 = 0Spades
          27 = 1Spades
          28 = 2Spades
          29 = 3Spades
          30 = 4Spades
          31 = 5Spades
          32 = 6Spades
          33 = 7Spades
          34 = 8Spades
          35 = 9Spades
          36 = 10Spades
          37 = 11Spades
          38 = 12Spades
          39 = 0Diamonds
          40 = 1Diamonds
          41 = 2Diamonds
          42 = 3Diamonds
          43 = 4Diamonds
          44 = 5Diamonds
          45 = 6Diamonds
          46 = 7Diamonds
          47 = 8Diamonds
          48 = 9Diamonds
           49= 10Diamonds
          50 = 11Diamonds
          51 = 12Diamonds*/

        //Run Tests
        CreateAndCheckHandsTest(ct[0], ct[13], ct[26], ct[39], ct[2], PokerCombo.FourOfAKind);
        CreateAndCheckHandsTest(ct[0], ct[14], ct[28], ct[16], ct[43], PokerCombo.Straight);
        CreateAndCheckHandsTest(ct[51], ct[49], ct[50], ct[48], ct[0], PokerCombo.Straight);
        CreateAndCheckHandsTest(ct[51], ct[49], ct[50], ct[48], ct[39], PokerCombo.RoyalFlush);
        CreateAndCheckHandsTest(ct[51], ct[49], ct[41], ct[48], ct[39], PokerCombo.Flush);


        CreateAndCheckHandsTest(CardSO_2Pair, PokerCombo.TwoPairs);
        CreateAndCheckHandsTest(CardSO_Straight, PokerCombo.Straight);
        CreateAndCheckHandsTest(CardSO_FullHouse, PokerCombo.FullHouse);
    }

    void CreateAndCheckHandsTest(CardType c1, CardType c2, CardType c3, CardType c4, CardType c5, PokerCombo ExpectedResult)
    {
        cs.AddCardToCombo(c1.cardValue, c1.cardSuit);
        cs.AddCardToCombo(c2.cardValue, c2.cardSuit);
        cs.AddCardToCombo(c3.cardValue, c3.cardSuit);
        cs.AddCardToCombo(c4.cardValue, c4.cardSuit);
        cs.AddCardToCombo(c5.cardValue, c5.cardSuit);

        //cs.HandleComboEnd();

        Debug.Log(ExpectedResult == TestComboHand);
    }

    void CreateAndCheckHandsTest(Cards[] cardarr, PokerCombo ExpectedResult)
    {
        cs.AddCardToCombo(cardarr[0].cardData);
        cs.AddCardToCombo(cardarr[1].cardData);
        cs.AddCardToCombo(cardarr[2].cardData);
        cs.AddCardToCombo(cardarr[3].cardData);
        cs.AddCardToCombo(cardarr[4].cardData);

        //cs.HandleComboEnd();

        Debug.Log(ExpectedResult == TestComboHand);
    }

    PokerCombo TestComboHand = PokerCombo.NO_COMBO;
    void LogResults(PokerCombo combo)
    {
        Debug.Log(combo.ToString());

        TestComboHand = combo;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            CreateAndCheckHandsTest(ct[0], ct[13], ct[26], ct[39], ct[2], PokerCombo.FourOfAKind);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            CreateAndCheckHandsTest(ct[51], ct[49], ct[50], ct[48], ct[39], PokerCombo.RoyalFlush);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            CreateAndCheckHandsTest(ct[51], ct[49], ct[50], ct[48], ct[0], PokerCombo.Straight);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateAndCheckHandsTest(CardSO_2Pair, PokerCombo.TwoPairs);
        }

        //
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cs.AddCardToCombo(1, CardSuit.Hearts);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            cs.AddCardToCombo(2, CardSuit.Hearts);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            cs.AddCardToCombo(3, CardSuit.Hearts);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            cs.AddCardToCombo(4, CardSuit.Hearts);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            cs.AddCardToCombo(5, CardSuit.Hearts);
        }


        //
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cs.AddCardToCombo(1, CardSuit.Clubs);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            cs.AddCardToCombo(13, CardSuit.Clubs);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            cs.AddCardToCombo(12, CardSuit.Clubs);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            cs.AddCardToCombo(11, CardSuit.Clubs);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            cs.AddCardToCombo(10, CardSuit.Clubs);
        }

    }
}
