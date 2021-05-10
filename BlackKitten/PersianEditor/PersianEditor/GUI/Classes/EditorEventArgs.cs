using System;
using System.Windows.Input;

namespace PersianEditor
{
    public class NotifyEventArgs : CmdEventArgs
    {
        public readonly NotifyType notifyType;
        public readonly NotifyAction action;
        public NotifyEventArgs(NotifyType notifyType, NotifyAction action, object Data)
            : base(Data)
        {
            this.notifyType = notifyType;
            this.action = action;
        }
    }

    public class CmdToXnaEventArgs : CmdEventArgs
    {
        public readonly ToXnaHeader Header;
        public readonly CmdToXna Cmd;
        public CmdToXnaEventArgs(ToXnaHeader Header, CmdToXna Cmd, object Data)
            : base(Data)
        {
            this.Header = Header;
            this.Cmd = Cmd;
        }
    }

    public class CmdToGDIEventArgs : CmdEventArgs
    {
        public readonly CmdToGDI Cmd;
        public CmdToGDIEventArgs(CmdToGDI Cmd, object Data)
            : base(Data)
        {
            this.Cmd = Cmd;
        }
    }
}
