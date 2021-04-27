using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  참고링크
//  http://buildnewgames.com/astar/

public class PathNode
{
    /// <summary>
    /// 시작점으로 부터 현재 노드까지의 경로를 이동하는데 소요되는 비용입니다.
    /// </summary>
    public int G;
    
    /// <summary>
    /// 현재 노드에서 목적지까지 예상 이동 비용입니다.
    /// 사이에 벽, 물 등으로 인해 셀제 거리는 알지 못합니다.
    /// 장애물을 무시하고 예상거리를 산출합니다.
    /// </summary>
    public int H;
    
    /// <summary>
    /// 현재까지 이동하는데 걸린 비용과 예상 비용을 합친 총 비용입니다.
    /// </summary>
    public int F => G + H;

    public PathNode(int go, int hoal)
    {
        G = go;
        H = hoal;
    }
}
