﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEditor.XNA
{
    public class MapDisplay : GraphicsDeviceControl
    {
        public event EventHandler OnInitialize;
        public event EventHandler OnDraw;

        protected override void Initialize()
        {
            if (OnInitialize != null)
                OnInitialize(this, null);
        }

        protected override void Draw()
        {
            if (OnDraw != null)
                OnDraw(this, null);
        }
    }
}
