using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntLibrary.Util
{
    interface IManager
    {
        bool StartManager();
        void StopManager();

        bool GetRunning();
        void SetRunning(bool value);
        void SetRunning(bool value, bool soft);
    }
}
