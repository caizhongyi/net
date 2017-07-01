using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Common
{
    public class ExceptionHandler:HFSoft.IExceptionHandler
    {
        #region IExceptionHandler ��Ա

        void HFSoft.IExceptionHandler.Disposal(Exception error, object container)
        {
            System.Windows.Forms.MessageBox.Show((System.Windows.Forms.IWin32Window)container,
                error.Message, "����", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        #endregion
    }
}
