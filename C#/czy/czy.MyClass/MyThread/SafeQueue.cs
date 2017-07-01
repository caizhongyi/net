using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace czy.MyClass.MyThread
{
    /// <summary>
    /// 线程安全队列
    /// </summary>
    public class SafeQueue
    {
        private Queue queue = Queue.Synchronized(new Queue());
        //private int count = 0;
        private int max_count = int.MaxValue - 1;
        private object read_lock = new object();
        private object write_lock = new object();
        //private SafeQueue queue = new SafeQueue();

        public int MaxTeamCount = 50;
        /// <summary>
        /// 元素数
        /// </summary>
        public int Count
        {
            get
            {
                return queue.Count;
            }
        }


        /// <summary>
        /// 压入数据
        /// </summary>
        /// <param name="data">数据</param>
        public void Push(object data)
        {
            lock (write_lock)
            {
                while (Count >= max_count)
                {
                    Monitor.Wait(write_lock, Timeout.Infinite);
                }
            }
            queue.Enqueue(data);
            //Interlocked.Increment(ref count);
            lock (read_lock)
            {
                Monitor.Pulse(read_lock);
            }
        }


        /// <summary>
        /// 弹出数据, 如果队列为空则返回空值
        /// </summary>
        /// <param name="interval">等待时间</param>
        /// <returns>队首</returns>
        public object Pop(int interval)
        {
            lock (read_lock)
            {
                if (Count == 0) Monitor.Wait(read_lock, interval);
            }
            object o = null;
            try
            {
                if (queue.Count > 0)
                {
                    o = queue.Dequeue();
                }
                //Interlocked.Decrement(ref count); 
            }
            catch (Exception)
            {
            }
            lock (write_lock)
            {
                Monitor.Pulse(write_lock);
            }
            return o;
        }

        public object[] Dump()
        {
            return queue.ToArray();
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear()
        {
            queue.Clear();
        }

    }
}
