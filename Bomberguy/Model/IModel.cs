using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace Bomberguy.Model
{
    interface IModel
    {
        Drawable GetView();
    }
}
