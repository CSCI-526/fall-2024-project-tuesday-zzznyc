using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckpointManager checkpointManager;

    void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>(); // 查找 CheckpointManager
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && checkpointManager != null)
        {
            checkpointManager.SetCheckpoint(transform.position); // 更新检查点
        }
    }
}
