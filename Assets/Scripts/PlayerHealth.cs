using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;
public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private TMP_Text _textPlayerHealth; //maybe change to a slider like thing?

    [SerializeField] private DeathMenu _deathMenu;
    [SerializeField] private ProgressBar _healthBar;

    private int _health = 100;

    private int _healthReductionRate = 1; //randomize that maybe?
	private void Start()
	{
        
        UpdateText();

	}
	private void UpdateText()
    {
        _textPlayerHealth.text = _health.ToString()+ "%";
        _healthBar.currentPercent = _health;
    }
   
    public void ReduceHealth()
    {
        _health -= _healthReductionRate;
        UpdateText();
        //randomized version, random between normal healthReductionRate (5) and normal healthReductionRate * 5 (25)
        //_health -= _healthReductionRate * Random.Range(1, 6);
    
        if (_health <= 0)
        {
            //We have died

            if(_deathMenu != null)
            {
                _deathMenu.OpenDeathMenu();
            }

            PlayerController PC = GetComponent<PlayerController>();

            if(PC != null)
            {
                PC.SetInPauseMenu(true);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    
    }

}
