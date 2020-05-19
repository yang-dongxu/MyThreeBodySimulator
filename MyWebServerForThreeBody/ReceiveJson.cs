using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServerForThreeBody
{
    public class ReceiveJson
    {
        public int steps;
        public int steps_pic;
        public float step_time;
        public float G;
        public enum perjc_type { raw, par, point };
        public perjc_type p;
        public float d;
    }
}
