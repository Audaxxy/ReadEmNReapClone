using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboUIManager : MonoBehaviour
{

    public ComboSystem cs;
    public Canvas canvas;
    public TMP_Text HandText;
    public TMP_Text ComboText;
    public VerticalLayoutGroup handList;
    public Color OriginalComboTextColor;

    public string CurrentCardList;

    public float CurrentTimerFadeout = 0.0f;
    public float MaxTimeFadeout = 5.0f;
    public bool bFadeOutTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        cs = GetComponent<ComboSystem>();
    
        if(cs != null)
        {
            //Combo Delegates
            cs.pokerComboRetreivalDelegate += DisplayPokerCombo;
            cs.addCardToHandDelegate += DisplayNewCard;
            cs.clearHandDelegate += ClearComboInfo;
        }
        else { 
            Debug.Log("The ComboSystem was null"); 
        }

        if (canvas != null)
        {

        }
        else {
            Debug.Log("The canvas Game Object was null"); 
        }

        
        if (ComboText == null)
        { 
            Debug.Log("The ComboText Game Object was null"); 
        }
        else
        {
            ComboText.text = "";
            OriginalComboTextColor = ComboText.color;
            OriginalComboTextColor.a = 0.0f;
            ComboText.color = OriginalComboTextColor;
        }


        if(HandText == null)
        {
            Debug.Log("The HandText Game Object was null");
        }
        else
        {
            HandText.text = "";
        }
    }

    public void DisplayPokerCombo(PokerCombo combo)
    {
        if (ComboText == null)
        { Debug.Log("The ComboText Game Object was null"); }

        bFadeOutTimer = true;
        CurrentTimerFadeout = 0.0f;

        //Display the poker combo we got
        switch (combo)
        {
            case PokerCombo.RoyalFlush:
                ComboText.text = "Royal Flush!";
                break;
            
            case PokerCombo.FourOfAKind:
                ComboText.text = "Four of a kind!";
                break;
            
            case PokerCombo.ThreeOfAKind:
                ComboText.text = "Three of a kind";
                break;
            
            case PokerCombo.Flush:
                ComboText.text = "Flush!";
                break;

            case PokerCombo.FullHouse:
                ComboText.text = "Full House!";
                break;

            case PokerCombo.OnePair:
                ComboText.text = "Just a pair";
                break;

            case PokerCombo.TwoPairs:
                ComboText.text = "2 Pairs";
                break;

            case PokerCombo.StraightFlush:
                ComboText.text = "Straight Flush";
                break;

            case PokerCombo.NO_COMBO:
                ComboText.text = "Missed combo";
                break;

            default:
                ComboText.text = combo.ToString();
                break;
        }
    }

    public void DisplayNewCard(CardType card)
    {
        switch (card.cardValue)
        {
            case 1:
                CurrentCardList += "Ace";
                break;
            case 11:
                CurrentCardList += "Jack";
                break;
            case 12:
                CurrentCardList += "Queen";
                break;
            case 13:
                CurrentCardList += "King";
                break;
            default:
                CurrentCardList += card.cardValue;
                break;
        }

        CurrentCardList += " of " + card.cardSuit.ToString() + "\n";

        if (HandText != null)
        { 
            HandText.text = CurrentCardList; 
        }

    }

    public void ClearComboInfo()
    {
        //Clear the current combo and other related information here
        CurrentCardList = "";

        if(HandText != null)
        {
            HandText.text = "";
        }
    }


    void Update()
    {
        if (bFadeOutTimer && ComboText != null)
        {
            CurrentTimerFadeout += Time.deltaTime;

            OriginalComboTextColor.a = 1 - (CurrentTimerFadeout / MaxTimeFadeout);
            ComboText.color = OriginalComboTextColor;

            if(CurrentTimerFadeout >= MaxTimeFadeout)
            {
                bFadeOutTimer = false;

                if (ComboText != null)
                { 
                    ComboText.text = ""; 
                }
            }
        }    
    }
}
