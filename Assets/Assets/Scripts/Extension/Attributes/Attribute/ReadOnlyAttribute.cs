﻿using System;
using UnityEngine;

namespace Extension
{
    namespace Attributes
    {
        /// <summary>
        /// Used to mark inspectable fields as read-only (that is, making them uneditable, even if they are visible).
        /// </summary>
        [AttributeUsage(AttributeTargets.Field)]
        public class ReadOnlyAttribute : PropertyAttribute { }
    }
}
