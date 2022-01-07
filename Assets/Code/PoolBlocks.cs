using System.Collections.Generic;
using Code.Blocks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = System.Random;

namespace Code
{
    public class PoolBlocks : MonoBehaviour
    {
        [SerializeField] private AssetReference _blockPrefab;
        [SerializeField] private int _capacity;
        [SerializeField] private Queue<Block> _pool;

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
            {
                block.gameObject.SetActive(false);
                _pool.Enqueue(block);
            }
        }

        private void GeneratePool()
        {
            if (_capacity <= 0)
                _capacity = 100;

            _pool = new Queue<Block>(_capacity);

            Addressables.LoadAssetAsync<GameObject>(_blockPrefab).Completed += LoadingComplete;
        }

        private void LoadingComplete(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                var loadedObj = obj.Result;
                for (int i = 0; i < _capacity; i++)
                {
                    var instantiatedObj = Instantiate(loadedObj);
                    var block = instantiatedObj.GetComponent<Block>();
                    block.name = $"block{i}";
                    _pool.Enqueue(block);
                    instantiatedObj.SetActive(false);
                }
            }
        }
    }
}