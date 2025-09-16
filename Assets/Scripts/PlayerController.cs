using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Karakterin hareket hızı
    public float moveSpeed = 5f;

    // Inspector'da atamanız gereken ok prefabları
    public GameObject defaultArrowPrefab;
    public GameObject specialArrowPrefab;

    // O an aktif olan ok prefabı
    private GameObject currentArrowPrefab;

    // Atış hızı kontrolü
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    void Start()
    {
        currentArrowPrefab = defaultArrowPrefab;
    }

    void Update()
    {
    
        HandleMovement();

        // Atış kodu
        HandleShooting();

           
    if (Input.GetKeyDown(KeyCode.E))
    {
        Debug.Log("E tuşuna basıldı!");
        SwitchArrowType();
    }

    }

    void HandleMovement()
    {
        // Yatay ve dikey eksenlerdeki girdi değerlerini al
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Hareket yönünü belirle
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Karakteri hareket ettir
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void HandleShooting()
    {
        // Fare sol tuşuna basıldığında ok at
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            ShootArrow();
        }
    }

    void SwitchArrowType()
    {
        // Eğer şu an varsayılan ok ise, özel oka geç
        if (currentArrowPrefab == defaultArrowPrefab)
        {
            currentArrowPrefab = specialArrowPrefab;
            Debug.Log("Ok tipi değişti: Özel Ok!");
        }
        // Eğer şu an özel ok ise, varsayılan oka geri dön
        else
        {
            currentArrowPrefab = defaultArrowPrefab;
            Debug.Log("Ok tipi değişti: Varsayılan Ok!");
        }
    }

    void ShootArrow()
    {
        // Tek bir ok oluştur
        Instantiate(currentArrowPrefab, transform.position, transform.rotation);
        Debug.Log("Ok Atış Komutu Verildi!");
    }
}