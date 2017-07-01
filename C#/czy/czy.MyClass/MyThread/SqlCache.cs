using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace czy.MyClass.MyThread
{
    /// <summary>
    ///  Sql 语句队列  
    ///  为提高效率 非立即执行的数据库操作以Sql语句形式放在此队列中  顺序执行
    /// </summary>
    /// <remarks></remarks>
    public class SqlCache
    {
        /// <summary>
        /// 事件委托
        /// </summary>
        /// <param name="e">参数</param>
        public delegate void CacheEventHandler(object e);
        /// <summary>
        /// 增加一条记录前事件
        /// </summary>
        public event CacheEventHandler BeforeAdd;
        /// <summary>
        /// 增加一条记录后事件
        /// </summary>
        public event CacheEventHandler AfterAdd;
        /// <summary>
        /// 错误事件
        /// </summary>
        public event CacheEventHandler Error;
        /// <summary>
        /// 运行事件
        /// </summary>
        public event CacheEventHandler Runing;
        //   private List<string> mList;
        //private System.Collections.Generic.Queue<string> mSqls = new Queue<string>(); 
        SafeQueue mSqls = new SafeQueue();
        private Object mListLock = new object();
        private Thread mThread;
        private int mState = 0;  //0 停止 1 运行 2 请求停止

        public SqlCache()
        {

        }

        //~SqlCache()
        //{

        //}

        public int Start()
        {
            mState = 1;
            mThread = new Thread(new ThreadStart(_Proc));
            mThread.Start();
            return 0;
        }

        public int Stop()
        {
            mState = 2;
            // 如果需要安全退出  这里应该等待 mState=0 然后再强制结束线称
            //   while (mState != 0 && mThread.IsAlive )
            //        Thread.Sleep(50);

            mThread.Abort();
            return 0;
        }

        public int Add(string Sql)
        {
            lock (mListLock)
            {
                try
                {
                    if (BeforeAdd != null)
                    {
                        BeforeAdd(Sql.ToString());
                    }
                    //mSqls.Enqueue(Sql);     
                    mSqls.Push(Sql);
                    if (AfterAdd != null)
                    {
                        AfterAdd(Sql.ToString());
                    }
                }
                catch (Exception err) { if (Error != null) { Error(err); } }
            }
            return 0;
        }

        public string _GetOne()
        {
            string Rt = "";
            if (mSqls.Count > 0)
                lock (mListLock)
                {
                    try
                    {
                        //Rt = mSqls.Dequeue();
                        Rt = mSqls.Pop(100) as string;
                    }
                    catch (Exception err) { if (Error != null) { Error(err); } };
                }
            if (Rt == null) Rt = "";
            return Rt;
        }
        //////
        private void _Proc()
        {
            string sql = "";
            while (mState != 2)
            {
                try
                {
                    sql = _GetOne();
                    if (sql == "")
                        Thread.Sleep(20);
                    else
                    {
                       // mSqls.Push(sql);
                        if (!string.IsNullOrEmpty(sql))
                        {
                            if (Runing != null)
                            {
                                Runing(sql);
                            }
                        }
                    }
                }
                catch (Exception err) { if (Error != null) { Error(err); } };
            }
            mState = 0;
        }

    }
}
