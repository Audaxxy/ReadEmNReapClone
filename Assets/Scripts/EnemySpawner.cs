using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject _cardPrefab;

    [SerializeField] private Cards[] _cards;

    [SerializeField] private Transform[] _spawnPositions;

    private List<Cards> _cardsCurrentlyInScene = new List<Cards>();

    private int _timer;
    private float _timeBetweenToSpawns = 500f;
    private float _decreasingRateForSpawnTimer = 0.98f;

    private int _enemiesSpawnedAtStart = 10;

    private int _maxEnemiesInScene = 30;


    private void Start()
    {
        //TODO spawn x enemies
        for (int i = 0; i < _enemiesSpawnedAtStart; i++)
        {
            SelectCard();
        }
    }

    private void Update()
    {
        _timer++;
        if (_timer >= _timeBetweenToSpawns)
        {
            _timer = 0;
            
            if (_cardsCurrentlyInScene.Count >= _maxEnemiesInScene)
            {
                return;
            }
            SelectCard();
            _timeBetweenToSpawns *= _decreasingRateForSpawnTimer;
        }
    }

    private void SelectCard()
    {
        Cards currentCard = _cards[Random.Range(0, _cards.Length)];

        for (int i = 0; i < _cardsCurrentlyInScene.Count; i++)
        {
            if (currentCard == _cardsCurrentlyInScene[i])
            {
                SelectCard();
                return;
            }
        }
        SpawnEnemy(currentCard);
    }

    private void SpawnEnemy(Cards currentCard)
    {
        Debug.Log("spawning");

        _cardsCurrentlyInScene.Add(currentCard);

        GameObject latestCard = Instantiate(_cardPrefab, _spawnPositions[Random.Range(0, _spawnPositions.Length)].position, _spawnPositions[Random.Range(0, _spawnPositions.Length)].rotation);
        latestCard.GetComponent<CardHandler>().card = currentCard;
        latestCard.GetComponent<CardHandler>().setSymbols();
    }

    public void EnemyDead(Cards deadCard) //TODO NEEDS TO BE CALLED BY ENEMY WHEN DEAD
    {
        _cardsCurrentlyInScene.Remove(deadCard);
        
    }


}
