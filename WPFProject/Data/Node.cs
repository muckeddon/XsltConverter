namespace WPFProject.Data
{
    /// <summary>
    /// Представляет класс ноды.
    /// </summary>
    internal class Node
    {
        #region Свойства

        /// <summary>
        /// Получает или возвращает наименование ноды.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Получает или возвращает количество потомков ноды.
        /// </summary>
        public int ChildrensCount { get; set; }

        #endregion Свойства

    }
}
