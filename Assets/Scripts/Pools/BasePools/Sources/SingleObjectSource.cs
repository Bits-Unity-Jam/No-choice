using UnityEngine;

namespace Pools.Sources
{
    public class SingleObjectSource<TObject> : BaseObjectSource<TObject>
        where TObject : Object
    {
        [SerializeField]
        private TObject instance;
        
        public TObject Instance
        {
            get => instance;
            set => instance = value;
        }
        
        public override TObject GetObject()
        {
            return instance;
        }
    }
}