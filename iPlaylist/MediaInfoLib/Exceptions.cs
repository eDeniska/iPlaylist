using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaInfoLib
{
    public class RatingNotFoundException : Exception
    {
        public RatingNotFoundException() : base()
        {
        }

        public RatingNotFoundException(String message)
            : base(message)
        {
        }
    }
}
