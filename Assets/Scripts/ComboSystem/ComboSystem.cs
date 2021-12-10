using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSuit : int
{
    Hearts = 0,
    Clubs,
    Spades,
    Diamonds,
}

public struct CardType
{
    public CardSuit cardSuit;
    public int cardValue;

    public CardType(int val, CardSuit suit)
    {
        cardValue = val;
        cardSuit = suit;
    }
}

public enum PokerCombo
{
    NO_COMBO        = 0,
    OnePair         = 25,
    TwoPairs        = 40,
    ThreeOfAKind    = 65, 
    Straight        = 90, // 5 Consecutive numbers
    Flush           = 110, //5 All same suit
    FullHouse       = 135, // 2&3
    FourOfAKind     = 150,
    StraightFlush   = 185,
    RoyalFlush      = 250,
}

//A Delegate to tell others what combo we got 
public delegate void PokerComboRetreival(PokerCombo combo);
//A Delegate to tell other classes who listen what card was added
public delegate void AddCardToHandDelegate(CardType card);
//Delegate when the hand gets cleared
public delegate void ClearThePokerHandDelegate();

public class ComboSystem : MonoBehaviour
{
    public List<CardType> CurrentHand = new List<CardType>();

    public float MaxTimer = 10.0f;
    public float CurrentTimer = 0.0f;

    bool bComboIsActive = false;

    public PokerComboRetreival pokerComboRetreivalDelegate;
    public AddCardToHandDelegate addCardToHandDelegate;
    public ClearThePokerHandDelegate clearHandDelegate;

    public void Update()
    {
        if (bComboIsActive == false)
        {
            return;
        }
        
        CurrentTimer += Time.deltaTime;

        if(CurrentTimer >= MaxTimer)
        {
            HandleComboEnd();
        }
    }

    public void HandleComboEnd()
    {
        //Check solutions
        PokerCombo combo = CheckPokerHand();

        if(pokerComboRetreivalDelegate != null)
            pokerComboRetreivalDelegate.Invoke(combo);

        //Flush
        FlushCombo();

        bComboIsActive = false;
    }

    public void AddCardToCombo(CardType card)
    {
        AddCardToCombo(card.cardValue, card.cardSuit);
    }

    public void AddCardToCombo(int value, int suit)
    {
        if (suit < 0 || suit > 3)
        {
            Debug.Log("The suit checked was either less than 0 or greater than three.  Modulousing the value.  VALUE: " + suit.ToString());
        }

        AddCardToCombo(value, (CardSuit)(suit % 4));
    }

    public void AddCardToCombo(cardData data)
    {
        AddCardToCombo(data.cardValue, data.cardSuit);
    }

    public void AddCardToCombo(int value, CardSuit suit)
    {
        //Debug.Log("Adding card to hand: " + value.ToString() + " of " + suit.ToString());

        CardType card = new CardType();

        card.cardSuit = suit;
        card.cardValue = value;

        CurrentHand.Add(card);

        if (addCardToHandDelegate != null) { 
            addCardToHandDelegate.DynamicInvoke(card); 
        }

        CurrentHand.Sort((CardType c1, CardType c2) =>
        c1.cardValue.CompareTo(c2.cardValue));

        StartTimer();

        if (CurrentHand.Count == 5)
        {
            //Debug.Log("Combo is full, ending combo");
            HandleComboEnd();
        }
    }

    public void FlushCombo()
    {
        //Debug.Log("Clearing combo");
        CurrentHand.Clear();

        if(clearHandDelegate != null)
            clearHandDelegate.Invoke();
    }


    public void StartTimer()
    {
        CurrentTimer = 0.0f;
        bComboIsActive = true;
    }
    
