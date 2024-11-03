using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 initialGravity;           // 初始重力方向
    private Vector2 chosenGravity;            // 玩家选择的重力方向
    private bool hasGravityPowerUp = false;   // 是否获得了修改重力的道具
    private bool gravitySelected = false;     // 玩家是否已选择新重力方向

    void Start()
    {
        // 保存初始的重力方向
        initialGravity = Physics2D.gravity;
        chosenGravity = initialGravity;
    }

    void Update()
    {
        // 玩家拾取道具后，可以通过箭头键选择新的重力方向
        if (hasGravityPowerUp && !gravitySelected)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                chosenGravity = new Vector2(0, 9.8f); // 向上
                gravitySelected = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                chosenGravity = new Vector2(0, -9.8f); // 向下
                gravitySelected = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                chosenGravity = new Vector2(-9.8f, 0); // 向左
                gravitySelected = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                chosenGravity = new Vector2(9.8f, 0); // 向右
                gravitySelected = true;
            }
        }

        // 当玩家选择了重力方向后，可以通过按 "G" 键在初始和新方向之间切换
        if (gravitySelected && Input.GetKeyDown(KeyCode.G))
        {
            if (Physics2D.gravity == initialGravity)
            {
                Physics2D.gravity = chosenGravity; // 切换到选择的方向
            }
            else
            {
                Physics2D.gravity = initialGravity; // 切换回初始重力
                hasGravityPowerUp = false;         // 使用完道具，取消道具效果
                gravitySelected = false;           // 重置选择状态
            }
        }
    }

    // 玩家拾取道具
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GravityPowerUp"))
        {
            hasGravityPowerUp = true;         // 获得道具
            gravitySelected = false;          // 重置选择状态
            Destroy(other.gameObject);        // 销毁道具对象
        }
    }
}
