using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TorchStudio;

namespace TorchStudio
{
    public static class TReferencer
    {
        [SerializeField] static Dictionary<string, object> references { get; } = new Dictionary<string, object>();

        public static void AddReference<T>(params (string _string, T _object)[] _tuple)
        {
            foreach (var (_string, _object) in _tuple)
            {
                if (!references.ContainsKey(_string))
                {
                    references.Add(_string, _object);
                }
                else
                {
                    Debug.LogError($"Failed to add {_string}'s {_object}, it is already in the reference!");
                }
            }
        }

        public static T AddReference<T>(this T _object)
        {
            if (!references.ContainsKey(_object.ToString()))
            {
                references.Add(_object.ToString(), _object);
            }
            else
            {
                Debug.LogError($"Failed to add {_object}, it is already in the reference!");

            }
            return _object;
        }

        public static T AddReference<T>(string _string, T _object)
        {
            if (!references.ContainsKey(_string))
            {
                references.Add(_string, _object);
            }
            else
            {
                Debug.LogError($"Failed to add {_string}'s {_object}, it is already in the reference!");
            }
            return _object;
        }

        public static T AddReference<T>(this T _object, string _string)
        {
            if (!references.ContainsKey(_string))
            {
                references.Add(_string, _object);
            }
            else
            {
                Debug.LogError($"Failed to add {_string}'s {_object}, it is already in the reference!");
            }          
            return _object;
        }

        public static void RemoveReference(params string[] _string)
        {
            foreach (var item in _string)
            {
                if (references.ContainsKey(item))
                {
                    references.Remove(item);
                }
                else
                {
                    Debug.LogError($"Failed to remove {_string}, it is not in the reference!");
                }
            }
        }

        public static void RemoveReference<T>(params T[] _object)
        {
            bool counter = false;
            foreach (var refer in references)
            {
                
                foreach (var item in _object)
                {
                    if (refer.Value == item as object)
                    {
                        references.Remove(_object.ToString());
                        counter = true;
                        break;
                    }
                }
                if (!counter)
                {
                    Debug.LogError($"Failed to remove {_object}, it is not in the reference!");
                }
            }
        }

        public static T RemoveReference<T>(this T _object)
        {
            if (references.ContainsValue(_object.ToString()))
            {
                references.Remove(_object.ToString());
            }
            else
            {
                Debug.LogError($"Failed to remove {_object}, it is not in the reference!");
            }
            return _object;
        }

        public static T RemoveReference<T>(this T _object, string _string)
        {
            if (references.ContainsKey(_string))
            {
                references.Remove(_string);
            }
            else
            {
                Debug.LogError($"Failed to remove {_string}'s {_object}, it is not in the reference!");
            }
            return _object;
        }

        public static void RemoveAllValueOfType<T>()
        {
            foreach (var item in references)
            {
                if(item.Value.GetType() == typeof(T))
                {
                    Debug.Log($"Removed {item.Key}'s {item.Value} reference.");
                    references.Remove(item.Key);
                }
            }
        }

        public static bool GetReference<T>(out T _object)
        {
            if (references.ContainsKey(typeof(T).ToString()))
            {
                _object = (T)references[typeof(T).ToString()];
                return true;
            }
            else
            {
                Debug.LogError($"Failed to get {typeof(T)}, it is not in the reference!");
                _object = default;
                return false;
            }
        }

        public static bool GetReference<T>(string _string, out T _object)
        {
            if (references.ContainsKey(_string))
            {
                _object = (T)references[_string];
                return true;
            }
            else
            {
                Debug.LogError($"Failed to get {_string} 's {typeof(T)}, it is not in the reference!");
                _object = default;
                return false;
            }

        }

        public static void ShowAllReferences()
        {
            foreach (var item in references)
            {
                Debug.LogError($" Key = [{item.Key}], value = [{item.Value}]");
            }
        }

        public static void ClearAllReferences()
        {
            Debug.Log($"Cleared {references.Count} references.");
            references.Clear();
        }
    }
}


public class TestingRef : MonoBehaviour
{
    public void Referencer_Method_Instruction()
    {
        #region AddReference
        //AddReference

        // Add reference by this class type with class name as string key.
        this.AddReference();

        // Add reference by this class type with specify string key.
        this.AddReference("asd");

        // Add reference when creating new class with class name as string key.
        TestingRef testingRef_Classname = new TestingRef().AddReference();

        // Add reference when creating new class with specify string key.
        TestingRef testingRef = new TestingRef().AddReference("TestingRef");

        // Add reference by specify string key and object value.
        TestingRef testingRef_Solo = new TestingRef();
        TReferencer.AddReference("testingRef_Solo", testingRef_Solo);

        // Add multiple references within a single call, each with a specify string key, and a object value assigned. 
        TReferencer.AddReference(
            ("Testing_1", new TestingRef()),
            ("Testing_2", new TestingRef()),
            ("Testing_3", new TestingRef()));
        #endregion

        #region GetReference
        //GetReference

        //Get reference by specify string key, out with a result.
        if (TReferencer.GetReference("MyTransform", out TestingRef _testingRef_withKey))
        {

        }

        //Get reference by specify class type, out with a result.
        if (TReferencer.GetReference(out TestingRef _testing_withClassName))
        {

        }
        #endregion

        #region RemoveReference
        //RemoveReference

        // Remove value by this class type with class name. (Ignore string key, randomly.)
        this.RemoveReference();

        // Remove value by this class type with specify string key.
        this.RemoveReference("asd");

        // Remove value by this specify type with class name as string key.
        TReferencer.RemoveReference<TestingRef>();

        // Remove value by this specify type with class name as string key.
        TReferencer.RemoveReference(typeof(TestingRef), "asd");

        //Remove all reference by specify type value.
        TReferencer.RemoveAllValueOfType<TestingRef>();
        #endregion


        // Shows all current references.
        TReferencer.ShowAllReferences();

        // Clear all current references.
        TReferencer.ClearAllReferences();
    }
}

