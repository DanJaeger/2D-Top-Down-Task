using HUD;
using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header(header: "Components: ")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    public HungerCounterHandler Hunger;
    public OutfitChanger ClothingChanger;
    [SerializeField] private ShopSystem _shopSystem;
    [SerializeField] private InventoryController _inventoryController;

    [Header(header: "Movement: ")]
    [SerializeField] private float _moveSpeed = 5.0f;
    private Vector2 _playerInput;

    public bool CanInteract;
    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
            if(_rb == null)
            {
                Debug.LogError("Could NOT access Rigidbody2D Component on " + this.name);
            }
        }
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Could NOT access Animator Component on " + this.name);
            }
        }
        if (_inventoryController == null)
        {
            _inventoryController = GetComponent<InventoryController>();
            if (_inventoryController == null)
            {
                Debug.LogError("Could NOT access InventoryController Component on " + this.name);
            }
        }
    }
    private void Start()
    {
        CanInteract = false;
    }

    void Update()
    {
        GetPlayerInput();
        if (Input.GetKeyDown(KeyCode.E) && CanInteract && !ShopSystem.IsPlayingIntro)
        {
            _shopSystem.OpenShop();
        }else if (Input.GetKeyDown(KeyCode.E) && !CanInteract && !ShopSystem.IsPlayingIntro && !ShopSystem.IsShopOpen)
        {
            _inventoryController.DisplayInventory();
        }
    }
    void GetPlayerInput()
    {
        _playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        UpdateAnimation();
        UpdateSpriteRotation();
    }
    void UpdateAnimation()
    {
        if (_playerInput != Vector2.zero)
        {
            _animator.Play("Run");
        }
        else
        {
            _animator.Play("Idle");
        }
    }
    void UpdateSpriteRotation()
    {
        if (_playerInput.x < 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else if (_playerInput.x > 0)
            transform.rotation = Quaternion.Euler(0f, 0.0f, 0f);
    }
    private void FixedUpdate()
    {
        MoveCharacter();
    }
    void MoveCharacter()
    {
        Vector2 forceToApply = _playerInput * _moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + forceToApply);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShopSystem shop = collision.GetComponent<ShopSystem>();
        if (shop != null)
        {
            CanInteract = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ShopSystem shop = collision.GetComponent<ShopSystem>();
        if (shop != null)
        {
            CanInteract = false;
        }
    }
    public void PlayStepAudio()
    {
        SoundFXManager.instance.PlayStepSound();
    }

}
