// ==============================
// file:Character(.cs)
// brief:ゲーム内キャラクター
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public float moveSpeed = 1F;
    public float jumpPower = 1F;


    public bool isGround { get; private set; }
    public bool downGravity { get; private set; }
    [HideInInspector]
    public Transform spawnPoint { private get; set; }

    [SerializeField]
    protected Material colorMat;
    protected Vector3 gravity;

    private Rigidbody rb;
    private Vector3 velocity;

    /// <summary>
    /// 初期化
    /// </summary>
    protected virtual void Start() {
        // 重力フラグの初期化
        isGround = false;
        downGravity = true;

        // 物理演算コンポーネントの取得
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        // 初期の位置と速度を設定
        spawnPoint = (transform.parent) ? transform.parent : transform;
        velocity = Vector3.zero;
    }

    /// <summary>
    /// 更新
    /// </summary>
    protected virtual void Update() {
        if (!rb.useGravity)
        {
            // 重力方向の線をデバック描画
            Debug.DrawLine(transform.position, transform.position + gravity.normalized);

            // 移動量と重力方向の初期化
            velocity = Vector3.zero;
            gravity = downGravity ? Vector3.down : Vector3.up;
            // 重力方向に応じた角度を描画に設定
            var angle = downGravity ? 0F : 180F;
            transform.eulerAngles = Vector3.left * angle;

            // 接地していなければ物理演算により落下
            if (!isGround)
            {
                rb.AddForce(gravity.normalized * Physics.gravity.magnitude);
            }
        }
    }

    /// <summary>
    /// 固定フレームでの更新
    /// </summary>
    private void FixedUpdate() {
        // 物理演算の機能で移動処理を行う
        rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    /// <summary>
    /// 移動方向と速度の設定
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="speed"></param>
    public void Move(Vector3 velocity, float speed) {
        this.velocity = velocity * speed;
    }

    /// <summary>
    /// 物理演算による跳躍
    /// </summary>
    /// <param name="power"></param>
    public void Jump(float power) {
        if (isGround)
        {
            // 接地中のみ自身の上方向に力を加える
            var jumpVec = -gravity.normalized;
            isGround = false;
            rb.AddForce(jumpVec * jumpPower * 50F);
        }
    }

    /// <summary>
    /// 重力方向の切り替え
    /// </summary>
    public void ChangeGravity() {
        if (isGround)
        {
            // 接地中のみ重力フラグを変更
            isGround = false;
            downGravity = !downGravity;
        }
    }

    /// <summary>
    /// 足場を逆方向に反転
    /// </summary>
    public void BlinkFloor() {
        if(isGround)
        {
            // 重力方向にある床を起点に回転を行う
            var rotaPoint = transform.position + gravity.normalized;
            transform.RotateAround(rotaPoint, Vector3.left, 180F);
            // 回転後に落下しないよう重力を反転
            ChangeGravity();
        }
    }

    /// <summary>
    /// 設定された初期化
    /// </summary>
    public virtual void Restart() {
        // 実行初期に設定されたパラメータの上で初期化処理を行う
        var trans = (transform.parent) ? transform.parent : transform;
        trans.position = spawnPoint.position;
        trans.rotation = spawnPoint.rotation;
        trans.localScale = spawnPoint.localScale;
        this.Start();
    }

    /// <summary>
    /// 衝突された瞬間の処理
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            if (Physics.Linecast(transform.position, transform.position + gravity.normalized * 9.8F))
            {
                isGround = true;
                rb.velocity = Vector3.zero;
            }
            else
            {
                isGround = false;
            }
        }
    }

    /// <summary>
    /// 衝突されてる間の処理
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Map")
        {
            if (Physics.Linecast(transform.position, transform.position + gravity.normalized * 9.8F))
            {
                if ((transform.position.y > collision.transform.position.y && gravity == Vector3.down)
                  || transform.position.y < collision.transform.position.y && gravity == Vector3.up)
                {
                    isGround = true;
                }
            }
            else
            {
                isGround = false;
            }
        }
    }

    /// <summary>
    /// 衝突から外れた時の処理
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionExit(Collision collision) {
        isGround = false;
    }
}