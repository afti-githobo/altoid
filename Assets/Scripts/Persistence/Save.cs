using System;
using System.Collections.Generic;
using UnityEngine;

namespace Altoid.Persistence
{
    public abstract class SaveField
    {
        public virtual float GetFloat()
        {
            throw new InvalidCastException();
        }

        public virtual int GetInt()
        {
            throw new InvalidCastException();
        }

        public virtual string GetString()
        {
            throw new InvalidCastException();
        }
    }

    public class SaveFieldFloat : SaveField
    {
        [SerializeField]
        private float value;

        public SaveFieldFloat(float value) => this.value = value;

        public override float GetFloat() => value;
    }

    public class SaveFieldInt : SaveField
    {
        [SerializeField]
        private int value;

        public SaveFieldInt(int value) => this.value = value;

        public override int GetInt() => value;
    }

    public class SaveFieldString : SaveField
    {
        [SerializeField]
        private string value;

        public SaveFieldString(string value) => this.value = value;

        public override string GetString() => value;
    }

    public class Save : ScriptableObject
    {
        public static Save Current { get; private set; } = CreateInstance<Save>(); // temp - should be a saner mechanism for supplying a default save

        public DateTime Timestamp { get => _timestamp; }
        private DateTime _timestamp = DateTime.Now;
        public IReadOnlyDictionary<string, SaveField> Data { get => _data; }
        private Dictionary<string, SaveField> _data = new();
        public int Height { get => _height; }
        private int _height = 0;
        public int Checksum { get => _checksum; }
        private int _checksum = 0;

        public float SetFloat(string k, float v) => (_data[k] = new SaveFieldFloat(v)).GetFloat();

        public float GetFloat(string k, float? _default = null)
        {
            if (Data.ContainsKey(k)) return Data[k].GetFloat();
            else if (_default != null) return SetFloat(k, _default.Value);
            else throw new KeyNotFoundException($"Save has no field {k}");
        }

        public int SetInt(string k, int v) => (_data[k] = new SaveFieldInt(v)).GetInt();

        public int GetInt(string k, int? _default = null)
        {
            if (Data.ContainsKey(k)) return Data[k].GetInt();
            else if (_default != null) return SetInt(k, _default.Value);
            else throw new KeyNotFoundException($"Save has no field {k}");
        }

        public string SetString(string k, string v) => (_data[k] = new SaveFieldString(v)).GetString();

        public string GetString(string k, string _default = null)
        {
            if (Data.ContainsKey(k)) return Data[k].GetString();
            else if (_default != null) return SetString(k, _default);
            else throw new KeyNotFoundException($"Save has no field {k}");
        }
    }

}