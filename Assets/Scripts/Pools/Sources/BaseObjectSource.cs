using UnityEngine;

namespace Pools.Sources
{
    
    public abstract class BaseObjectSource<TObject> : MonoBehaviour
    {
        public abstract TObject GetObject();
    }
}