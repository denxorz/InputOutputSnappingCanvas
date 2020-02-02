using System;
using System.Collections.Generic;
using System.Drawing;
using Denxorz;

namespace Sample
{
    public partial class BlueRectangleWithOutput : ISnapHost, IColorProvider
    {
        public IReadOnlyCollection<ISnapOutput> Outputs => new ISnapOutput[]{ outControl };

        public IReadOnlyCollection<ISnapInput> Inputs => Array.Empty<ISnapInput>();

        public Color Color => Color.Blue;

        public BlueRectangleWithOutput()
        {
            InitializeComponent();
            outControl.Attached = this;
        }
    }
}
