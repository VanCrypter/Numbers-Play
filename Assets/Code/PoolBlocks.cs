using System;
using System.Collections.Generic;
using Code.Blocks;
using UnityEngine;
using  UnityEngine.AddressableAssets;
using  UnityEngine.ResourceManagement.AsyncOperations;

namespace Code
{
    public class PoolBlocks : MonoBehaviour
    {
        [SerializeField] private AssetReference _blockPrefab;

        private Queue<Block> _pool;
        private int _capacity;

        private void Start()
        {
            if (_capacity <= 0)
                _capacity = 100;

            GeneratePool();
        }

        public Block GetBlock()
        {
            if (_pool.Count <= 0)
                GeneratePool();
            return _pool.Dequeue();
        }

        public void ReturnBlock(Block block)
        {
            if (block != null)
                _pool.Enqueue(block);
        }
  
        private void GeneratePool()
        {
            if (_capacity <= 0)
                _capacity = 100;

            _pool = new Queue<Block>(_capacity);

            for (int i = 0; i < _capacity; i++)
            {
                Addressables.LoadAssetAsync<GameObject>(_blockPrefab).Completed += LoadingComplete;
            }
        }

        private void LoadingComplete(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                var loadedObj = obj.Result;
               var instantiatedObj = Instantiate(loadedObj,this.transform);
                _pool.Enqueue(instantiatedObj.GetComponent<Block>());
                instantiatedObj.SetActive(false);
            }
        }
    }
}