using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.Core.Commands
{
    public interface ICommand<in TParameters, out TResult>
    {
        TResult Execute(TParameters parameters);
    }
}
