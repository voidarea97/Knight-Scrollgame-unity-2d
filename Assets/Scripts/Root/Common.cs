using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public static class Common
    {
        public delegate void CallbackFunc();
        public static IEnumerator WaitTime(float time,CallbackFunc func)
        {
            yield return new WaitForSecondsRealtime(time);
            func();
        }
    }

}