    public PokerCombo CheckPokerHand()
    {
        /*  1 Pair
         *  2 Pair
         *  Three of a kind
         *  Straight -- 12345
         *  Flush   --  Same Suit
         *  Full House -- 2 + 3
         *  Four of a kind
         *  Straight Flush
         *  Royal Flush */

        if (CurrentHand.Count < 2)
        {
            return PokerCombo.NO_COMBO; //We have 1 or 2 values no need to check
        }

        else if (CurrentHand.Count == 2)
        {
            if (CurrentHand[0].cardValue == CurrentHand[1].cardValue)
            {
                return PokerCombo.OnePair;
            }
            return PokerCombo.NO_COMBO;
        }

        else if (CurrentHand.Count == 3)
        {
            if (CurrentHand[0].cardValue == CurrentHand[1].cardValue &&
                CurrentHand[1].cardValue == CurrentHand[2].cardValue)
            {
                return PokerCombo.ThreeOfAKind;
            }
        }

        else if (CurrentHand.Count == 4)
        {
            if (CurrentHand[0].cardValue == CurrentHand[1].cardValue &&
               CurrentHand[1].cardValue == CurrentHand[2].cardValue &&
               CurrentHand[2].cardValue == CurrentHand[3].cardValue)
            {
                return PokerCombo.FourOfAKind;
            }
        }


        Dictionary<int, int> CardValueCounts = new Dictionary<int, int>();
        Dictionary<CardSuit, int> CardSuitCounts = new Dictionary<CardSuit, int>();

        foreach (CardType c in CurrentHand)
        {
            if(!CardValueCounts.ContainsKey(c.cardValue))
            {
                //IF we don't already have the value in our dictionary then we add it
                CardValueCounts.Add(c.cardValue, 1);
            }
            else
            {
                CardValueCounts[c.cardValue]++;
            }

            if(!CardSuitCounts.ContainsKey(c.cardSuit))
            {
                CardSuitCounts.Add(c.cardSuit, 1);
            }
            else
            {
                CardSuitCounts[c.cardSuit]++;
            }

        }

        PokerCombo FoundCombo = PokerCombo.NO_COMBO;

        if(CardSuitCounts.Keys.Count == 1 && CurrentHand.Count == 5)
        {
            //We have 1 suit which means we have a flush of some sort
            
            if(CheckSequential())
            {
                //Since we have sorted the values from lowest to highest we can check if the first element is 1 and the last is 13
                if(CurrentHand[0].cardValue == 1 && CurrentHand[4].cardValue == 13)
                {
                    FoundCombo = CompareHands(FoundCombo, PokerCombo.RoyalFlush);
                }

                FoundCombo = CompareHands(FoundCombo, PokerCombo.StraightFlush);
            }

            FoundCombo = CompareHands(FoundCombo, PokerCombo.Flush);
        }


        //Check for a Straight
        if(CheckSequential())
        {
            FoundCombo = CompareHands(FoundCombo, PokerCombo.Straight);
        }


        //A Possible Full house or 4 of a kind
        if(CardValueCounts.Keys.Count == 2)
        {
            if(CardValueCounts.ContainsValue(2) &&
                CardValueCounts.ContainsValue(3))
            {
                FoundCombo = CompareHands(FoundCombo, PokerCombo.FullHouse);
            }


            //If the card values contains 4 of 1 value than we have four of a kind
            if (CardValueCounts.ContainsValue(4))
            {
                FoundCombo = CompareHands(FoundCombo, PokerCombo.FourOfAKind);
            }
        }


        else if(CardValueCounts.ContainsValue(3))
        {
            //A Good chance that we have 3 of a kind
            FoundCombo = CompareHands(FoundCombo, PokerCombo.ThreeOfAKind);
        }

        //If the card value counts contain 2 we at least have a pair
        if (CardValueCounts.ContainsValue(2))
        {
            if (CardValueCounts.Keys.Count <= 3) //If we have less than 3 unique values than we either have a full house or 2 pairs
            {
                FoundCombo = CompareHands(FoundCombo, PokerCombo.TwoPairs);
            }
            FoundCombo = CompareHands(FoundCombo, PokerCombo.OnePair);
        }

        return FoundCombo;
    }

    PokerCombo CompareHands(PokerCombo Original, PokerCombo New)
    {
        if(Original < New)
        {
            return New;
        }
        return Original;
    }

    bool CheckSequential()
    {
        if (CurrentHand.Count < 5)
        { //If we don't have 5 cards there is no point in checking
            return false; 
        } 

        //Since we are sorted we can check this
        int first, second, third, fourth, fifth;
        first = CurrentHand[0].cardValue;
        second = CurrentHand[1].cardValue;
        third = CurrentHand[2].cardValue;
        fourth = CurrentHand[3].cardValue;
        fifth = CurrentHand[4].cardValue;


        //Check values 2 - 5 since we have sorted them and they should be in value order
        
        //1 = 2-1
        //2 = 3-1
        //3 = etc..
        
        if( second == (third - 1) &&
            third == (fourth - 1) &&
            fourth == (fifth - 1) )
        {
            //Check for Ace and King
            if (first == 1 && fifth == 13)
                return true;

            //Check to make sure the smallest value is in order
            if (first == (second - 1))
                return true;

        }

        return false;
    }

}
