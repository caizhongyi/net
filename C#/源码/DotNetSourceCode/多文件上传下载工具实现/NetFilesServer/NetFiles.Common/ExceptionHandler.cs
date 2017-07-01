using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Common
{
    public class ExceptionHandler:HFSoft.IExceptionHandler
    {
        #region IExceptionHandler ≥…‘±

        void HFSoft.IExceptionHandler.Disposal(Exception error, object container)
        {
            System.Windows.Forms.MessageBox.Show((System.Windows.Forms.IWin32Window)container,
                error.Message, "¥ÌŒÛ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        #endregion
    }
}
