using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;

public class VR_ShootScript : MonoBehaviour
{

    [SerializeField] private ParticleSystem _shootParticles;
    [SerializeField] private GameObject _hitParticles;

    [SerializeField] private Text _textAmmo;

    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;

    public AudioSource audioSource;

    private int _ammo = 20;
    private int _maxAmmo;

    private float _reloadingTime = 2; //in seconds

    private bool _reloading;

    private int _shootDelay = 5;
    private int _shootTimer;

    private bool _weaponIsInLeftHand;
    private bool _weaponIsInRightHand;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        _shootTimer++;

        if ((Input.GetAxis("XRI_Left_Trigger") != 0 && _weaponIsInLeftHand) || (Input.GetAxis("XRI_Right_Trigger") != 0 && _weaponIsInRightHand))
        {
            if (_shootTimer < _shootDelay)
            {
                return;
            }
            if (_reloading)
            {
                //TODO User Feedback reloading?
                return;
            }
            if (_ammo <= 0)
            {
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
        //_ammo--; TODO UNCOMMENT WHEN READY
        //_textAmmo.text = _ammo.ToString(); TODO UNCOMMENT WHEN READY
        _shootTimer = 0;
        _shootParticles.Play();
        audioSource.PlayOneShot(audioSource.clip);

        RaycastHit hit;
        if (_weaponIsInLeftHand)
        {
            if (Physics.Raycast(_leftHand.transform.position, _leftHand.transform.forward, out hit,
            Mathf.Infinity))
            {
                GameObject hitParticle = Instantiate(_hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitParticle, 1);

                if (hit.transform.GetComponent<Symbol>())
                {
                    hit.transform.GetComponent<Symbol>().gotShot();
                }
            }
        }
        else
        {
            if (Physics.Raycast(_rightHand.transform.position, _rightHand.transform.forward, out hit,
                Mathf.Infinity))
            {
                GameObject hitParticle = Instantiate(_hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitParticle, 1);

                if (hit.transform.GetComponent<Symbol>())
                {
                    hit.transform.GetComponent<Symbol>().gotShot();
                }
            }
        }
    }

    private void Reload()
    {
        _reloading = true;
        Invoke(nameof(ReloadingFinished), _reloadingTime);
    }

    private void ReloadingFinished()
    {
        _reloading = false;
        _ammo = _maxAmmo;
        //_textAmmo.text = _ammo.ToString(); TODO UNCOMMENT WHEN READY
    }

    public void GrabLeftHand()
    {
        _weaponIsInLeftHand = true;
    }

    public void ReleaseLeftHand()
    {
        _weaponIsInLeftHand = false;
    }

    public void GrabRightHand()
    {
        _weaponIsInRightHand = true;
    }

    public void ReleaseRightHand()
    {
        _weaponIsInRightHand = false;
    }


}
