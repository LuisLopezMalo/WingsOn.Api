using System;
using System.Reflection;
using WingsOn.Domain;

namespace WingsOn.Api.Extensions
{
    /// <summary>
    /// Class to extend methods for the already given functionality.
    /// In this class, extensions methods should be placed to expand the current functionality.
    /// As the code is right now, not many relevant extensions can be made.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Check if a <see cref="DomainObject"/> object is null.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns></returns>
        public static bool IsNull(this DomainObject obj)
        {
            return obj == null;
        }
    }
}
