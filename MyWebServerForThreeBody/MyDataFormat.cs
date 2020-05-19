using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MyThreeBodySimulator;
using Newtonsoft.Json;

namespace MyWebServerForThreeBody
{
    static class MyDataFormat
    {
        static public List<Dictionary<int, Vector3>> ParseInputJson(string paramsJson, MyEnvironment env)
        {
            ReceiveJson param = JsonConvert.DeserializeObject<ReceiveJson>(paramsJson);
            List<Dictionary<int, Vector3>> Result = env.StepsMove(param.steps, param.steps_pic, param.step_time, param.G);

            //进行投影校正
            var corrected = MyDataFormat.Projection(Result, param);

            return corrected;
        }

        static public string ParseOutput(List<Dictionary<int, Vector3>> Stepresult)
        {
            string resultJson = JsonConvert.SerializeObject(Stepresult);
            return resultJson;
        }

        static public List<Dictionary<int, Vector3>> Projection (List<Dictionary<int, Vector3>> positions, ReceiveJson param)
        {
            List<Dictionary<int, Vector3>> prop_results=new List<Dictionary<int, Vector3>>();
            if (param.p == ReceiveJson.perjc_type.raw)
            {
                return positions;
            }
            else if (param.p == ReceiveJson.perjc_type.par)
            {
                foreach (var i in positions)
                {
                    var changed_positons = new Dictionary<int, Vector3>();
                    foreach (int key in i.Keys)
                    {
                        Vector3 p = new Vector3(i[key].X, i[key].Y, 0);
                        changed_positons.Add(key, p);
                    }
                    prop_results.Add(changed_positons);
                }
                return prop_results;
            }
            else
            {
                foreach (var i in positions)
                {
                    var changed_positons = new Dictionary<int, Vector3>();
                    foreach (int key in i.Keys)
                    {
                        float X = i[key].X * param.d / i[key].Z;
                        float Y = i[key].Y * param.d / i[key].Z;
                        Vector3 p = new Vector3(X, Y, 0);
                        changed_positons.Add(key, p);                        
                    }
                    prop_results.Add(changed_positons);
                }
                return prop_results;
            }
        }


    }

}


