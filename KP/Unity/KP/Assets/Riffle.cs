using UnityEngine;
using UnityEngine.AI;

public class Riffle : MonoBehaviour
{
    [Header("Spray Parameters")]
    [SerializeField] private AnimationCurve _sprayCurve;
    [SerializeField] private AnimationCurve _sprayCurveControl;
    [SerializeField] private float _recoilXMultipier;
    [SerializeField] private float _recoilYMultipier;
    [SerializeField] private float _recoilBackMultiplier;
    [SerializeField] private float _maxRecoilTtimeDelayer = 3f;

    private float _recoilCurvePos = 0f;

    [Header("Weapon Parameters")]
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private GameObject[] _shootSounds;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _shootsInSecond = 5f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 50f;
    private float _nextTimeToShoot;

    [Header("References")]
    private Vector3 _initialLocalPosition;
    private Quaternion _initialLocalRotation;

    [Header("Aiming Parameters")]
    [SerializeField] private Vector3 _aimPositionOffset;
    [SerializeField] private Vector3 _aimRotationOffset;
    [SerializeField] private float _aimSpeed = 8f;

    [Header("Camera Recoil Parameters")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraRecoilAmount = 2f;
    [SerializeField] private float _cameraRecoilReturnSpeed = 5f;

    private bool _isAiming = false;
    private Vector3 _cameraInitialPosition;
    private Quaternion _cameraInitialRotation;

    private void Start()
    {
        // Store the initial local position and rotation
        _initialLocalPosition = transform.localPosition;
        _initialLocalRotation = transform.localRotation;

        // Сохраняем начальные позиции и вращения камеры
        if (_camera != null)
        {
            _cameraInitialPosition = _camera.transform.localPosition;
            _cameraInitialRotation = _camera.transform.localRotation;
        }
    }

    private void Update()
    {
        HandleAiming();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time > _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.time + 1 / _shootsInSecond;
                Shoot();
            }
            _recoilCurvePos += Time.deltaTime / _maxRecoilTtimeDelayer;
        }
        else
        {
            _recoilCurvePos = 0f;
        }

        // Smoothly return the weapon to its original position and rotation after recoil
        if (!_isAiming)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _initialLocalPosition, Time.deltaTime * _recoilBackMultiplier);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _initialLocalRotation, Time.deltaTime * _recoilBackMultiplier);
        }
        if (_camera != null)
        {
            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, _cameraInitialPosition, Time.deltaTime * _cameraRecoilReturnSpeed);
            _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, _cameraInitialRotation, Time.deltaTime * _cameraRecoilReturnSpeed);
        }
    }

    private void Shoot()
    {
        AddRecoil();

        Bullet bulletC = Instantiate(bullet, _shootPosition.position, _shootPosition.rotation).GetComponent<Bullet>();
        bulletC.StartCoroutine(bulletC.shoot(_shootPosition.position, _shootPosition.forward * 500f, bulletSpeed));

        GameObject _sound = Instantiate(_shootSounds[Random.Range(0, _shootSounds.Length)], _shootPosition.position, _shootPosition.rotation);
        Destroy(_sound, 2.3f);

        _animator.SetTrigger("Shoot");
    }

    private void AddRecoil()
    {
        transform.Rotate(new Vector3(
            (-_recoilCurvePos * (_recoilYMultipier * _sprayCurveControl.Evaluate(_recoilCurvePos))) + Random.Range(-0.1f, 0.1f),
            (_sprayCurve.Evaluate(_recoilCurvePos) * (_recoilXMultipier * _sprayCurveControl.Evaluate(_recoilCurvePos))) + Random.Range(-0.1f, 0.1f),
            0f
        ), Space.World);

        transform.Translate(transform.forward * 0.03f, Space.World);

        // Применяем отдачу к камере
        if (_camera != null)
        {
            Vector3 cameraRecoil = new Vector3(
                Random.Range(-0.05f, 0.05f), // Уменьшено значение амплитуды случайной тряски по X
                -_recoilCurvePos * _cameraRecoilAmount,
                0f
            );

            _camera.transform.localPosition += cameraRecoil;
            _camera.transform.localRotation *= Quaternion.Euler(cameraRecoil);
        }
    }

    private void HandleAiming()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _isAiming = !_isAiming;
        }

        if (_isAiming)
        {
            Vector3 targetPosition = _initialLocalPosition + _aimPositionOffset;
            Quaternion targetRotation = _initialLocalRotation * Quaternion.Euler(_aimRotationOffset); // Корректное вычисление целевого вращения
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * _aimSpeed);
            // Плавное изменение вращения оружия при прицеливании
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _aimSpeed);
        }

        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _initialLocalPosition, Time.deltaTime * _aimSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _initialLocalRotation, Time.deltaTime * _aimSpeed);
        }
    }
}
