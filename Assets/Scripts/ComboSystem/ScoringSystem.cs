using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{

    public TMPro.TMP_Text ScoringText;

    public int fullScore = 0;

    public AudioSource audioSource;
    public AudioClip gainScore,missed,killCard;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ComboManager = GameObject.FindGameObjectWithTag("comboManager");

        if (ComboManager != null)
        {
            ComboSystem CS = ComboManager.GetComponent<ComboSystem>();

            if (CS == null)
            {
                Debug.LogError("Combo system was null and cannot affect scoring system");
            }
            else
            {
                CS.pokerComboRetreivalDelegate += UpdateScore;
                CS.addCardToHandDelegate += UpdateScore;
            }
        }

        if (ScoringText != null)
        {
            ScoringText.text = "Score: " + fullScore.ToString();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }


    void UpdateScore(PokerCombo combo)
    {
        UpdateScore((int)combo);
    }

    void UpdateScore(CardType card)
    {
        //Update the score by a set amount per card
        UpdateScore(10);
    }

    void UpdateScore(int inScore)
    {
        fullScore += inScore;

        if (ScoringText != null)
        {
            ScoringText.text = "Score: " + fullScore.ToString();
        }
        if (inScore == 0)
        {
            audioSource.PlayOneShot(missed);
        }
        else if (inScore == 10)
        {
            audioSource.PlayOneShot(killCard);
        }
        else
        {
            audioSource.PlayOneShot(gainScore);
        }

    }

}
