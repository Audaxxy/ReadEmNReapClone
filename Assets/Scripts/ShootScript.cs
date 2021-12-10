using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShootScript : MonoBehaviour
{

    [SerializeField] private ParticleSystem _shootParticles;
    [SerializeField] private GameObject _hitParticles;

    [SerializeField] private TMP_Text _textAmmo;

    public Animator myAnimator;
    public AudioSource audioSource;
    public AudioClip gunEmpty, gunFire, gunReload,symbolHit;
    
    private int _ammo = 20;
    private int _maxAmmo=20;

    private float _reloadingTime = 1f; //in seconds

    private bool _reloading;

	private void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
        //_textAmmo.text = _ammo.ToString();
	}
	private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_reloading)
            {
                //TODO User Feedback reloading?
                return;
            }
            if (_ammo <= 0)
            {
                audioSource.PlayOneShot(gunEmpty);
                //TODO User Feedback No Ammo
                return;
            }

            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_ammo == _maxAmmo)
            {
                //TODO User Feedback Full Ammo??
                return; //? or let the player reload anyways?
            }
            Reload();
        }
    }

    private void Shoot()
    {
        _ammo--; 
        //_textAmmo.text = _ammo.ToString(); 
        _shootParticles.Play();
        audioSource.PlayOneShot(gunFire);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,
            Mathf.Infinity))
        {
            GameObject hitParticle = Instantiate(_hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitParticle, 1);

            Debug.Log(hit.transform.gameObject.name);

            if (hit.transform.GetComponent<Symbol>())
            {
                hit.transform.GetComponent<Symbol>().gotShot();
                audioSource.PlayOneShot(symbolHit);
            }
        }

    }

    private void Reload()
    {
        _reloading = true;
        myAnimator.SetTrigger("Reload");
        audioSource.PlayOneShot(gunReload);
      
       Invoke(nameof(ReloadingFinished), _reloadingTime);
    }

    private void ReloadingFinished()
    {
        
        _reloading = false;
        _ammo = _maxAmmo;
        _textAmmo.text = _ammo.ToString();
    }
}
