using UnityEngine;

namespace Code.Blocks
{
    public class Block : MonoBehaviour, ISelectable,IMoveable
    {
        private BlockView _blockView;

        private BlockData _data;

        public float Size;
    

        public void Select()
        {
       
        }

        public void Deselect()
        {
          
        }

        public void Move()
        {
            
        }
    }
}