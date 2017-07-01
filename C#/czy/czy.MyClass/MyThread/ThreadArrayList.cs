using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace czy.MyClass.MyThread
{
    /// <summary>
    /// 线程ArrayList
    /// </summary>
    public class ThreadArrayList : ArrayList
    {

        public override int Add(object value)
        {
            lock (this.SyncRoot)
            {
                return base.Add(value);
            }
        }
        public override void Remove(object obj)
        {
            lock (this.SyncRoot)
            {
                if (this.Contains(obj))
                {
                    base.Remove(obj);
                }
            }
        }
        public override void RemoveAt(int index)
        {
            lock (this.SyncRoot)
            {
                base.RemoveAt(index);
            }
        }
        /// <summary>
        /// 获取第一个元素
        /// </summary>
        /// <returns></returns>
        public object GetOne()
        {
            object o = null;
            lock (this.SyncRoot)
            {
                if (this.Count > 0)
                {
                    o = this[0];
                }
            }
            return o;
        }
    }
}

