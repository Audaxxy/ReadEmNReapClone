using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    private float _rotationTimer;
    private int _rotationLength;
    private float _stopRotation;

    private float _rotateSpeed = 5;

    private int _state;
    // 0 nothing
    // 1 rotating
    // 2 slowing down
    // 3 can stop

    private void Update()
    {
        if (_state == 0)
        {
            return;
        }
        ManageRotation();
    }

    private void ManageRotation()
    {
        _rotationTimer += Time.deltaTime;

        switch (_state)
        {
            case 0:
                return;
            case 1:
                Rotate();
                if (_rotationTimer > _rotationLength - 2)
                {
                    _state = 2;
                }

                break;
            case 2:
                transform.localRotation = Quaternion.Euler(_stopRotation, 0, 0);
                _state = 0;

                break;
        }
    }

    private void Rotate()
    {
        gameObject.transform.Rotate(Vector3.right * _rotateSpeed);
        if (transform.rotation.x >= 360)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void StartRotate(int length, float stopRotation)
    {
        _rotationLength = length;
        _stopRotation = stopRotation;
        _rotationTimer = 0;
        _rotateSpeed = 5;
        _state = 1;
    }



}