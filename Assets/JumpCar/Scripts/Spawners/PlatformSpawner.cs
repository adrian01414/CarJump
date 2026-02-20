using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public LevelConfig LevelConfig;
    public Platform PlatformPrefab;

    private LinkedList<Platform> PrespawnedPlatforms = new();

    private int _platformCounter = 0;
    private float _platformDistance;

    private void Awake()
    {
        _platformDistance = LevelConfig.PlatformDistance;

        for (int i = 0; i < 10; i++)
        {
            float positionY = i * _platformDistance + transform.position.y;

            var platform = Instantiate(PlatformPrefab, 
                new Vector3(transform.position.x, positionY, transform.position.z),
                Quaternion.identity,
                transform);

            PrespawnedPlatforms.AddLast(platform);
        }
    }

    private void OnEnable()
    {
        Platform.OnPlayerLanded += RespawnPlatform;
    }

    private void RespawnPlatform()
    {
        if(_platformCounter > 3)
        {
            var platform = PrespawnedPlatforms.First.Value;
            platform.Reset();
            platform.transform.position = new Vector3(platform.transform.position.x, 
                PrespawnedPlatforms.Last.Value.transform.position.y + _platformDistance,
                platform.transform.position.z);

            PrespawnedPlatforms.RemoveFirst();
            PrespawnedPlatforms.AddLast(platform);
        }

        _platformCounter++;
    }

    private void OnDisable()
    {
        Platform.OnPlayerLanded -= RespawnPlatform;
    }
}
