using UnityEngine;

public interface IWeapon
{
    void PerformAttack(Vector2 _target);
    void StopAttack();
}
