

using UnityEngine;

public static class Utils
{
    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        if (explosionDistance > 0)
        {
            if (upwardsModifier == 0)
                explosionDir /= explosionDistance;
            else
            {
                explosionDir.y += upwardsModifier;
                explosionDir.Normalize();
            }
            Vector2 f = Mathf.Lerp(0, explosionForce, (1 - explosionDistance / explosionRadius)) * explosionDir;
            rb.AddForce(f, mode);

        }
    }



    public static void AddImplosionForce(this Rigidbody2D rb, float implosionForce, Vector2 implosionPosition, float implosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        var implosionDir = implosionPosition - rb.position;
        var implosionDistance = implosionDir.magnitude;

        if (implosionDistance > 0)
        {
            if (upwardsModifier == 0)
                implosionDir /= implosionDistance;
            else
            {
                implosionDir.y += upwardsModifier;
                implosionDir.Normalize();
            }
            Vector2 f = Mathf.Lerp(0, implosionForce, (1 - implosionDistance / implosionRadius)) * implosionDir;
            rb.AddForce(f, mode);
        }
    }



}
