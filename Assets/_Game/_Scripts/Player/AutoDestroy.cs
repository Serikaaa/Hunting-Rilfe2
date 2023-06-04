using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class AutoDestroy : NetworkBehaviour
{
    public float delayBeforeDestroy = 0.5f;
 
    [ServerRpc(RequireOwnership =false)]
    private void DestroyParticlesServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
        Destroy(gameObject, delayBeforeDestroy);
    }
}
