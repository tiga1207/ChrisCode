using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoblinType {Warrior, Shaman, Warcheif}
public class Goblin : MonsterBase
{
    public GoblinType goblinType;
    //ScriptableObject로 변수 관리 예정
    protected override void Start()
    {
        base.Start();   
        switch (goblinType)
        {
            case GoblinType.Warrior:
            hp =10;
                break;
        }
    }

    void Update()
    {
        
    }
}
