using System;


namespace SOLID.Store.Validation {
    /// <summary>
    /// Desribes a broken business rule
    /// </summary>
    public class BrokenRule {
        internal BrokenRule() :base(){}
        public BrokenRule(string attribute, string problem) 
            {m_Attribute = attribute; m_Problem = problem;}
        
        private readonly string  m_Attribute = null;
        private readonly string  m_Problem = null;


        /// <summary>
        /// Name of the property which has a briken rulle associated with it
        /// </summary>
        public string PropertyName =>m_Attribute;

        /// <summary>
        /// Problem description for the broken rule
        /// </summary>
        public string Problem =>m_Problem;
        
        public override string ToString() => $"{m_Attribute}: {m_Problem}";
    }
}