using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawn : MonoBehaviour
{
    public GameObject TrapPrefab;
    public GameObject SpawnPoints;

    public int InitialCoinSpawn = 1;

    public float SpawnRateMin = 0.5f, SpawnRateMax = 2.5f;

    public float Cooldown, SpawnRate;

    void SpawnCoin(int index = -1)
    {
        //Ambil salah satu lokasi
        var i = index == -1 ? Random.Range(0, SpawnPoints.transform.childCount) : index;
        var lokasi = SpawnPoints.transform.GetChild(i).position;

        //Cek dulu di posisi ini apa sudah ada koin
        var hitColliders = Physics.OverlapSphere(lokasi, 0.1f);

        if (hitColliders.Length > 0) return;

        //Acak spawn rate terbaru
        SpawnRate = Random.Range(SpawnRateMin, SpawnRateMax);

        //Reset CD
        Cooldown = SpawnRate;

        //Munculkan koin di lokasi
        Instantiate(TrapPrefab, lokasi, Quaternion.identity, SpawnPoints.transform);
    }

    void Start()
    {
        //Di awal permainan, munculkan sejumlah koin ini
        for (int i = 0; i < InitialCoinSpawn; i++)
        {
            SpawnCoin(i);
        }
    }

    void Update()
    {
        Cooldown -= Time.deltaTime;
        if (Cooldown <= 0f) SpawnCoin();
    }
}
