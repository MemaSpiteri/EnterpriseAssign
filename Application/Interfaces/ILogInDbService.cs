﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ILogInDbService
    {
        public void AddLog(Logs log);
    }
}