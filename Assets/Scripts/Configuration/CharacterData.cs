using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public int HP;
    public Gun Gun;
    public float Speed;
    public CharacterGraphics Graphics;
    public int Damage;
}
