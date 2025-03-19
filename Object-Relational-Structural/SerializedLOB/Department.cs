namespace SerializedLOB
{
    /// <summary>
    /// Object to serialized as LOB.
    /// </summary>
    public class Department
    {
        public string? Name { get; set; }

        /// <summary>
        /// Hierarchy
        /// </summary>
        public List<Department>? Subsidiaries { get; set; } = [];
    }
}
