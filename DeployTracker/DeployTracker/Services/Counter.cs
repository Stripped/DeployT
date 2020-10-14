﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Services
{
    public interface ICounter
    {
        void Increment();
        int GetValue();
    }
    public class Counter : ICounter
    {
        private int _counter = 0;
        public void Increment() => ++_counter;
        public int GetValue() => _counter;
    }
}
