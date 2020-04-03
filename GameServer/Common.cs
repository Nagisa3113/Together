﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum ActionCode
    {
        None,
        Join,
        Move,
        Shoot,
        Anim,

        GameOver,
    }
    public enum RequestCode
    {
        None,
        Game,
    }

    public enum ReturnCode
    {
        Success,
        Fail,
        NotFound,
    }
}