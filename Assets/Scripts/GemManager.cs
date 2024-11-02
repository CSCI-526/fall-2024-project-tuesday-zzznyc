using System.Collections;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    [SerializeField] GameObject gemPrefab; // 用于生成宝石的预制体
    [SerializeField] Vector3 spawnPosition; // 宝石生成的位置

    private GameObject currentGem; // 当前存在的宝石对象

    void Start()
    {
        // 在游戏开始时生成宝石
        SpawnGem();
    }

    // 生成宝石的方法
    public void SpawnGem()
    {
        // 生成新的宝石对象
        currentGem = Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
    }

    // 销毁宝石并启动重生计时器
    public void DestroyAndRespawnGem()
    {
        if (currentGem != null)
        {
            Destroy(currentGem); // 销毁当前宝石
        }

        StartCoroutine(RespawnGem(2f)); // 2秒后重新生成
    }

    private IEnumerator RespawnGem(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnGem(); // 重新生成宝石
    }
}