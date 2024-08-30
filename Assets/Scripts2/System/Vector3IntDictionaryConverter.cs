using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;
public class Vector3IntDictionaryConverter : JsonConverter {
    public override bool CanWrite  { 
        get { return false; } 
    }

    public override bool CanConvert(Type objectType) {
        if(objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Dictionary<,>)) {
            Type[] types = objectType.GetGenericArguments();
            if(types[0] == typeof(Vector3Int)) {
                return true;
            }
        }
        return false;
    }

    public Dictionary<Vector3Int, T> ConvertToVector3IntDictionary<T>(JObject jo) {
        Dictionary<string, T> dict = jo.ToObject<Dictionary<string, T>>();
        
        Dictionary<Vector3Int, T> ret = new Dictionary<Vector3Int, T>();
        foreach(KeyValuePair<string, T> pair in dict) {
            ret.Add(ConvertToVector3Int(pair.Key), pair.Value);
        }
        return ret;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
        JObject jo = JObject.Load(reader);

        Type[] types = objectType.GetGenericArguments();
        MethodInfo method = GetType().GetMethod("ConvertToVector3IntDictionary")
            .MakeGenericMethod(new Type[] { types[1] });
        return method.Invoke(this, new object[] { jo });
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
        throw new NotImplementedException();
    }

    Vector3Int ConvertToVector3Int(string str) {
        str = str[1..^1];
        
        string[] splited = str.Split(',');

        return new Vector3Int {
            x = int.Parse(splited[0]),
            y = int.Parse(splited[1]),
            z = int.Parse(splited[2])
        };
    }
}
