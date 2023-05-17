using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject bulletHoleGraphic;
    //public GameObject muzzleFlash;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    private PlayerInputMap _playerInput;

    private void Start()
    {

        _playerInput = new PlayerInputMap();
        _playerInput.Juego.Enable();
    }

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        /*SetText
        text.SetText(bulletsLeft + " / " + magazineSize);*/
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = _playerInput.Juego.Shoot.IsPressed();
        else shooting = _playerInput.Juego.Shoot.WasPressedThisFrame();

        if (_playerInput.Juego.Reload.WasPressedThisFrame() && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = bulletsPerTap;
            Debug.Log("Hello");
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            Debug.Log("Hello world");

            GameObject.Find(rayHit.collider.name).GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            
            /*if (rayHit.collider.CompareTag("Enemy"))
                 rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage); */
        }

        
        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}