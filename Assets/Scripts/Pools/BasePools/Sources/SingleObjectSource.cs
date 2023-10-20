using Assets.Scripts.Pools.BasePools;
using DataStructures.Sourses;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pools.BasePools.Sources
{
    public class SingleObjectSource<TObject> : BaseObjectSource<TObject>
        where TObject : Object
    {
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

        public async override Task<TObject> GetObjectAsync()
        {
            return instance;
        }
    }
}

