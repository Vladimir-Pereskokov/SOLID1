using System;
using System.Collections.Generic;
using SOLID.Store.DTOs.BaseDTO;
using SOLID.Store.Entities.BaseEntities;


namespace SOLID.Store.DataAccess{

    /// <summary>
    /// Defines search criteria to be used to populate lists of DTO objects.
    /// Use the indexer to specify the Key/Value pairs of the search property name and search property value
    /// </summary>
    public class SearchCriteria {
        private readonly Guid m_ID;
        private readonly Type m_dtoType;
        private Dictionary<string, string> m_dic = null;
        private SearchCriteria() : base() { }
        /// <summary>
        /// Constructs search criteria
        /// </summary>
        /// <param name="dtoType">DTO object type to construct the criteria for</param>
        protected internal SearchCriteria(Type dtoType): this(dtoType, Guid.Empty) {}

        /// <summary>
        /// Constructs search criteria
        /// </summary>
        /// <param name="dtoType">DTO object type to construct the criteria for</param>
        /// <param name="id">Value of the id (primary key) to search record(s) by</param>
        protected internal SearchCriteria(Type dtoType, Guid id) {m_ID = id; m_dtoType = dtoType; }

        /// <summary>
        /// Creates search criteria
        /// </summary>
        /// <typeparam name="T">type of DTO object(s) to read</typeparam>
        /// <returns></returns>
        public static SearchCriteria Create<T>() where T : pocoBase => new SearchCriteria(typeof(T));

        /// <summary>
        /// Creates search criteria
        /// </summary>
        /// <typeparam name="T">type of DTO object(s) to read</typeparam>
        /// <param name="id">id (primary key) of entity to read</param>
        /// <returns></returns>
        public static SearchCriteria Create<T>(Guid id) where T : pocoBase => new SearchCriteria(typeof(T), id);

        /// <summary>
        /// Creates search criteria
        /// </summary>
        /// <typeparam name="E">type of Entity object to read</typeparam>
        /// <returns></returns>
        public static SearchCriteria CreateForEntity<E>() where E : Entity {
            return CreateForEntity<E>(Guid.Empty);            
        }

        /// <summary>
        /// Creates search criteria
        /// </summary>
        /// <typeparam name="E">type of Entity object to read</typeparam>
        /// <param name="id">id (primary key) of Entity object to read</param>
        /// <returns></returns>
        public static SearchCriteria CreateForEntity<E>(Guid id) where E : Entity
        {
            return new SearchCriteria(DataAccessService.Current.ResolveDtoType<E>(), id);
        }

        /// <summary>
        /// ID of the item to fetch
        /// </summary>
        public Guid ID => m_ID;

        /// <summary>
        /// Type of the entity to search records for
        /// </summary>
        public Type dtoType => m_dtoType;
 
        /// <summary>
        /// Indexer. May be used for more complex search scenarios
        /// </summary>
        /// <param name="propertyName">search property name</param>
        /// <returns>search value for given property name</returns>
        public string this[string propertyName] {
            get {
                if(m_dic == null || string.IsNullOrEmpty(propertyName)) return null;
                else {
                    if(m_dic.TryGetValue(propertyName, out var vl)) return vl;
                    else return null;
                }
            }
            set {
                if (!string.IsNullOrEmpty(propertyName)) {
                    if(m_dic == null) m_dic = new Dictionary<string, string>();
                    m_dic[propertyName] = value;
                }
            }
        }


    }

}
