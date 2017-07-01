using System.Threading;
using System.ComponentModel;
using System.Windows.Input;
public class MClick
{

    private static int intCount;
    private static object objSender;
    private static MouseButtonEventArgs mbeE;
    private static MouseButtonEventHandler handleClick;
    private static int intClickCount = 0;
    private static bool bolClicked = false;
    private static bool bolClicking = false;
    private static object objThreadLock;
    private static AsyncOperation asyncOperation;
    private static int intTimeOut = 200;


    /// <summary>
    /// 鼠标双击事件
    /// </summary>
    /// <param name="count">点击次数</param>
    /// <param name="handle">引发执行的事件名称</param>
    /// <param name="sender">object</param>
    /// <param name="e">MouseButtonEventArgs</param>
    public static void MulClick(int count, MouseButtonEventHandler handle, object sender, MouseButtonEventArgs e)
    {
        bolClicking = true;
        if (bolClicked)
        {
            intClickCount++;
        }
        else
        {
            bolClicked = true;
            intCount = count;
            handleClick = handle;
            objSender = sender;
            mbeE = e;
            asyncOperation = AsyncOperationManager.CreateOperation(null);
            objThreadLock = new object();
            Thread thread = new Thread(ResetThread);
            thread.Start();
            while (!thread.IsAlive) ;
        }
    }


   
    public static int TimeOut
    {
        get
        {
            return intTimeOut;
        }

        set
        {
            intTimeOut = value < 1 ? 1 : value;
        }
    }

    

    private static void ResetThread()
    {
        while (bolClicking)
        {
            bolClicking = false;
            Thread.Sleep(TimeOut);
        }

        lock (objThreadLock)
        {
            if (intCount == ++intClickCount)
            {
                asyncOperation.Post(callback, objSender);
            }

            intClickCount = 0;
            bolClicked = false;
        }

    }

    private static void callback(object state)
    {
        handleClick(objSender, mbeE);
    }
}
