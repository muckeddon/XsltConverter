using System.Collections.Generic;
using System.Xml;

namespace WPFProject.Services
{
    /// <summary>
    /// Представляет сервис по добавлению атрибутов.
    /// </summary>
    internal class AttributeAdder : XmlTransformer
    {
        #region Методы

        /// <summary>
        /// Добавляет атрибут count для всех элементов, потомками которых являются элементы item.
        /// </summary>
        /// <param name="path"> Путь к xml файлу. </param>
        public void AddAttributeWithNumberOfElements(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);

            var allNodesForChildCount = GetAllNodesForChildCount(doc);

            foreach(var node in allNodesForChildCount)
            {
                int childCount = node.ChildNodes.Count;
                var xmlElement = (XmlElement)node;
                xmlElement.SetAttribute("count", childCount.ToString());
            }

            doc.Save(path);
        }

        #endregion Методы
    }
}
