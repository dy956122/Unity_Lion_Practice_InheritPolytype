using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 會竄逃的人類
/// </summary>
public class PeopleRun : People
{
    [Header("半徑"), Range(1, 30)]
    public float radius = 5;
    
    /// <summary>
    /// 最終座標
    /// </summary>
    private Vector3 final;

    private void Update()
    {
        Flee();
    }

    /// <summary>
    ///  竄逃
    /// </summary>
    private void Flee()
    {
        if (agent.remainingDistance < 1.5f)
        {

            // 隨機座標 = 隨機.球內隨機點 * 半徑 + 中心點，換句話說就是從中心點發出 半徑 5 單位 的圓 作為 偵測範圍
            Vector3 pointRan = Random.insideUnitSphere * 5 + transform.position;

            // 導覽網格碰撞 碰撞點 ，在此段的意思為 創建 一個給導覽器 碰撞偵測用的點
            NavMeshHit hit;

            // 導覽網格.樣本座標(座標,碰撞點,半徑,圖層)
            // out 執行方法會將結果直接儲存到傳入的參數內
            // 執行後會將取得的隨機點儲存在 hit 參數內
            NavMesh.SamplePosition(pointRan, out hit, 5, 1);

            // 最終座標 = 碰撞點.座標
            final = hit.position;

            // 代理器：設定目的地(座標 - 三維向量)，也就是說 設定一目標，讓導覽器移動過去
            agent.SetDestination(final);
        }


    }
}
