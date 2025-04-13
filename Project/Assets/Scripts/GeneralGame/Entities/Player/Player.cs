
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
        PointStruct hp = new PointStruct(10);

        
        public Vector2 Position
        {
            get => playerEntity.Position;
            set => playerEntity.Position = value;
        }
        public UnityEvent OnDeath { get => onDeath; set => onDeath = value; }

        // Системы персонажа
        GameObject playerGameObject;
        PlayerEntity playerEntity;
        PlayerInput playerInput;        
        PlayerAnimationController playerAnimationController;
        Gun gun;

        public Player(PlayerConfig config)
        {
            ///
            playerGameObject = new GameObject("Player");
            playerGameObject.transform.localScale *= 1.5f;
            playerAnimationController = playerGameObject.AddComponent<PlayerAnimationController>();
            ///

            playerAnimationController.SetConfig(config);
            playerAnimationController.OnCompleteDeathAnimation.AddListener(() => { OnDeath.Invoke(); });


            playerEntity = playerGameObject.AddComponent<PlayerEntity>();
            playerEntity.OnCollide.AddListener(() => { hp.Reduce(1); });
            playerEntity.Speed = new PointStruct(config.Speed);


            ///
            playerInput = new PlayerInput();

            //
            gun = new Gun(playerGameObject, config.Bullet, 0.05f);
            playerInput.OnAttack.AddListener(gun.StartAttack);
            playerInput.InAttack.AddListener(gun.ProcessingAttack);
            playerInput.AfterAttack.AddListener(gun.StopAttack);
            //

            playerInput.OnStartMove.AddListener(()=> {
                if(playerInput.Direction == Vector2.zero)
                    playerEntity.Speed.Reset();
            });
            playerInput.InMove.AddListener(() =>
            {
                playerEntity.Dir = playerInput.Direction;
                playerEntity.Speed.Increase(playerEntity.Speed.MaxPoint / 30);
            });
            ///

            hp.OnEmpty.AddListener(() => IsLife = false);            
        }
        
        /// <summary>
        /// Действия который происходят каждый кадр
        /// </summary>
        public void Update()
        {
            gun.Update();
            playerInput.Update();            
        }
        public void Destroy()
        {
            Destroyer.Instance.Destroy(playerGameObject);
        }
    }
}
