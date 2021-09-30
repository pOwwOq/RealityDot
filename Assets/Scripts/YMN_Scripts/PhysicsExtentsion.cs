using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Physicsの拡張クラス
/// </summary>
public static class PhysicsExtentsion 
{
  
    //Rayの表示時間
    private const float RAY_DISPLAY_TIME = 1;
  
    /// <summary>
    /// Rayを飛ばすと同時に画面に線を描画する
    /// </summary>
    public static bool RaycastAndDraw(Ray ray, out RaycastHit hit, float maxDistance, int layerMask)
    {
        return RaycastAndDraw(ray.origin, ray.direction, out hit, maxDistance, layerMask);
    }

    /// <summary>
    /// Rayを飛ばすと同時に画面に線を描画する
    /// </summary>
    public static bool RaycastAndDraw(Vector3 origin, Vector3 direction, out RaycastHit hit, float maxDistance, int layerMask)
    {
        if (Physics.Raycast(origin, direction, out hit, maxDistance, layerMask)){
            //衝突時のRayを画面に表示
            Debug.DrawRay(origin, hit.point - origin, Color.blue, RAY_DISPLAY_TIME, false);
            return true;
        } 

        //非衝突時のRayを画面に表示
        Debug.DrawRay(origin, direction * maxDistance, Color.green, RAY_DISPLAY_TIME, false);
        return false;
    }

}