using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponData
    {
        ICharacterController Owner;
        string ID;
        string Name;
        string PrefabName;
        Vector3 DeparturePoint; //localPosition ������ ���� �.�.
        string FVXShotingName;
        List<string> AvailableImpactIDs; // ��� ������ ����������� ������� ��� �������� - ��� ����������
    }
}