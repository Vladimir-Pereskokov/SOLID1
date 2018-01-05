using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SOLID.Store.Validation
{
    /// <summary>
    /// Defines various extension methods for broken rule related data structures
    /// </summary>
    public static class BrokenRulesProcessor
    {

        /// <summary>
        /// Creates broken rules summary
        /// </summary>
        /// <param name="value"><see cref="IMayHaveBrokenRules"/> instance to create the broken rules summary for</param>
        /// <returns>A summary of all broken rules for a given <see cref="IMayHaveBrokenRules"/></returns>
        public static string Summary(this IMayHaveBrokenRules value)
        {
            var lst = value?.GetBrokenRules();
            if (lst != null)
            {
                var sb = new StringBuilder();
                foreach (var rl in lst)
                {
                    if (rl != null)
                    {
                        var s = rl.ToString();
                        if (!string.IsNullOrEmpty(s))
                        {
                            if (sb.Length > 0) sb.AppendLine();
                            sb.Append(s);
                        }
                    }
                }
                return sb.ToString();
            }
            return "";
        }

        /// <summary>
        /// Pushes given broken rule into given enumerable of broken rule items
        /// </summary>
        /// <param name="value">instance of <see cref="IEnumerable{BrokenRule}"/> to push new rule into</param>
        /// <param name="newItem">New Business Rule item to be pushed in</param>
        /// <returns>New instance of <see cref="IEnumerable{BrokenRule}"/></returns>
        public static IEnumerable<BrokenRule> PushRule(this IEnumerable<BrokenRule> value, BrokenRule newItem)
        {
            if (newItem == null) return value;
            List<BrokenRule> lst = null;
            if (value != null && lst.GetType().IsInstanceOfType(value)) lst = (List<BrokenRule>)value;
            else if (value != null && value.Any()) lst = new List<BrokenRule>(value);
            else lst = new List<BrokenRule>();
            lst.Add(newItem);
            return lst;
        }

        /// <summary>
        /// Pushes new broken rule into given enumerable of broken rule items
        /// </summary>
        /// <param name="value">instance of <see cref="IEnumerable{BrokenRule}"/> to push new rule into</param>
        /// <param name="propertyName">property name for new broken rule</param>
        /// <param name="problemDescription">problem description for new broken rule</param>
        /// <returns></returns>
        public static IEnumerable<BrokenRule> PushRule(this IEnumerable<BrokenRule> value,
            string propertyName, string problemDescription) => PushRule(value, new BrokenRule(propertyName, problemDescription));

    }
}