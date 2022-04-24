using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Decals
{
    public class SetUpDecalData
    {
        public Quaternion rot;
        public Vector3 pos, scale;
        public SetUpDecalData() { }
        public SetUpDecalData(Vector3 _pos, Vector3 _scale, Quaternion _rot) { pos = _pos; scale = _scale; rot = _rot; }
    }


    public static GameObject SpawnDecal(GameObject _decalPrefab, RaycastHit _raycastHit)
    {
        Decals.SetUpDecalData decalData = Decals.SetUpDecal(_decalPrefab, _raycastHit.point, _raycastHit.normal, _raycastHit.transform.localScale);
        GameObject decalGO = GameObject.Instantiate(_decalPrefab, decalData.pos, decalData.rot, _raycastHit.transform);
        decalGO.transform.localScale = decalData.scale;

        return decalGO;
    }
    public static GameObject SpawnDecal(GameObject _decalPrefab, Collision _col)
    {
        Decals.SetUpDecalData decalData = Decals.SetUpDecal(_decalPrefab, _col);
        GameObject decalGO = GameObject.Instantiate(_decalPrefab, decalData.pos, decalData.rot, _col.transform);
        decalGO.transform.localScale = decalData.scale;

        return decalGO;
    }

    public static SetUpDecalData SetUpDecal(GameObject _decalPrefab, Collision _col)
    {
        ContactPoint contactPoint = _col.GetContact(0);
        Quaternion decalRot =
            _decalPrefab.transform.rotation * Quaternion.FromToRotation(_decalPrefab.transform.forward, contactPoint.normal);
        Vector3 decalScale = _decalPrefab.transform.localScale;
        Vector3 colScale = _col.transform.localScale;
        Vector3 finalDecalScale = new Vector3(decalScale.x / colScale.x, decalScale.y / colScale.y, decalScale.z / colScale.z);

        return new SetUpDecalData(contactPoint.point, finalDecalScale, decalRot);
    }
    public static SetUpDecalData SetUpDecal(GameObject _decalPrefab, Vector3 _hitPos, Vector3 _hitNormal, Vector3 _hitScale)
    {
        Quaternion decalRot =
            _decalPrefab.transform.rotation * Quaternion.FromToRotation(_decalPrefab.transform.forward, _hitNormal);
        Vector3 decalScale = _decalPrefab.transform.localScale;
        Vector3 colScale = _hitScale;
        Vector3 finalDecalScale = new Vector3(decalScale.x / colScale.x, decalScale.y / colScale.y, decalScale.z / colScale.z);

        return new SetUpDecalData(_hitPos, finalDecalScale, decalRot);
    }

}
