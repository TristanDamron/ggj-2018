using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTentacles : MonoBehaviour {


    [SerializeField]
    GameObject _mainPortal;


    [SerializeField]
    InFrontOfCamera _front;

    int _spawnCount;
    float _timer;
    float _spawnTime;

	void Update () 
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnTime)
        {
            if (_spawnCount > 1)
            {
                _timer = 0f;
                _spawnTime = 5;
                return;
            }

            var spawnPos = _front.ReturnVectorPointInFront(Random.Range(2, 4));
            Instantiate(_mainPortal, new Vector3(spawnPos.x + Random.Range(-10f, 10f), spawnPos.y, spawnPos.z + Random.Range(-10f, 10f)), transform.rotation);
            _timer = 0f;
            _spawnCount++;
            _spawnTime = Random.Range(10, 30);
        }
	}
}
