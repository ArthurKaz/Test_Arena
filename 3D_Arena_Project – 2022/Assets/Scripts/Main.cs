using System.Collections.Generic;
using System.Linq;
using Test_Project;
using Test_Project.TestEnemy;
using UnityEngine;
using Test_Project.Abstractions;
using Test_Project.Entities;
using UI;

public class Main : MonoBehaviour, IInstigator
{
    [Header("Input")] 
    [SerializeField] private MovementInput movementInput;
    [SerializeField] private ShootInput shootInput;
    [SerializeField] private UltimateInput ultimateInput;
    
    [Header("Player")] 
    [SerializeField] private Player player;
    [SerializeField] private EntityMovement entityMovement;
    [SerializeField] private Transform cameraDirection;
    [SerializeField] private TargetAble targetAble;
    
    [Header("Spawners")] 
    [SerializeField] private ProjectileSpawner projectileSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    
    [Header("Prefabs")] 
    [SerializeField] private SimpleBullet simpleBulletPrefab;
    [SerializeField] private HomingBullet homingBulletPrefab;
    [SerializeField] private Enemy blueEnemyPrefab;
    [SerializeField] private Enemy redEnemyPrefab;
    
    [Header("World")] 
    [SerializeField] private Edge[] edges;
    [SerializeField] private Transform container;
    [SerializeField] private LayerMask obstacleMasks;
    [SerializeField] private List<Vector3> positionsOnScene;
    [SerializeField] private EndGameHandle endGameHandle;

    [Header("UI")]
    [SerializeField] private UIProgressBar energyProgressBar;
    [SerializeField] private UIProgressBar healthProgressBar;

    private RuntimeController _runtimeController;

    private IUltimate _ultimate;

    private EnemyRandomPosition _enemyRandomPosition;
    private PlayerRandomPosition _playerRandomPosition;
    
    private SimpleObjectPull<SimpleBullet> _simpleBulletPull;
    private SimpleObjectPull<HomingBullet> _homingBulletPull;
    
    private SimpleObjectPull<Enemy> _blueEnemy;
    private SimpleObjectPull<Enemy> _redEnemy;


    private void Start()
    {
        PlayerMain();
        InputMain();
        ProjectileMain();
        EnemyMain();
       // PlayerPrefs.DeleteAll();
    }

    private void PlayerMain()
    {
        _playerRandomPosition = new PlayerRandomPosition(positionsOnScene, enemySpawner);
       
        _ultimate = new KillAllEnemiesUltimate(enemySpawner,player);
        foreach (var edge in edges)
        {
            edge.Init(_playerRandomPosition);
        }

        _runtimeController = new RuntimeController(enemySpawner);
        endGameHandle.Init(player, _ultimate,_runtimeController);
        player.Died += endGameHandle.EndGame;
        energyProgressBar.Init(player.GetEnergyProgress());
        healthProgressBar.Init(player.GetHealthProgress());
        player.Init();
    }

    private void InputMain()
    {
        movementInput.Init(entityMovement);
        shootInput.Init(projectileSpawner, cameraDirection, player, player);
        ultimateInput.Init(_ultimate);
    }
    private void ProjectileMain()
    {
        _simpleBulletPull = new SimpleObjectPull<SimpleBullet>(this,container,simpleBulletPrefab);
        _homingBulletPull = new SimpleObjectPull<HomingBullet>(this, container, homingBulletPrefab);
        projectileSpawner.Init(_simpleBulletPull,_homingBulletPull, targetAble);
    }

    private void EnemyMain()
    {
        _enemyRandomPosition = new EnemyRandomPosition(positionsOnScene, obstacleMasks);
        _blueEnemy = new SimpleObjectPull<Enemy>(this, container, blueEnemyPrefab);
        _redEnemy = new SimpleObjectPull<Enemy>(this, container, redEnemyPrefab);
        enemySpawner.Init(_blueEnemy,_redEnemy);
        enemySpawner.Init( projectileSpawner,_enemyRandomPosition,targetAble);
        enemySpawner.StarSpawnEnemies();
    }

    private void PositionGenerate()
    {
        float minZ = -2;
        float maxZ = 2;
        float minX = -2;
        float maxX = 2;
        float step = 0.5f;
        for (float z = minZ; z < maxZ; z += step)
        {
            for (float x = minX; x < maxX; x += step)
            {
                var pos = new Vector3(x, 0, z);
                if (FreePoint(pos))
                {
                    positionsOnScene.Add(pos);
                }
            }
        }
    }
    private bool FreePoint(Vector3 position)
    {
        var needSpaceInRadius = 0.5f;
        var colliders = Physics.OverlapSphere(position,needSpaceInRadius , obstacleMasks);
        return colliders.Length == 0;
    }

    public new T Instantiate<T>(T prefab) where T : MonoBehaviour
    {
        return Instantiate(prefab, container, true);
    }
}