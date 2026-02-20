using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public LevelConfig LevelConfig;
    public List<GameObject> EnemyPrefabs;

    private List<GameObject> _spawnedEnemies = new();
    private List<GameObject> _unspawnedEnemies = new();

    private float _platformDistance;
    private float _leftBound;
    private float _rightBound;

    private float _carSizeY;

    private int _enemyCounter = 0;

    private void Awake()
    {
        var carMovement = EnemyPrefabs[0].GetComponent<CarMovement>();
        if(carMovement) _carSizeY = carMovement.GraySpriteRenderer.size.y;

        CameraBounds.CalculateBounds(Camera.main, out _leftBound, out _rightBound, out _, out _);

        _platformDistance = LevelConfig.PlatformDistance;

        for (int i = 0; i < 4; i++)
        {
            foreach (var enemyPrefab in EnemyPrefabs)
            {
                var enemyCar = Instantiate(enemyPrefab, transform);
                enemyCar.SetActive(false);

                _unspawnedEnemies.Add(enemyCar);
            }
        }

        ShuffleList(_unspawnedEnemies);

        for(int i = 0; i < 8; i++)
        {
            var enemy = _unspawnedEnemies.Last<GameObject>();

            _spawnedEnemies.Add(enemy);
            _unspawnedEnemies.Remove(enemy);

            enemy.transform.position = new Vector3(Random.Range(_leftBound, _rightBound), _platformDistance * i + 1f - _carSizeY / 2f, 0f);
            enemy.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Platform.OnPlayerLanded += SpawnEnemy;
    }

    private void SpawnEnemy()
    {
        if(_enemyCounter > 4)
        {
            // spawn new enemy
            var enemy = _unspawnedEnemies.Last<GameObject>();
            enemy.transform.position = new Vector3(Random.Range(_leftBound, _rightBound), _spawnedEnemies.Last<GameObject>().transform.position.y + _platformDistance, 0f);
            _spawnedEnemies.Add(enemy);
            _unspawnedEnemies.Remove(enemy);
            enemy.SetActive(true);

            // return last enemy in pool
            var lastEnemy = _spawnedEnemies.First<GameObject>();
            _unspawnedEnemies.Add(lastEnemy);
            _spawnedEnemies.Remove(lastEnemy);
            lastEnemy.SetActive(false);

            if(_enemyCounter % 8 == 0)
            {
                ShuffleList(_unspawnedEnemies);
            }
        }

        _enemyCounter++;
    }

    private void ShuffleList(List<GameObject> objects)
    {
        var rnd = new System.Random();
        for (int i = objects.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            (objects[i], objects[j]) = (objects[j], objects[i]);
        }
    }

    private void OnDisable()
    {
        Platform.OnPlayerLanded -= SpawnEnemy;
    }
}
