using UnityEngine;
using MyTypes;
using UnityEngine.Events;
using Assets.Scripts.Accessory;


namespace Assets.Scripts.GeneralGame.Entities.Player
{
    /// <summary>
    /// Класс для создания объекта игрока и связывания его систем
    /// </summary>
    internal class Player
    {
        private bool isLife = true;
        
        public bool IsLife
        {
            set
            {
                isLife = value;
                if (!isLife)
                {
                    playerAnimationController.Animator.SetBool("IsDestroy",true);    
                }
            }
            get => isLife;
        }



        UnityEvent onDeath = new UnityEvent();
        

        UnityEvent<float> onHPChange = new UnityEvent<float>();
        UnityEvent<float> onShieldChange = new UnityEvent<float>();
        public Vector2 Position
        {
            get => playerEntity.Position;
            set => playerEntity.Position = value;
        }
        public UnityEvent OnDeath { get => onDeath; }
        public UnityEvent<GameObject> OnChangeWeapon { get => onChangeWeapon;  }
        public UnityEvent<float> OnHPChange { get => onHPChange; }
        public UnityEvent<float> OnShieldChange { get => onShieldChange; }

        // Системы персонажа
        GameObject playerGameObject;
        PlayerEntity playerEntity;
        PlayerInput playerInput;        
        PlayerAnimationController playerAnimationController;
        PlayerDamageable playerDamageable;
        PlayerGun gun;


        public void OnStartNewLevel(int levelID)
        {
            gun.UpdateBullets(levelID);
        }

        UnityEvent<GameObject> onChangeWeapon = new();
        public void ChangeWeapon()
        {
            OnChangeWeapon.Invoke(gun.ChangeWeapon());
        }
        public Player(PlayerConfig config)
        {
            ///
            //Creating player game object
            playerGameObject = new GameObject("Player");
            playerGameObject.transform.localScale *= 1.5f;
            playerAnimationController = playerGameObject.AddComponent<PlayerAnimationController>();
            playerDamageable = playerGameObject.AddComponent<PlayerDamageable>();
            playerDamageable.Hp = new PointStruct(config.Hp);
            playerDamageable.Shield = new PointStruct(config.Shield);
            ///

            //player animation
            playerAnimationController.SetConfig(config);
            playerAnimationController.OnCompleteDeathAnimation.AddListener(() => { OnDeath.Invoke(); });


            // player physics
            playerEntity = playerGameObject.AddComponent<PlayerEntity>();
            //playerEntity.OnCollide.AddListener(() => 
            //{ 
            //    if(shield.isEmpty())
            //        hp.Reduce(1);
            //    else
            //        shield.Reduce(3);
            //});
            playerEntity.Speed = new PointStruct(config.Speed);


            //Привязка системы ввода к другим системам
            playerInput = new PlayerInput();

            // Gunning
            gun = new PlayerGun(playerGameObject, config.Bullets, 0.05f);
            playerInput.OnAttack.AddListener(gun.StartAttack);
            playerInput.InAttack.AddListener(gun.ProcessingAttack);
            playerInput.AfterAttack.AddListener(gun.StopAttack);
            playerInput.OnChangeWeapon.AddListener(ChangeWeapon);
            //

            // Move methods
            playerInput.OnStartMove.AddListener(()=> {
                if (playerInput.Direction == Vector2.zero)
                {
                    playerEntity.Speed.Reset();
                    //playerEntity.Speed.Increase(playerEntity.Speed.MaxPoint / 10);
                }
            });
            playerInput.InMove.AddListener(() =>
            {
                playerEntity.Dir = playerInput.Direction;
                playerEntity.Speed.Increase(playerEntity.Speed.MaxPoint / 20);
            });
            //

            playerDamageable.Hp.OnEmpty.AddListener(() => IsLife = false);
            playerDamageable.Hp.OnValueChange.AddListener((float current, float delta) => { OnHPChange.Invoke(playerDamageable.Hp.GetRatio()); });
            playerDamageable.Shield.OnValueChange.AddListener((float current, float delta) => { OnShieldChange.Invoke(playerDamageable.Shield.GetRatio()); });
        }

        /// <summary>
        /// Действия который происходят каждый кадр
        /// </summary>
        public void Update()
        {
            playerInput.Update();
            playerDamageable.NewUpdate();
        }
        public void Destroy()
        {
            Destroyer.Instance.Destroy(playerGameObject);
            playerInput.Dispose();
        }
    }
}
