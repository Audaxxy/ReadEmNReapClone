using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineManager : MonoBehaviour
{

    private Text _textUserInfoSlotMachine; //A TEXT NEEDS TO BE ATTACHED TO THE SLOT MACHINE (IN CHILDREN)

    private Dictionary<int, List<float>> _numberToRotation = new Dictionary<int, List<float>>();

    private SpinWheel[] _wheels;

    private int _spinningTime = 5;

    private int _reward;

    private bool _spinning;

    private bool _playerNearby;

    private void Awake()
    {
        
        _wheels = GetComponentsInChildren<SpinWheel>();
        
        _textUserInfoSlotMachine = GetComponentInChildren<Text>();
        FillDictionary();
    }

    private void Update()
    {
        if (!_playerNearby)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E)&&_spinning==false)
        {
            StartSpin();
        }
    }

    private void FillDictionary()
    {
        //Cherry
        List<float> cherry = new List<float>();
        cherry.Add(262);
        cherry.Add(22);
        cherry.Add(94);
        _numberToRotation.Add(0, cherry);

        //Watermelon
        List<float> watermelon = new List<float>();
        watermelon.Add(0);
        watermelon.Add(240);
        _numberToRotation.Add(1, watermelon);

        //7
        List<float> seven = new List<float>();
        seven.Add(47);
        seven.Add(190);
        _numberToRotation.Add(2, seven);

        //Diamonds
        List<float> diamond = new List<float>();
        diamond.Add(164);
        diamond.Add(308);
        _numberToRotation.Add(3, diamond);

        //plum
        List<float> plum = new List<float>();
        plum.Add(332);
        plum.Add(139);
        _numberToRotation.Add(4, plum);

        //lemon
        List<float> lemon = new List<float>();
        lemon.Add(114);
        lemon.Add(284);
        _numberToRotation.Add(5, lemon);

        //bell
        List<float> bell = new List<float>();
        bell.Add(72);
        bell.Add(217);
        _numberToRotation.Add(6, bell);
    }


    public void StartSpin()
    {
        _textUserInfoSlotMachine.text = "";

        if (_spinning)
        {
            return;
        }

        _spinning = true;

        int[] outcomes = new int[3];

        for (int i = 0; i < _wheels.Length; i++)
        {
            int outCome = Random.Range(0, 7);
            outcomes[i] = outCome;
            List<float> list = _numberToRotation[outCome];
            float stopRotation =list[Random.Range(0, list.Count)];
            _wheels[i].StartRotate(_spinningTime + i, stopRotation);
        }

        if (outcomes[0] == outcomes[1] && outcomes[0] == outcomes[2])
        {
            _reward = 1;
            //_reward = x depending on number TODO
            //what is the reward? points, ammo, health? maybe could be all, depending on which symbol
        }
        else
        {
            _reward = 0;
        }
        Invoke(nameof(StopSpin), _spinningTime * 1.5f);
    }

    private void StopSpin()
    {
        if (_reward != 0)
        {
            _textUserInfoSlotMachine.text = "WIN: " + _reward;
        }
        else
        {
            _textUserInfoSlotMachine.text = "LOSE";
        }
        _spinning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerNearby = false;
        }
    }


}
