namespace eGym.Core.Domain
{
    public enum SearchStringKindEnum
    {
        /// <summary>
        /// a start with b (InvariantCultureIgnoreCase)
        /// </summary>
        StartsWith,
        /// <summary>
        /// a ends with b (InvariantCultureIgnoreCase)
        /// </summary>
        EndsWith,
        /// <summary>
        /// a contains b (InvariantCultureIgnoreCase)
        /// </summary>
        Contains,
        /// <summary>
        /// a==b (InvariantCultureIgnoreCase)
        /// </summary>
        Match,

        ///// <summary>
        ///// a start with b (CaseSensitive)
        ///// </summary>
        //ExactStartsWith,
        ///// <summary>
        ///// a end with b (CaseSensitive)
        ///// </summary>
        //ExactEndsWith,
        ///// <summary>
        ///// a contains b (CaseSensitive)
        ///// </summary>
        //ExactContains,
        ///// <summary>
        ///// a==b (CaseSensitive)
        ///// </summary>
        //ExactMatch,
    }
}
