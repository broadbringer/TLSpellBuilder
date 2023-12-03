using System;
using System.Collections.Generic;
using UnityEngine;

namespace Packages.ThroneAndLibertySpellBuilder.Scripts.Extensions
{
    public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>,
        UnityEngine.ISerializationCallbackReceiver
    {
        public IEqualityComparer<TKey> Comparer => comparer;

        public int Count => dictionary?.Count ?? 0;

        [SerializeField]
        private TKey[] keys;

        [SerializeField]
        private TValue[] values;

        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private TValue[] _values;

        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private TKey[] _keys;

        [NonSerialized]
        private Dictionary<TKey, TValue> dictionary;

        [NonSerialized]
        private IEqualityComparer<TKey> comparer;
        
        public SerializableDictionary() => dictionary = new Dictionary<TKey, TValue>(comparer);

        public SerializableDictionary(IDictionary<TKey, TValue> source, IEqualityComparer<TKey> comparer = null)
        {
            dictionary = new Dictionary<TKey, TValue>(source);
            this.comparer = comparer;
        }

        public SerializableDictionary(IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, TValue>(comparer);
            this.comparer = comparer;
        }

        public static implicit operator Dictionary<TKey, TValue>(SerializableDictionary<TKey, TValue> value) => value.dictionary;

        public static implicit operator SerializableDictionary<TKey, TValue>(Dictionary<TKey, TValue> value) => new SerializableDictionary<TKey, TValue>(value);
        
        public void Add(TKey key, TValue value) => dictionary.Add(key, value);

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public ICollection<TKey> Keys => dictionary.Keys;

        public bool Remove(TKey key) => dictionary.Remove(key);

        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);

        public ICollection<TValue> Values => dictionary.Values;

        public TValue this[TKey key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        public void Clear() => dictionary?.Clear();

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) =>
            (dictionary as ICollection<KeyValuePair<TKey, TValue>>).Add(item);

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) =>
            dictionary != null && (dictionary as ICollection<KeyValuePair<TKey, TValue>>).Contains(item);

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            (dictionary as ICollection<KeyValuePair<TKey, TValue>>).CopyTo(array, arrayIndex);

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) =>
            dictionary != null && (dictionary as ICollection<KeyValuePair<TKey, TValue>>).Remove(item);

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator() => dictionary.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => dictionary.GetEnumerator();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
            dictionary.GetEnumerator();

        void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            var dictionaryKeys = keys ?? _keys;
            var dictionaryValues = values ?? _values;
            
            
            if (dictionaryKeys != null && dictionaryValues != null)
            {
                dictionary = dictionary ?? new Dictionary<TKey, TValue>();
                dictionary.Clear();
                
                for (var iterator = 0; iterator < dictionaryKeys.Length; iterator++)
                {
                    if (iterator < dictionaryValues.Length)
                        dictionary[dictionaryKeys[iterator]] = dictionaryValues[iterator];
                    else
                        dictionary[dictionaryKeys[iterator]] = default;
                }
            }

            keys = null;
            values = null;
            _values = null;
            _keys = null;
        }

        void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                keys = null;
                values = null;
                _values = null;
                _keys = null;
            }
            else
            {
                var cnt = dictionary.Count;
                keys = new TKey[cnt];
                values = new TValue[cnt];
                var iterator = 0;
                using (var numerator = dictionary.GetEnumerator())
                {
                    while (numerator.MoveNext())
                    {
                        keys[iterator] = numerator.Current.Key;
                        values[iterator] = numerator.Current.Value;
                        iterator++;
                    }
                }

                _values = null;
                _keys = null;
            }
        }
    }
}