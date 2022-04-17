using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable//this will be used to move an object
{
    void Move(Rigidbody2D thisBody, Vector2 movement, float moveSpeed);
}

public interface IDamageable//this will be used to do damage to a character
{
    void Damage(int damage, Collider2D obj);
    IEnumerator Invincibility();
}

public interface IKillable//this will be used to kill a character
{
    void Kill(GameObject obj);
}

public interface IPushable//this will be used to push a character after damaging
{
    float thrust { get; }
    float pushTime { get; }
    void Push(Collider2D other);
    IEnumerator PushCo(Rigidbody2D character);
}
public interface IInteractable//reserved for interaction
{
    void Interact();
}

public interface IActivatable//reserved for activation
{
    void Activate();
}

public interface ICollectable//reserved for collecting
{
    void Collect();
}
public interface IDataPersistence//for loading or saving data
{
    void LoadData(GameData data);
    void SaveData(GameData data);
}
