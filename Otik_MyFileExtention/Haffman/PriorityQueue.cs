using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otik_MyFileExtention.Haffman
{
    internal class PriorityQueue<T>
    {
        private int _count;
        private SortedDictionary<int, Queue<T>> _storage;

        #region Get/Set

        public int Count { get { return _count; } }

        #endregion

        public PriorityQueue()
        {
            _storage = new SortedDictionary<int, Queue<T>>();
            _count = 0;
        }

        public void Enqueue(int priority, T item)
        {
            if (!_storage.ContainsKey(priority))
            {
                _storage.Add(priority, new Queue<T>());
            }
            _storage[priority].Enqueue(item);
            _count++;
        }

        public T Dequeue()
        {
            if (_count == 0)
                throw new Exception("Queue empty");
            _count--;
            foreach (Queue<T> q in _storage.Values)
            {
                if (q.Count > 0)                
                    return q.Dequeue();               
            }
            throw new Exception("Queue Error");
        }
    }
}
