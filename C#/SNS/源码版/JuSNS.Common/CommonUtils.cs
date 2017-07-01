using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using JuSNS.Common;

namespace System
{
    /// <summary>
    /// The CommonUtils class.
    /// </summary>
    public sealed class CommonUtils
    {
        private CommonUtils() { }

        #region DefaultValue

        /// <summary>
        /// Gets the default value of a specified Type.
        /// </summary>
        /// <returns>The default value.</returns>
        public static T DefaultValue<T>()
        {
            return default(T);
        }

        /// <summary>
        /// Gets the default value of a specified Type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object DefaultValue(Type type)
        {
            Check.Require(type, "type");

            return typeof(CommonUtils).GetMethod("DefaultValue", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null).MakeGenericMethod(type).Invoke(null, null);
        }

        #endregion

        #region GetType

        /// <summary>
        /// Gets a type in all loaded assemblies of current app domain.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>
        public static Type GetType(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return null;
            }

            Type t = null;

            if (fullName.StartsWith("System.Nullable`1["))
            {
                string genericTypeStr = fullName.Substring("System.Nullable`1[".Length).Trim('[', ']');
                if (genericTypeStr.Contains(","))
                {
                    genericTypeStr = genericTypeStr.Substring(0, genericTypeStr.IndexOf(",")).Trim();
                }
                t = typeof(Nullable<>).MakeGenericType(GetType(genericTypeStr));

                if (t != null)
                {
                    return t;
                }
            }

            try
            {
                t = Type.GetType(fullName);
            }
            catch
            {
            }

            if (t == null)
            {
                try
                {
                    if (fullName.Contains(","))
                    {
                        string[] classNameAssembly = fullName.Split(',');
                        Assembly ass = Assembly.LoadFrom(classNameAssembly[1]);
                        if (ass != null)
                            t = ass.GetType(classNameAssembly[0]);
                    }
                    else
                    {
                        Assembly[] asses = AppDomain.CurrentDomain.GetAssemblies();

                        for (int i = asses.Length - 1; i >= 0; i--)
                        {
                            Assembly ass = asses[i];
                            try
                            {
                                t = ass.GetType(fullName);
                            }
                            catch
                            {
                            }

                            if (t != null)
                            {
                                break;
                            }
                        }
                    }
                }
                catch
                {
                }
            }

            return t;
        }

        /// <summary>
        /// GetOriginalTypeOfNullableType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetOriginalTypeOfNullableType(Type type)
        {
            Check.Require(type, "type");

            if (type.ToString().StartsWith("System.Nullable`1["))
            {
                return GetType(type.ToString().Substring("System.Nullable`1[".Length).Trim('[', ']'));
            }

            return type;
        }

        #endregion



        #region Misc

        /// <summary>
        /// Check equal of two arrays
        /// </summary>
        /// <param name="leftArr">left array</param>
        /// <param name="rightArr">right array</param>
        /// <returns></returns>
        public static bool IsArrayEquals(IEnumerable leftArr, IEnumerable rightArr)
        {
            if ((leftArr == null && rightArr != null) ||
                (leftArr != null && rightArr == null))
            {
                return false;
            }

            IEnumerator enLeft = leftArr.GetEnumerator();
            IEnumerator enRight = rightArr.GetEnumerator();

            return IsArrayEquals(enLeft, enRight);
        }

        /// <summary>
        /// Check equal of two arrays
        /// </summary>
        /// <param name="enLeft">left array</param>
        /// <param name="enRight">right array</param>
        /// <returns></returns>
        public static bool IsArrayEquals(IEnumerator enLeft, IEnumerator enRight)
        {
            if ((enLeft == null && enRight != null) ||
                (enLeft != null && enRight == null))
            {
                return false;
            }

            bool isEquals = true;

            while (true)
            {
                bool leftHasNext = enLeft.MoveNext();
                bool rightHasNext = enRight.MoveNext();

                if ((leftHasNext && (!rightHasNext)) ||
                    ((!leftHasNext) && rightHasNext))
                {
                    isEquals = false;
                    break;
                }

                if ((!leftHasNext) && (!rightHasNext))
                {
                    break;
                }

                if ((enLeft.Current == null && enRight.Current != null) ||
                    (enLeft.Current != null && enRight.Current == null) ||
                    (!enLeft.Current.Equals(enRight.Current)))
                {
                    isEquals = false;
                    break;
                }
            }

            return isEquals;
        }

        /// <summary>
        /// Convert enumerator to List
        /// </summary>
        /// <param name="en">the enumerator</param>
        /// <returns></returns>
        public static List<ReturnElementType> ToObjectList<ReturnElementType>(IEnumerator en)
        {
            Check.Require(en, "en");

            List<ReturnElementType> list = new List<ReturnElementType>();
            while (en.MoveNext())
            {
                list.Add((ReturnElementType)en.Current);
            }
            return list;
        }

        public static List<T> ConvertArrayToList<T>(T[] arr)
        {
            List<T> list = new List<T>();
            if (arr != null && arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; ++i)
                    list.Add(arr[i]);
            }

            return list;
        }

        public static bool IsEqual(object left, object right)
        {
            return IsEqual(left, right, false);
        }

        public static bool IsEqual(object left, object right, bool treatNullAndEmptyStringAsEqual)
        {
            if (left == null && right == null)
                return true;

            bool retValue = (left != null && left.Equals(right));
            if ((!retValue) && treatNullAndEmptyStringAsEqual)
            {
                if (left != null)
                    retValue = (left.ToString() == string.Empty && right == null);
                else if (right != null)
                    retValue = (right.ToString() == string.Empty && left == null);
            }
            return retValue;
        }

        #endregion
    }
}