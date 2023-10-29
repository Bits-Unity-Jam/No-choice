using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DataStructures.Sourses
{
    
    public abstract class BaseObjectSource<TObject> : MonoBehaviour
    {
        public abstract TObject GetObject();
        public abstract Task<TObject> GetObjectAsync();
    }
}

