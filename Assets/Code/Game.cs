using System;
using System.Collections;
using Code.Blocks;
using UnityEngine;

namespace Code
{
    public class Game : MonoBehaviour
    {
        private PoolBlocks _pool;
        private void Start()
        {
        
        }

        private void Initialization()
        {
            _pool = new PoolBlocks();
        }
    }
}