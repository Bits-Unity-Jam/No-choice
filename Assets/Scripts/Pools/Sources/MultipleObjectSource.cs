using UnityEngine;

namespace Pools.Sources
{
    public class MultipleObjectSource<TObject> : BaseObjectSource<TObject>
        where TObject : Object
    {
        [SerializeField]
        private TObject[] instances;

        [SerializeField] private int lastRequiredIndex;
        
        public TObject[] Instances
        {
            get => instances;
            set => instances = value;
        }
        
        public override TObject GetObject()
        {
            if (lastRequiredIndex >= instances.Length)
            {
                lastRequiredIndex = 0;
            }
            
            var required = instances[lastRequiredIndex];
            
            lastRequiredIndex++;

            return required;
        }
    }
}